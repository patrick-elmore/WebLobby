using LobbyService.LobbyDataObjects;
using LobbyService.LobbyLogic;
using LobbyServiceDataModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LobbyWebServer.Models
{
	public class LobbyMonitorModel
	{
		public List<ILobbyUserStatus> Users { get; set; }

		public LobbyMonitorModel()
		{
			Users = LobbyManager.GetLobbyManagerForDb("TEST").GetUsersInLobby();
		}

	}
}