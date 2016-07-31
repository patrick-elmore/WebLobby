using LobbyServiceDataModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LobbyServiceDataModels.Interfaces
{
	public interface IUserAction
	{
		LobbyResult Result { get; }

		DateTime TimeOfResult { get; set; }

		bool UserAcknowledgedResult { get; set; }

		DateTime AcknowledgedTime { get; set; }
		void DoLobbyCompleteAction();
	}
}
