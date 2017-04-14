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
    public class UserResourceFixture
    {
        [Fact]
        public async void GetAsync_Called_CallsBuildUriWithFieldId()
        {
            // Given
            var client = new Mock<IRestClient>();
            client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("4321")))).Returns(new Uri("http://search"));
            var userResource = new UserResource(client.Object);

            // When
            await userResource.GetAsync(4321);

            // Then
            client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("4321")), ""));
        }

        [Fact]
        public async void GetAsync_Called_ReturnsUserResponse()
        {
            // Given
            var client = new Mock<IRestClient>();
            var response = new UserResponse { Item = new User { Id = 1 } };
            client.Setup(b => b.GetAsync<UserResponse>(It.IsAny<Uri>(), It.IsAny<string>(), It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));
            var userResource = new UserResource(client.Object);

            // When
            var result = await userResource.GetAsync(4321);

            // Then
            Assert.Equal(result, response);
        }

        [Fact]
        public async void GetAllAsync_Called_CallsBuildUriWithFieldId()
        {
            // Given
            var client = new Mock<IRestClient>();
            client.Setup(b => b.BuildUri(It.IsAny<string>(), It.IsAny<string>())).Returns(new Uri("http://search"));
            var userResource = new UserResource(client.Object);

            // When
            await userResource.GetAllAsync(new List<long> { 4321, 3456, 6789 });

            // Then
            client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("/show_many")), It.Is<string>(st => st.Contains("4321,3456,6789"))));
        }

        [Fact]
        public async void GetAllAsync_Called_ReturnsUserResponse()
        {
            // Given
            var client = new Mock<IRestClient>();
            var response = new UserListResponse { Results = new List<User> { new User { Id = 1 } } };
            client.Setup(b => b.GetAsync<UserListResponse>(It.IsAny<Uri>(), It.IsAny<string>(), It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));
            var userResource = new UserResource(client.Object);

            // When
            var result = await userResource.GetAllAsync(new List<long> { 4321, 3456, 6789 });

            // Then
            Assert.Equal(result, response);
        }

        [Fact]
        public async void PostAsync_Called_BuildsUri()
        {
            // Given
            var client = new Mock<IRestClient>();
            var request = new UserRequest { Item = new User { Name = "Owner Name" } };
            var userResource = new UserResource(client.Object);

            // When
            await userResource.PostAsync(request);

            // Then
            client.Verify(b => b.BuildUri(It.IsAny<string>(), ""));
        }

        [Fact]
        public async void PostAsync_CalledWithUser_ReturnsUserReponse()
        {
            // Given
            var client = new Mock<IRestClient>();
            var response = new UserResponse { Item = new User { Name = "Owner Name" } };
            var request = new UserRequest { Item = new User { Name = "Owner Name" } };
            client.Setup(b => b.PostAsync<UserResponse>(It.IsAny<Uri>(), request, "application/json", It.IsAny<string>(), It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));
            var userResource = new UserResource(client.Object);

            // When
            var result = await userResource.PostAsync(request);

            // Then
            Assert.Equal(result, response);
        }

        [Fact]
        public async void PutAsync_Called_BuildsUri()
        {
            // Given
            var client = new Mock<IRestClient>();
            var request = new UserRequest { Item = new User { Name = "Owner Name", Id = 123 } };
            var userResource = new UserResource(client.Object);

            // When
            await userResource.PutAsync(request);

            // Then
            client.Verify(b => b.BuildUri(It.IsAny<string>(), ""));
        }

        [Fact]
        public async void PutAsync_CalledWithUser_ReturnsUserReponse()
        {
            // Given
            var client = new Mock<IRestClient>();
            var response = new UserResponse { Item = new User { Name = "Owner Name" } };
            var request = new UserRequest { Item = new User { Name = "Owner Name", Id = 123 } };
            client.Setup(b => b.PutAsync<UserResponse>(It.IsAny<Uri>(), request, "application/json", It.IsAny<string>(), It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));
            var userResource = new UserResource(client.Object);

            // When
            var result = await userResource.PutAsync(request);

            // Then
            Assert.Equal(result, response);
        }
    }
}
