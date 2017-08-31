using System.Collections.Generic;
using System.Security.Claims;

namespace Auth.WEB.Models
{
	public class TokenApiModel
	{
		public string Token { get; set; }

		public long ExpiresIn { get; set; }

		public List<Claim> Claims { get; set; }

		//public string token_type { get; set; }
		//public string access_token { get; set; }
		//public long expires_in { get; set; }
	}
}
