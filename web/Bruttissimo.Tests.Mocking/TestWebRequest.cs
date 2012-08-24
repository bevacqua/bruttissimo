using System.IO;
using System.Net;

namespace Bruttissimo.Tests.Mocking
{
    public class TestWebRequest : WebRequest
    {
        private readonly Stream requestStream;
        private readonly Stream responseStream;

        public override string Method { get; set; }
        public override string ContentType { get; set; }
        public override long ContentLength { get; set; }

        public TestWebRequest(Stream responseStream)
        {
            requestStream = new MemoryStream();
            this.responseStream = responseStream;
        }

        public override Stream GetRequestStream()
        {
            return requestStream;
        }

        public override WebResponse GetResponse()
        {
            Stream memoryStream = new MemoryStream();
            responseStream.CopyTo(memoryStream);
            responseStream.Position = memoryStream.Position = 0;
            return new TestWebReponse(memoryStream);
        }
    }
}
