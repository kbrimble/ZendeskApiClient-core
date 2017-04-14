using System;
using System.Collections.Generic;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;
using Moq;
using Xunit;

namespace ZendeskApi.Client.Tests.Resources
{
    public class RequestResourceFixture
    {
        [Fact]
        public async void GetAsync_Called_CallsBuildUriWithFieldId()
        {
            // Given
            var client = new Mock<IRestClient>();
            client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://search"));
            var resource = new RequestResource(client.Object);

            // When
            await resource.GetAsync(321);

            // Then
            client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("321")), ""));
        }

        [Fact]
        public async void GetAsync_Called_ReturnsRequestResponse()
        {
            // Given
            var client = new Mock<IRestClient>();
            var response = new RequestResponse { Item = new Request { Id = 1 } };
            client.Setup(b => b.GetAsync<RequestResponse>(
                It.IsAny<Uri>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(TaskHelper.CreateTaskFromResult(response));

            var resource = new RequestResource(client.Object);

            // When
            var result = await resource.GetAsync(321);

            // Then
            Assert.Equal(result, response);
        }

        [Fact]
        public async void GetAsync_CalledWithStatuses_CallsBuildUriWithStatuses()
        {
            // Given
            var client = new Mock<IRestClient>();
            client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://search"));
            var resource = new RequestResource(client.Object);
            var statuses = new List<TicketStatus> {TicketStatus.Hold, TicketStatus.Open};

            // When
            await resource.GetAsync(statuses);

            // Then
            client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("requests")), It.Is<string>(s => s.Contains("status=hold,open"))));
        }

        [Fact]
        public async void PutAsync_Called_BuildsUri()
        {
            // Given
            var client = new Mock<IRestClient>();
            var request = new RequestRequest { Item = new Request { Subject = "blah blah", Id = 123 } };
            var resource = new RequestResource(client.Object);

            // When
            await resource.PutAsync(request);

            // Then
            client.Verify(b => b.BuildUri(It.IsAny<string>(), ""));
        }

        [Fact]
        public async void PutAsync_CalledWithRequest_ReturnsRequestReponse()
        {
            // Given
            var client = new Mock<IRestClient>();
            var response = new RequestResponse { Item = new Request { Subject = "blah blah" } };
            var request = new RequestRequest { Item = new Request { Subject = "blah blah", Id = 123 } };
            client.Setup(b => b.PutAsync<RequestResponse>(
                It.IsAny<Uri>(), 
                request, 
                "application/json",
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(TaskHelper.CreateTaskFromResult(response));
            var resource = new RequestResource(client.Object);

            // When
            var result = await resource.PutAsync(request);

            // Then
            Assert.Equal(result, response);
        }

        [Fact]
        public void PutAsync_RequestHasNoId_ThrowsException()
        {
            // Given
            var client = new Mock<IRestClient>();
            var response = new RequestResponse { Item = new Request { Subject = "blah blah" } };
            var request = new RequestRequest { Item = new Request { Subject = "blah blah" } };
            client.Setup(b => b.PutAsync<RequestResponse>(
                It.IsAny<Uri>(), 
                request, 
                "application/json",
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(TaskHelper.CreateTaskFromResult(response));

            var requestResource = new RequestResource(client.Object);

            // When, Then
            Assert.Throws<ArgumentException>(async () => await requestResource.PutAsync(request));
        }

        [Fact]
        public async void PostAsync_Called_BuildsUri()
        {
            // Given
            var client = new Mock<IRestClient>();
            var request = new RequestRequest { Item = new Request { Subject = "blah blah" } };
            var requestResource = new RequestResource(client.Object);

            // When
            await requestResource.PostAsync(request);

            // Then
            client.Verify(b => b.BuildUri(It.IsAny<string>(), ""));
        }

        [Fact]
        public async void PostAsync_CalledWithRequest_ReturnsRequestReponse()
        {
            // Given
            var client = new Mock<IRestClient>();
            var response = new RequestResponse { Item = new Request { Subject = "blah blah" } };
            var request = new RequestRequest { Item = new Request { Subject = "blah blah" } };
            client.Setup(b => b.PostAsync<RequestResponse>(
                It.IsAny<Uri>(), 
                request, 
                "application/json",
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(TaskHelper.CreateTaskFromResult(response));

            var requestResource = new RequestResource(client.Object);

            // When
            var result = await requestResource.PostAsync(request);

            // Then
            Assert.Equal(result, response);
        }

        [Fact]
        public async void DeleteAsync_Called_CallsBuildUriWithFieldId()
        {
            // Given
            var client = new Mock<IRestClient>();
            client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://search"));
            var requestResource = new RequestResource(client.Object);

            // When
            await requestResource.DeleteAsync(321);

            // Then
            client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("321")), ""));
        }

        [Fact]
        public async void DeleteAsync_Called_CallsDeleteOnClient()
        {
            // Given
            var client = new Mock<IRestClient>();
            var response = new RequestResponse { Item = new Request { Id = 1 } };
            client.Setup(b => b.GetAsync<RequestResponse>(It.IsAny<Uri>(), It.IsAny<string>(), It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));
            var requestResource = new RequestResource(client.Object);

            // When
            await requestResource.DeleteAsync(321);

            // Then
            client.Verify(c => c.DeleteAsync<object>(It.IsAny<Uri>(), It.IsAny<object>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
        }
    }
}
