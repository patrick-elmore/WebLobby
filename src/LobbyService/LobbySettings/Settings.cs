using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LobbyService
{
	public class Settings
	{
		int _timeoutSeconds;
		TimeSpan _timeout;

		public string Database { get; set; }
		public int MaxCapacity { get; set; }
		public int RefreshIntervalSeconds { get; set; }
		public int TimeoutSeconds {
			get
			{
				return _timeoutSeconds;
			}
				
				set
			{
				_timeoutSeconds = value;
				_timeout = TimeSpan.FromSeconds(_timeoutSeconds);
			}
		}

		[IgnoreDataMember]
		public TimeSpan Timeout
		{
			get
			{
				return _timeout;
			}
		}

	}
}
