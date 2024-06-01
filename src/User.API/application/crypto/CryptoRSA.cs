using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace User.API.appkication.crypto;

public static class CryptoRSA
{
    /// <summary>
    /// 公開鍵暗号で文字列を暗号化する
    /// </summary>
    /// <param name="text">平文の文字列</param>
    /// <param name="publickey">公開鍵</param>
    /// <returns>暗号化された文字列</returns>
    public static string Encrypto(string text, string publicKey)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(publicKey);
            byte[] data = Encoding.UTF8.GetBytes(text);
            data = rsa.Encrypt(data, false);

            return Convert.ToBase64String(data);
        }
    }

    /// <summary>
    /// 対称鍵暗号で暗号文を復号する
    /// </summary>
    /// <param name="cipher">平文の文字列</param>
    /// <param name="privatekey">秘密鍵</param>
    /// <returns>復号された文字列</returns>
    public static string Decrypto(string cipher, string privateKey)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(privateKey);
            byte[] data = Convert.FromBase64String(cipher);
            data = rsa.Decrypt(data, false);

            return Encoding.UTF8.GetString(data);
        }
    }
}