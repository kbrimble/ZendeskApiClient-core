﻿using System;
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
    public class UserIdentityResourceFixture
    {
        [Fact]
        public async void GetAllAsync_Called_CallsBuildUriWithFieldId()
        {
            // Given
            var client = new Mock<IRestClient>();
            client.Setup(b => b.BuildUri(It.IsAny<string>(), It.IsAny<string>())).Returns(new Uri("http://search"));
            var userIdentityResource = new UserIdentityResource(client.Object);

            // When
            await userIdentityResource.GetAllAsync(4321);

            // Then
            client.Verify(c => c.BuildUri(It.Is<string>(st => st.Contains("4321")), ""));
        }

        [Fact]
        public async void GetAllAsync_Called_ReturnsUserIdentityResponse()
        {
            // Given
            var client = new Mock<IRestClient>();
            var response = new UserIdentityListResponse { Results = new List<UserIdentity> { new UserIdentity { Id = 1 } } };
            client.Setup(b => b.GetAsync<UserIdentityListResponse>(It.IsAny<Uri>(), It.IsAny<string>(), It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));
            var userIdentityResource = new UserIdentityResource(client.Object);

            // When
            var result = await userIdentityResource.GetAllAsync(4321);

            // Then
            Assert.Equal(result, response);
        }

        [Fact]
        public async void PostAsync_Called_BuildsUriWithFieldUserId()
        {
            // Given
            var client = new Mock<IRestClient>();
            var request = new UserIdentityRequest { Item = new UserIdentity { Name = "email", UserId = 1234 } };
            var userIdentityResource = new UserIdentityResource(client.Object);

            // When
            await userIdentityResource.PostAsync(request);

            // Then
            client.Verify(b => b.BuildUri(It.Is<string>(s => s.Contains("1234")), ""));
        }

        [Fact]
        public async void PostAsync_CalledWithUser_ReturnsUserReponse()
        {
            // Given
            var client = new Mock<IRestClient>();
            var response = new UserIdentityResponse { Item = new UserIdentity { Name = "email" } };
            var request = new UserIdentityRequest { Item = new UserIdentity { Name = "email" } };
            client.Setup(b => b.PostAsync<UserIdentityResponse>(It.IsAny<Uri>(), request, "application/json", It.IsAny<string>(), It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));
            var userIdentityResource = new UserIdentityResource(client.Object);

            // When
            var result = await userIdentityResource.PostAsync(request);

            // Then
            Assert.Equal(result, response);
        }

        [Fact]
        public async void PutAsync_Called_BuildsUriWithFieldUserId()
        {
            // Given
            var client = new Mock<IRestClient>();
            var request = new UserIdentityRequest { Item = new UserIdentity { Name = "email", UserId = 1234, Id = 123 } };
            var userIdentityResource = new UserIdentityResource(client.Object);

            // When
            await userIdentityResource.PutAsync(request);

            // Then
            client.Verify(b => b.BuildUri(It.Is<string>(s => s.Contains("1234")), ""));
        }

        [Fact]
        public async void PutAsync_CalledWithUser_ReturnsUserReponse()
        {
            // Given
            var client = new Mock<IRestClient>();
            var response = new UserIdentityResponse { Item = new UserIdentity { Name = "email", Id = 123 } };
            var request = new UserIdentityRequest { Item = new UserIdentity { Name = "email", Id = 123 } };
            client.Setup(b => b.PutAsync<UserIdentityResponse>(It.IsAny<Uri>(), request, "application/json", It.IsAny<string>(), It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));
            var userIdentityResource = new UserIdentityResource(client.Object);

            // When
            var result = await userIdentityResource.PutAsync(request);

            // Then
            Assert.Equal(result, response);
        }
    }
}
