using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LobbyDataStoreService.Interfaces;
using LobbyServiceDataModels.Interfaces;

namespace LobbyDataStoreService.DataProviders
{
    public class Redis : ILobbyDataStore<ILobbyUserStatus>
    {
        private readonly string redisKeyLobbyQueue = "TEST:LobbyQueue";

        private static Redis instance;

        private RedisManagerPool _managerPool;

        private string connectionString = "redis://sql1:6379?ConnectTimeout=5000&IdleTimeOutSecs=180";

        public static Redis Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Redis();
                }

                return instance;
            }
        }


        private Redis()
        {
            _managerPool = new RedisManagerPool(connectionString);
        }

        public void SaveUserList(List<ILobbyUserStatus> userList)
        {
            _managerPool.GetClient().Set(redisKeyLobbyQueue, userList);
        }

        public List<ILobbyUserStatus> LoadUserList()
        {
            List<ILobbyUserStatus> userList = _managerPool.GetClient().Get<List<ILobbyUserStatus>>(redisKeyLobbyQueue);

            return userList;
        }

        public void ClearUserList()
        {
            _managerPool.GetClient().Remove(redisKeyLobbyQueue);
        }
    }
}
