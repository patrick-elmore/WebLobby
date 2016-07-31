using LobbyService.LobbyLogic;
using LobbyServiceDataModels.Enums;
using LobbyServiceDataModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LobbyService.LobbyDataObjects
{
	public class UserStatus : ILobbyUserStatus
	{
		private string database { get; set; }

		public UserStatus(string db, Guid userId)
		{
			database = db;
			UserId = userId;
			EnteredLobby = DateTime.UtcNow;
			LastContact = DateTime.UtcNow;
		}

		public Guid UserId { get; set; }
		public DateTime EnteredLobby { get; set; }
		public DateTime LastContact { get; set; }
		public int Position
		{
			get
			{
				LobbyManager manager = LobbyManager.GetLobbyManagerForDb(database);

				return manager.GetPositionOfUser(this);
			}
		}

		public IUserAction PostLobbyAction { get; set; }

	}

	public class UserStatusEqualityComparer : IEqualityComparer<UserStatus>
	{
		public bool Equals(UserStatus x, UserStatus y)
		{
			return x.UserId == y.UserId;
		}

		public int GetHashCode(UserStatus obj)
		{
			return obj.UserId.GetHashCode();
		}
	}
}
