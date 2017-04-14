using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;
using Moq;
using Xunit;

namespace ZendeskApi.Client.Tests.Resources
{
    public class OrganizationMembershipResourceFixture
    {
        [Fact]
        public async void GetAllByOrganizationAsync_Called_CallsBuildUriWithFieldIdAndType()
        {
            // Given
            var client = new Mock<IRestClient>();
            client.Setup(b => b.BuildUri(It.IsAny<string>(), It.IsAny<string>())).Returns(new Uri("http://search"));
            var organizationMembershipResource = new OrganizationMembershipResource(client.Object);

            // When
            await organizationMembershipResource.GetAllByOrganizationAsync(4321);

            // Then
            client.Verify(c => c.BuildUri(It.Is<string>(st => st.Contains("4321") && st.Contains("/organizations/")), ""));
        }

        [Fact]
        public async void GetAllByOrganizationAsync_Called_ReturnsOrganizationMembershipResponse()
        {
            // Given
            var client = new Mock<IRestClient>();
            var response = new OrganizationMembershipListResponse { Results = new List<OrganizationMembership> { new OrganizationMembership { Id = 1 } } };
            client.Setup(b => b.GetAsync<OrganizationMembershipListResponse>(
                It.IsAny<Uri>(), 
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(TaskHelper.CreateTaskFromResult(response));

            var organizationMembershipResource = new OrganizationMembershipResource(client.Object);

            // When
            var result = await organizationMembershipResource.GetAllByOrganizationAsync(4321);

            // Then
            Assert.Equals(result, response);
        }

        [Fact]
        public async void GetAllByUserAsync_Called_CallsBuildUriWithFieldIdAndType()
        {
            // Given
            var client = new Mock<IRestClient>();
            client.Setup(b => b.BuildUri(It.IsAny<string>(), It.IsAny<string>())).Returns(new Uri("http://search"));
            var organizationMembershipResource = new OrganizationMembershipResource(client.Object);

            // When
            await organizationMembershipResource.GetAllByUserAsync(4321);

            // Then
            client.Verify(c => c.BuildUri(It.Is<string>(st => st.Contains("4321") && st.Contains("/users/")), ""));
        }

        [Fact]
        public async void GetAllByUserAsync_Called_ReturnsOrganizationMembershipResponse()
        {
            // Given
            var client = new Mock<IRestClient>();
            var response = new OrganizationMembershipListResponse { Results = new List<OrganizationMembership> { new OrganizationMembership { Id = 1 } } };
            client.Setup(b => b.GetAsync<OrganizationMembershipListResponse>(
                It.IsAny<Uri>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(TaskHelper.CreateTaskFromResult(response));

            var organizationMembershipResource = new OrganizationMembershipResource(client.Object);

            // When
            var result = await organizationMembershipResource.GetAllByUserAsync(4321);

            // Then
            Assert.Equals(result, response);
        }

        [Fact]
        public void GetAllAsync_Called_CallsBuildUriWithFieldId()
        {
            // Given
            var client = new Mock<IRestClient>();
            client.Setup(b => b.BuildUri(It.IsAny<string>(), It.IsAny<string>())).Returns(new Uri("http://search"));
            var organizationMembershipResource = new OrganizationMembershipResource(client.Object);

            // When
            organizationMembershipResource.GetAllAsync(4321);

            // Then
            client.Verify(c => c.BuildUri(It.Is<string>(st => st.Contains("4321")),""));
        }

        [Fact]
        public async void PostAsync_Called_BuildsUriWithFieldUserId()
        {
            // Given
            var client = new Mock<IRestClient>();
            var request = new OrganizationMembershipRequest { Item = new OrganizationMembership { UserId = 1234 } };
            var organizationMembershipResource = new OrganizationMembershipResource(client.Object);

            // When
            await organizationMembershipResource.PostAsync(request);

            // Then
            client.Setup(b => b.BuildUri(It.Is<string>(s => s.Contains("1234")), ""));
        }

        [Fact]
        public async void PostAsync_CalledWithId_ReturnsReponseWithId()
        {
            // Given
            var client = new Mock<IRestClient>();
            var response = new OrganizationMembershipResponse { Item = new OrganizationMembership { Id = 123 } };
            var request = new OrganizationMembershipRequest { Item = new OrganizationMembership { Id = 123 } };
            client.Setup(b => b.PostAsync<OrganizationMembershipResponse>(It.IsAny<Uri>(), request, "application/json", It.IsAny<string>(), It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));
            var organizationMembershipResource = new OrganizationMembershipResource(client.Object);

            // When
            var result = await organizationMembershipResource.PostAsync(request);

            // Then
            Assert.Equals(result, response);
        }
    }
}
