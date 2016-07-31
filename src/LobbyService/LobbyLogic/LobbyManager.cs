using LobbyDataStoreService.DataProviders;
using LobbyService.LobbyDataObjects;
using LobbyServiceDataModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace LobbyService.LobbyLogic
{
    public class LobbyManager
    {
        private static List<LobbyManager> _lobbyManagers = new List<LobbyManager>();


        public static LobbyManager GetLobbyManagerForDb(string db)
        {
            LobbyManager manager = _lobbyManagers.Where(x => x.Database == db).FirstOrDefault();

            return manager;
        }

		public static LobbyManager CreateNewLobbyManagerInstance(string db)
		{
			LobbyManager manager = _lobbyManagers.Where(x => x.Database == db).FirstOrDefault();

			if (manager == null)
			{
				manager = new LobbyManager(db);
				_lobbyManagers.Add(manager);
			}

			return manager;
		}

        public string Database { get; set; }
		private Settings settings;

        private List<ILobbyUserStatus> _masterUserList = new List<ILobbyUserStatus>();
        private List<ILobbyUserStatus> _readOnlyUserList = new List<ILobbyUserStatus>();

		private List<ILobbyUserStatus> _dequeuedUsers = new List<ILobbyUserStatus>();
		private List<ILobbyUserStatus> _timedOutUsers = new List<ILobbyUserStatus>();
		private List<ILobbyUserStatus> _archivedUsers = new List<ILobbyUserStatus>();
		private List<ILobbyUserStatus> _updateContactTimeUsers = new List<ILobbyUserStatus>();

		private object queueLock = new object();
        private object readOnlyQueueLock = new object();
		private object dequeueLock = new object();
		private object timedOutLock = new object();

        private Timer dequeueTimer;

        public LobbyManager(string db)
        {
            Database = db;
            dequeueTimer = new Timer(10000);
            dequeueTimer.Elapsed += DequeueTimer_Elapsed;
			dequeueTimer.Start();
        }

		public void UpdateLobbySettings(Settings newSettings)
		{
			settings = newSettings;
		}

        private void DequeueTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            int numberOfAvailableSpots = getAvailableCapacity();

			if (numberOfAvailableSpots > 0 && _readOnlyUserList.Count > 0)
			{
				List<ILobbyUserStatus> usersToAdvance = new List<ILobbyUserStatus>(_readOnlyUserList.GetRange(0, numberOfAvailableSpots).ToList());

				advanceUsers(usersToAdvance);
			}

			if (_readOnlyUserList.Count > 0)
			{
				List<ILobbyUserStatus> expiredUsers = new List<ILobbyUserStatus>(_readOnlyUserList.Where(x => x.LastContact.Add(settings.Timeout) < DateTime.UtcNow).ToList());

				timeoutUsers(expiredUsers);
			}
        }

		private int getAvailableCapacity()
		{

			int activeUserCount = 10;

			int availableCapacity = settings.MaxCapacity - activeUserCount;

			return availableCapacity;
		}
		private void advanceUsers(List<ILobbyUserStatus> users)
		{
			List<ILobbyUserStatus> advancingUsers = new List<ILobbyUserStatus>(users);

			foreach(ILobbyUserStatus u in advancingUsers)
			{
				u.PostLobbyAction = new LobbySuccessAction();
				u.PostLobbyAction.TimeOfResult = DateTime.UtcNow;
			}

			lock (_masterUserList)
			{
				_masterUserList = _masterUserList.Except(advancingUsers).ToList();
				_readOnlyUserList = new List<ILobbyUserStatus>(_masterUserList);
			}

			lock (dequeueLock)
			{
				_dequeuedUsers.AddRange(advancingUsers);
			}
		}

		private void timeoutUsers(List<ILobbyUserStatus> users)
		{
			List<ILobbyUserStatus> timedOutUsers = new List<ILobbyUserStatus>(users);

			foreach (ILobbyUserStatus u in timedOutUsers)
			{
				u.PostLobbyAction = new TimeOutAction();
				u.PostLobbyAction.TimeOfResult = DateTime.UtcNow;
			}

			lock (_masterUserList)
			{
				_masterUserList = _masterUserList.Except(timedOutUsers).ToList();
				_readOnlyUserList = new List<ILobbyUserStatus>(_masterUserList);
			}

			lock (dequeueLock)
			{
				_timedOutUsers.AddRange(timedOutUsers);
			}
		}

		public ILobbyUserStatus GetLobbyStatusForUser(Guid userId)
        {
            ILobbyUserStatus status = _readOnlyUserList.Where(x => x.UserId == userId).FirstOrDefault();
            return status;
        }

		public List<ILobbyUserStatus> GetUsersInLobby()
		{
			return _readOnlyUserList;
		}

        public int GetPositionOfUser(ILobbyUserStatus userStatus)
        {
            int position = _readOnlyUserList.IndexOf(userStatus);

            return position;
        }

        public ILobbyUserStatus AddUserToLobby(Guid userId)
        {
			ILobbyUserStatus existingUser = GetLobbyStatusForUser(userId);

			if (existingUser != null)
			{
				return existingUser;
			}

            ILobbyUserStatus userStatus = new UserStatus(this.Database, userId);

            lock (queueLock)
            {
                _masterUserList.Add(userStatus);                
            }

            lock (readOnlyQueueLock)
            {
                _readOnlyUserList = new List<ILobbyUserStatus>(_masterUserList);
            }

            Redis.Instance.SaveUserList(_readOnlyUserList);

            return userStatus;
        }
    }
}
