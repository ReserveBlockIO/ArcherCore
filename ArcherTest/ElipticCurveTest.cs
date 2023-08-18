using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherTest
{
    public class ElipticCurveTest
    {
        [Test]
        public void SignatureTest()
        {
            ArcherCore.EllipticCurve.PrivateKey privateKey = new ArcherCore.EllipticCurve.PrivateKey();
            var message = "Some Message To Sign.";
            var signature = ArcherCore.EllipticCurve.Ecdsa.sign(message, privateKey);

            var signatureVerify = ArcherCore.EllipticCurve.Ecdsa.verify(message, signature, privateKey.publicKey());

            if (signatureVerify)
                Assert.Pass();

            Assert.Fail();
        }
    }
}
