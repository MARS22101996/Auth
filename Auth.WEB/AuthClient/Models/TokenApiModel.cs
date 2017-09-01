namespace AuthClient.Models
{
    public class TokenApiModel
    {
        public string Token { get; set; }

        public long ExpiresIn { get; set; }
    }
}
