using LobbyServiceWebAPIModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LobbyServiceWebAPIModels.ResponseModels.v1
{
	public class ErrorMessage
	{
		public ErrorCode ErrorCode { get; set; }
		public string ShortDescription { get; set; }
		public string Details { get; set; }
	}
}
