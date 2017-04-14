using System;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Queries;
using ZendeskApi.Contracts.Responses;
using Moq;
using Xunit;

namespace ZendeskApi.Client.Tests.Resources
{
    public class SearchResourceFixture
    {
        [Fact]
        public async void FindAsync_Called_CallsBuildUriWithFieldId()
        {
            // Given
            var client = new Mock<IRestClient>();
            var query = new Mock<IZendeskQuery<Organization>>();
            client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321"))))
                .Returns(new Uri("http://search"));
            var searchResource = new SearchResource(client.Object);
            query.Setup(q => q.BuildQuery()).Returns("query");

            // When
            await searchResource.FindAsync(query.Object);

            // Then
            client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("search")), It.Is<string>(s => s.Contains("query"))));
        }

        [Fact]
        public async void FindAsync_Called_CallsGetOnClient()
        {
            // Given
            var client = new Mock<IRestClient>();
            var query = new Mock<IZendeskQuery<Organization>>();
            client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321"))))
                .Returns(new Uri("http://search"));
            query.Setup(q => q.BuildQuery()).Returns("query");
            var searchResource = new SearchResource(client.Object);

            // When
            await searchResource.FindAsync(query.Object);

            // Then
            client.Verify(c => c.GetAsync<ListResponse<Organization>>(
                It.IsAny<Uri>(),
                It.IsAny<string>(),
                It.IsAny<string>()));
        }
    }
}
