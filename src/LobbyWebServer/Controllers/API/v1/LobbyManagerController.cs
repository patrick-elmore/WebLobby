using LobbyService.LobbyLogic;
using LobbyServiceWebAPIModels.ResponseModels.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LobbyWebServer.Controllers.API.v1
{
    public class LobbyManagerController : ApiController
    {
		[Route("api/manager/{database}")]
		[HttpPost]
		public LobbyManagerInstance CreateLobbyManager(string database)
		{
			LobbyManager manager = LobbyManager.GetLobbyManagerForDb(database);
			LobbyManagerInstance managerInstance = new LobbyManagerInstance() { FoundExistingInstace = true };

			if (manager == null)
			{
				manager = LobbyManager.CreateNewLobbyManagerInstance(database);
				managerInstance.FoundExistingInstace = false;
			}

			managerInstance.Database = manager.Database;

			return managerInstance;
		}
    }
}
