using QuickLearn.Demo.XmlUtility.Tests.TestData;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace QuickLearn.Demo.XmlUtility.Tests.Mocks
{
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        public FakeHttpMessageHandler()
        {

        }

        public delegate void FakeHttpClientCalledHandler(object sender, EventArgs e);
        public event FakeHttpClientCalledHandler FakeHttpClientCalled;

        private void OnFakeHttpClientCalled()
        {
            FakeHttpClientCalled?.Invoke(this, new EventArgs());
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            if (request.RequestUri.AbsoluteUri.EndsWith("PrintJob.xsd", StringComparison.InvariantCulture))
            {
                response.Content = new StringContent(XmlStrings.DOCUMENT_STRING);
            }
            else
            {
                response.Content = new StringContent(XmlStrings.SCHEMA_LISTING);
            }

            OnFakeHttpClientCalled();

            return Task.FromResult(response);
        }
    }

}
