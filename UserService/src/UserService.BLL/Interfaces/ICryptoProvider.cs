namespace UserService.BLL.Interfaces
{
    public interface ICryptoProvider
    {
        string GetHash(string plaintext);

        bool VerifyHash(string text, string hashedValue);
    }
}