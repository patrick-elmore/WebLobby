using LobbyService;
using LobbyService.LobbyLogic;
using LobbyServiceWebAPIModels;
using LobbyServiceWebAPIModels.ResponseModels.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LobbyWebServer.Controllers.v1
{
	public class SettingsController : ApiController
	{
		[Route("api/{database}/settings/all")]
		[HttpPost]
		public HttpResponseMessage UpdateAll([FromBody]Settings lobbySettings, string database)
		{
			LobbyManager manager = LobbyManager.GetLobbyManagerForDb(database);

			if (manager == null)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound,
					ErrorMessageBuilder.GetErrorMessageForErrorCode(LobbyServiceWebAPIModels.Enums.ErrorCode.InstanceNotFound));
			}

			manager.UpdateLobbySettings(lobbySettings);

			return Request.CreateResponse(HttpStatusCode.Created, new SettingsUpdateResult(true));
		}
	}
}