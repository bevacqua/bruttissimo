using System.IO;
using System.Net;
using Bruttissimo.Common.Guard;

namespace Bruttissimo.Tests.Mocking
{
    public class TestWebReponse : WebResponse
    {
        private readonly Stream responseStream;

        public TestWebReponse(Stream responseStream)
        {
            Ensure.That(responseStream, "responseStream").IsNotNull();

            this.responseStream = responseStream;
        }

        public override Stream GetResponseStream()
        {
            return responseStream;
        }

        public override WebHeaderCollection Headers
        {
            get { return new WebHeaderCollection(); }
        }
    }
}
