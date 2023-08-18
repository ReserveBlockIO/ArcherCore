using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherTest
{
    public class EncryptionTest
    {
        [Test]
        public void EncryptDecryptTest()
        {
            var passPhrase = "somePassword1234";
            var message = "Some message 4321.";

            var encryptedMessage = ArcherCore.Encryption.EncryptionUtilities.Encrypt(message, passPhrase);

            var decryptedMessage = ArcherCore.Encryption.EncryptionUtilities.Decrypt(encryptedMessage, passPhrase);

            if (decryptedMessage.Equals(message))
                Assert.Pass();

            Assert.Fail();
        }
    }
}
