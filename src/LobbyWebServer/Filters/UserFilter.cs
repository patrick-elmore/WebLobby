using LobbyService.LobbyLogic;
using LobbyServiceDataModels.Interfaces;
using LobbyServiceWebAPIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace LobbyWebServer.Filters
{
	public class UserFilter : ActionFilterAttribute
	{
		public override void OnActionExecuting(HttpActionContext actionContext)
		{
			LobbyManager manager = (LobbyManager)actionContext.ActionArguments["Manager"];
			Guid userId = (Guid)actionContext.ActionArguments["token"];

			ILobbyUserStatus user = manager.GetLobbyStatusForUser(userId);

			if (user == null)
			{
				actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.NotFound,
					ErrorMessageBuilder.GetErrorMessageForErrorCode(LobbyServiceWebAPIModels.Enums.ErrorCode.UserNotFound));
			}
			else
			{
				actionContext.ActionArguments.Add("LobbyUser", user);
			}
		}
	}
}