using LobbyServiceDataModels.Enums;
using LobbyServiceDataModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LobbyServiceDataModels.Interfaces
{
    public interface ILobbyUserStatus
    {
        Guid UserId { get; set; }

        DateTime EnteredLobby { get; set; }

        DateTime LastContact { get; set; }

        int Position { get; }

		IUserAction PostLobbyAction { get; set; }
    }
}
