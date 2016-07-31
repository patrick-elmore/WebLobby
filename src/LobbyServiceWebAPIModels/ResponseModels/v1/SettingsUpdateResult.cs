using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LobbyServiceWebAPIModels.ResponseModels.v1
{
	public class SettingsUpdateResult
	{
		public bool Success { get; set; }

		public SettingsUpdateResult(bool result)
		{
			Success = result;
		}
	}
}
