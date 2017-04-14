using System;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Resources;
using Moq;
using Xunit;

namespace ZendeskApi.Client.Tests.Resources
{
    public class AssignableGroupResourceFixture
    {
        [Fact]
        public async void GetAllAsync_Called_CallsBuildUriWithFieldId()
        {
            // Given
            var client = new Mock<IRestClient>();
            client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://search"));
            var groupResource = new AssignableGroupResource(client.Object);

            // When
            await groupResource.GetAllAsync();

            // Then
            client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("/assignable")), ""));
        }

    }
}
