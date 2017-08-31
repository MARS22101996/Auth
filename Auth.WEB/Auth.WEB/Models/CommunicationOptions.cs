using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.WEB.Models
{
	public class CommunicationOptions
	{
		public CommunicationOptions()
		{
			HostAddress = "localhost:5000";
			RetryTimeSpans = new[] { 0, 1 };
			DefaultPrefix = "DefaultService";
		}

		public string HostAddress { get; set; }

		public string DefaultPrefix { get; set; }

		public int[] RetryTimeSpans { get; set; }
	}
}
