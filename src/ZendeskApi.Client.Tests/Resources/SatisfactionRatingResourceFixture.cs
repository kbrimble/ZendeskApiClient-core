using System;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;
using Moq;
using Xunit;
using ZendeskApi.Contracts.Requests;

namespace ZendeskApi.Client.Tests.Resources
{
    public class SatisfactionRatingResourceFixture
    {
        [Fact]
        public void Post_MultipleMethodsAreCalled_CalledUrlIsCorrect()
        {
            // Given
            var client = new Mock<IRestClient>();
            client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://zendesk"));
            var resource = new SatisfactionRatingResource(client.Object);

            // When
            resource.Get(321);
            resource.Post(new SatisfactionRatingRequest(), 1);

            // Then
            client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("/tickets/1")), ""));
        }

        [Fact]
        public async void GetAsync_Called_CallsBuildUriWithFieldId()
        {
            // Given
            var client = new Mock<IRestClient>();
            client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://zendesk"));
            var resource = new SatisfactionRatingResource(client.Object);

            // When
            await resource.GetAsync(321);

            // Then
            client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("/satisfaction_ratings/321")), ""));
        }

        [Fact]
        public async void GetAsync_Called_ReturnsResponse()
        {
            // Given
            var client = new Mock<IRestClient>();
            var response = new SatisfactionRatingResponse { Item = new SatisfactionRating { Id = 1 }};
            client.Setup(b => b.GetAsync<SatisfactionRatingResponse>(
                It.IsAny<Uri>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(TaskHelper.CreateTaskFromResult(response));

            client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://zendesk"));
            var resource = new SatisfactionRatingResource(client.Object);

            // When
            var result = await resource.GetAsync(321);

            // Then
            Assert.Equal(result, response);
        }

        [Fact]
        public async void PostAsync_Called_BuildsUri()
        {
            // Given
            var client = new Mock<IRestClient>();
            var request = new SatisfactionRatingRequest { Item = new SatisfactionRating { Score = SatisfactionRatingScore.good } };
            var resource = new SatisfactionRatingResource(client.Object);

            // When
            await resource.PostAsync(request, 231);

            // Then
            client.Verify(b => b.BuildUri(It.IsAny<string>(), ""));
        }

        [Fact]
        public async void PostAsync_CalledWithRating_ReturnsUserReponse()
        {
            // Given
            var client = new Mock<IRestClient>();
            var response = new SatisfactionRatingResponse { Item = new SatisfactionRating { Score = SatisfactionRatingScore.good } };
            var request = new SatisfactionRatingRequest { Item = new SatisfactionRating { Score = SatisfactionRatingScore.good } };
            client.Setup(b => b.PostAsync<SatisfactionRatingResponse>(
                It.IsAny<Uri>(), 
                request, 
                "application/json", 
                It.IsAny<string>(), 
                It.IsAny<string>()))
                .Returns(TaskHelper.CreateTaskFromResult(response));

            var resource = new SatisfactionRatingResource(client.Object);

            // When
            var result = await resource.PostAsync(request, 21);

            // Then
            Assert.Equal(result, response);
        }
    }
}
