using LobbyService.LobbyLogic;
using LobbyServiceDataModels.Interfaces;
using LobbyServiceWebAPIModels;
using LobbyServiceWebAPIModels.ResponseModels.v1;
using LobbyWebServer.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LobbyWebServer.Controllers.v1
{
	[LobbyManagerInstanceFilter]
	public class UserController : ApiController
    {
		[Route("api/{database}/User/{token}")]
		[HttpPost]
		public HttpResponseMessage AddUser(string database, Guid token)
		{
			LobbyManager manager = (LobbyManager)ActionContext.ActionArguments["Manager"];

			manager.AddUserToLobby(token);

			return Request.CreateResponse(HttpStatusCode.Created, manager.GetLobbyStatusForUser(token));
		}

		[Route("api/{database}/User")]
		[HttpPost]
		public HttpResponseMessage AddUsers(string database, [FromBody] List<Guid> users)
		{
			LobbyManager manager = (LobbyManager)ActionContext.ActionArguments["Manager"];

			int usersAdded = 0;

			foreach (Guid userId in users)
			{
				manager.AddUserToLobby(userId);
				usersAdded++;
			}

			return Request.CreateResponse(HttpStatusCode.Created, usersAdded);
		}

		[Route("api/{database}/User/{token}")]
		[HttpGet]
		[UserFilter]
		public HttpResponseMessage GetUserStatus(string database, Guid token)
		{
			ILobbyUserStatus userStatus = (ILobbyUserStatus)ActionContext.ActionArguments["LobbyUser"];
			return Request.CreateResponse(HttpStatusCode.OK, userStatus);
		}

	}
}
