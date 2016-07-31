using LobbyServiceWebAPIModels.Enums;
using LobbyServiceWebAPIModels.ResponseModels.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LobbyServiceWebAPIModels
{
	public static class ErrorMessageBuilder
	{
		private static ErrorMessage genericInstanceNotFoundMessage;
		private static ErrorMessage genericUserNotFoundMessage;
		private static ErrorMessage genericUnknownErrorMessage;

		public static ErrorMessage GetErrorMessageForErrorCode(ErrorCode errorCode)
		{
			switch (errorCode)
			{
				case ErrorCode.UserNotFound:
					return BuildUserNotFoundMessage();
				case ErrorCode.InstanceNotFound:
					return BuildManagerNotFoundMessage();
				default:
					return BuildUnkownErrorMessage();
			}
		}

		private static ErrorMessage BuildManagerNotFoundMessage()
		{
			if (genericInstanceNotFoundMessage == null)
			{
				genericInstanceNotFoundMessage = new ErrorMessage();
				genericInstanceNotFoundMessage.ErrorCode = ErrorCode.InstanceNotFound;
				genericInstanceNotFoundMessage.ShortDescription = "Lobby Not Found";
				genericInstanceNotFoundMessage.Details = "No lobby found for specified instance";
			}

			return genericInstanceNotFoundMessage;
		}

		private static ErrorMessage BuildUserNotFoundMessage()
		{
			if (genericUserNotFoundMessage == null)
			{
				genericUserNotFoundMessage = new ErrorMessage();
				genericUserNotFoundMessage.ErrorCode = ErrorCode.InstanceNotFound;
				genericUserNotFoundMessage.ShortDescription = "User Not Found";
				genericUserNotFoundMessage.Details = "No user found for the provided user ID";
			}

			return genericUserNotFoundMessage;
		}

		private static ErrorMessage BuildUnkownErrorMessage()
		{
			if (genericUnknownErrorMessage == null)
			{
				genericUnknownErrorMessage = new ErrorMessage();
				genericUnknownErrorMessage.ErrorCode = ErrorCode.InstanceNotFound;
				genericUnknownErrorMessage.ShortDescription = "Unknown Error";
				genericUnknownErrorMessage.Details = "An unkown error has occurred";
			}

			return genericUnknownErrorMessage;
		}
	}
}
