using System;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;
using Moq;
using Xunit;

namespace ZendeskApi.Client.Tests.Resources
{
    public class OrganizationResourceFixture
    {
        [Fact]
        public async void GetAsync_Called_CallsBuildUriWithFieldId()
        {
            // Given
            var client = new Mock<IRestClient>();
            client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://search"));
            var resource = new OrganizationResource(client.Object);

            // When
            await resource.GetAsync(321);

            // Then
            client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("321")), ""));
        }

        [Fact]
        public async void GetAsync_Called_ReturnsResponse()
        {
            // Given
            var client = new Mock<IRestClient>();
            var response = new OrganizationResponse { Item = new Organization { Id = 1 } };
            client.Setup(b => b.GetAsync<OrganizationResponse>(
                It.IsAny<Uri>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(TaskHelper.CreateTaskFromResult(response));

            var resource = new OrganizationResource(client.Object);

            // When
            var result = await resource.GetAsync(321);

            // Then
            Assert.Equal(result, response);
        }

        [Fact]
        public async void PutAsync_Called_BuildsUri()
        {
            // Given
            var client = new Mock<IRestClient>();
            var request = new OrganizationRequest { Item = new Organization { Name = "Organizations", Id = 123 } };
            var resource = new OrganizationResource(client.Object);

            // When
            await resource.PutAsync(request);

            // Then
            client.Verify(b => b.BuildUri(It.IsAny<string>(), ""));
        }

        [Fact]
        public async void PutAsync_CalledWithItem_ReturnsReponse()
        {
            // Given
            var client = new Mock<IRestClient>();
            var response = new OrganizationResponse { Item = new Organization { Name = "blah blah" } };
            var request = new OrganizationRequest { Item = new Organization { Name = "blah blah", Id = 123 } };
            client.Setup(b => b.PutAsync<OrganizationResponse>(
                It.IsAny<Uri>(), 
                request, 
                "application/json",
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(TaskHelper.CreateTaskFromResult(response));

            var resource = new OrganizationResource(client.Object);

            // When
            var result = await resource.PutAsync(request);

            // Then
            Assert.Equal(result, response);
        }

        [Fact]
        public void PutAsync_HasNoId_ThrowsException()
        {
            // Given
            var client = new Mock<IRestClient>();
            var response = new OrganizationResponse { Item = new Organization { Name = "blah blah" } };
            var request = new OrganizationRequest { Item = new Organization { Name = "blah blah" } };
            client.Setup(b => b.PutAsync<OrganizationResponse>(
                It.IsAny<Uri>(), 
                request, 
                "application/json",
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(TaskHelper.CreateTaskFromResult(response));

            var resource = new OrganizationResource(client.Object);

            // When, Then
            Assert.Throws<ArgumentException>(async () => await resource.PutAsync(request));
        }

        [Fact]
        public async void PostAsync_Called_BuildsUri()
        {
            // Given
            var request = new OrganizationRequest { Item = new Organization { Name = "blah blah" } };
            var resource = new OrganizationResource(client.Object);
            
            // When
            await resource.PostAsync(request);

            // Then
            client.Verify(b => b.BuildUri(It.IsAny<string>(), ""));
        }

        [Fact]
        public async void PostAsync_CalledWithItem_ReturnsReponse()
        {
            // Given
            var client = new Mock<IRestClient>();
            var response = new OrganizationResponse { Item = new Organization { Name = "blah blah" } };
            var request = new OrganizationRequest { Item = new Organization { Name = "blah blah" } };
            client.Setup(b => b.PostAsync<OrganizationResponse>(
                It.IsAny<Uri>(),
                request,
                "application/json",
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(TaskHelper.CreateTaskFromResult(response));

            var resource = new OrganizationResource(client.Object);

            // When
            var result = await resource.PostAsync(request);

            // Then
            Assert.Equal(result, response);
        }

        [Fact]
        public async void DeleteAsync_Called_CallsBuildUriWithFieldId()
        {
            // Given
            var client = new Mock<IRestClient>();
            client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://search"));
            var resource = new OrganizationResource(client.Object);

            // When
            await resource.DeleteAsync(321);

            // Then
            client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("321")), ""));
        }

        [Fact]
        public async void DeleteAsync_Called_CallsDeleteOnClient()
        {
            // Given
            var client = new Mock<IRestClient>();
            var response = new OrganizationResponse { Item = new Organization { Id = 1 } };
            client.Setup(b => b.GetAsync<OrganizationResponse>(
                It.IsAny<Uri>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(TaskHelper.CreateTaskFromResult(response));

            var resource = new OrganizationResource(client.Object);

            // When
            await resource.DeleteAsync(321);

            // Then
            client.Verify(c => c.DeleteAsync<object>(It.IsAny<Uri>(), It.IsAny<object>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
        }
    }
}
