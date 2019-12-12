using System;
using System.Text;

using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;

namespace Carwings.ApiClient
{
    internal class Blowfish
    {
        internal string Encrypt(string password, string key)
        {
            var engine = new BlowfishEngine();
            var cipher = new PaddedBufferedBlockCipher(engine);
            var keyBytes = new KeyParameter(Encoding.UTF8.GetBytes(key));

            cipher.Init(true, keyBytes);

            var inputBuffer = Encoding.UTF8.GetBytes(password);
            var outputBuffer = new byte[cipher.GetOutputSize(inputBuffer.Length)];

            var length = cipher.ProcessBytes(inputBuffer, 0, inputBuffer.Length, outputBuffer, 0);

            cipher.DoFinal(outputBuffer, length);

            return Convert.ToBase64String(outputBuffer);
        }
    }
}