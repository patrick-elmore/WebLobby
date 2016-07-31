using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LobbyDataStoreService.Interfaces
{
    public interface ILobbyDataStore<T>
    {
        void SaveUserList(List<T> userList);

        List<T> LoadUserList();

        void ClearUserList();
    }
}
