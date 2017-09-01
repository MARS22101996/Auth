namespace AuthHost.WEB.Models.AccountApiModels
{
    public class TokenApiModel
    {
        public string Token { get; set; }

        public long ExpiresIn { get; set; }
    }
}