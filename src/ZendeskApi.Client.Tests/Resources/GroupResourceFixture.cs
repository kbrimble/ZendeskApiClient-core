using System;
using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;
using Moq;
using Xunit;

namespace ZendeskApi.Client.Tests.Resources
{
    public class GroupResourceFixture
    {
        [Fact]
        public async void GetAsync_Called_CallsBuildUriWithFieldId()
        {
            // Given
            var client = new Mock<IRestClient>();
            client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://zendesk"));
            var groupResource = new GroupsResource(client.Object);

            // When
            await groupResource.GetAsync(321);

            // Then
            client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("/groups/321")), ""));
        }

        [Fact]
        public async void GetAsync_Called_ReturnsResponse()
        {
            // Given
            var client = new Mock<IRestClient>();
            var response = new GroupResponse { Item = new Group { Id = 1 }};
            client.Setup(b => b.GetAsync<GroupResponse>(
                It.IsAny<Uri>(),
                It.IsAny<string>(),
                It.IsAny<string>()
                ))
                .Returns(TaskHelper.CreateTaskFromResult(response));

            client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://zendesk"));
            var groupResource = new GroupsResource(client.Object);

            // When
            var result = await groupResource.GetAsync(321);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }
    }
}
