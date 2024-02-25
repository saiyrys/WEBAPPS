namespace Markets.Abstractions;

public interface IEncryptService
{
    byte[] GenerateSalt();
    byte[] HashPassword(string password, byte[] salt);
}