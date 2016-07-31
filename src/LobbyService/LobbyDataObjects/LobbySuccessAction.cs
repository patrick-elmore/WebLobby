using LobbyServiceDataModels.Enums;
using LobbyServiceDataModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LobbyService.LobbyDataObjects
{
	public class LobbySuccessAction : IUserAction
	{

		public LobbyResult Result
		{
			get
			{
				return LobbyResult.Success;
			}
		}
		public DateTime TimeOfResult { get; set; }

		public bool UserAcknowledgedResult { get; set; }

		public DateTime AcknowledgedTime { get; set; }

		public void DoLobbyCompleteAction()
		{

		}
	}
}
