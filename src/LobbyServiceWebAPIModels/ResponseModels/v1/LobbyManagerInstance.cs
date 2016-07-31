using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LobbyServiceWebAPIModels.ResponseModels.v1
{
	public class LobbyManagerInstance
	{
		public string Database { get; set; }
		public bool FoundExistingInstace { get; set; }
	}
}
