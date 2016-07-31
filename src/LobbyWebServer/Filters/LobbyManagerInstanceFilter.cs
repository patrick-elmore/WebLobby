using LobbyService.LobbyLogic;
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
	public class LobbyManagerInstanceFilter : ActionFilterAttribute
	{

		public override void OnActionExecuting(HttpActionContext actionContext)
		{
			string database = actionContext.ActionArguments["database"].ToString();

			LobbyManager manager = LobbyManager.GetLobbyManagerForDb(database);

			if (manager == null)
			{
				actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.NotFound,
					ErrorMessageBuilder.GetErrorMessageForErrorCode(LobbyServiceWebAPIModels.Enums.ErrorCode.InstanceNotFound));
			}
			else
			{
				actionContext.ActionArguments.Add("Manager", manager);
			}
		}
	}
}