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
    public class TicketResourceFixture
    {
        [Fact]
        public async void GetAsync_Called_CallsBuildUriWithFieldId()
        {
            // Given
            var client = new Mock<IRestClient>();
            client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://search"));
            var ticketResource = new TicketResource(client.Object);

            // When
            await ticketResource.GetAsync(321);

            // Then
            client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("321")), ""));
        }

        [Fact]
        public async void GetAsync_Called_ReturnsTicketResponse()
        {
            // Given
            var client = new Mock<IRestClient>();
            var response = new TicketResponse { Item = new Ticket { Id = 1 }};
            client.Setup(b => b.GetAsync<TicketResponse>(It.IsAny<Uri>(), It.IsAny<string>(), It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));
            var ticketResource = new TicketResource(client.Object);

            // When
            var result = await ticketResource.GetAsync(321);

            // Then
            Assert.Equal(result, response);
        }

        [Fact]
        public async void GetAllAsync_Called_CallsBuildUriWithFieldId()
        {
            // Given
            var client = new Mock<IRestClient>();
            client.Setup(b => b.BuildUri(It.IsAny<string>(), It.IsAny<string>())).Returns(new Uri("http://search"));
            var ticketResource = new TicketResource(client.Object);

            // When
            await ticketResource.GetAllAsync(new List<long> { 321, 456, 789 });

            // Then
            client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("/show_many")), It.IsAny<string>()));
        }

        [Fact]
        public async void GetAllAsync_Called_ReturnsTicketResponse()
        {
            // Given
            var client = new Mock<IRestClient>();
            var response = new TicketListResponse { Results = new List<Ticket> { new Ticket { Id = 1 } } };
            client.Setup(b => b.GetAsync<TicketListResponse>(It.IsAny<Uri>(), It.IsAny<string>(), It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));
            var ticketResource = new TicketResource(client.Object);

            // When
            var result = await ticketResource.GetAllAsync(new List<long> { 321, 456, 789 });

            // Then
            Assert.Equal(result, response);
        }

        [Fact]
        public async void PutAsync_Called_BuildsUri()
        {
            // Given
            var client = new Mock<IRestClient>();
            var request = new TicketRequest { Item = new Ticket { Subject = "blah blah", Id = 123 } };
            var ticketResource = new TicketResource(client.Object);

            // When
            await ticketResource.PutAsync(request);

            // Then
            client.Verify(b => b.BuildUri(It.IsAny<string>(), ""));
        }

        [Fact]
        public async void PutAsync_CalledWithTicket_ReturnsTicketReponse()
        {
            // Given
            var client = new Mock<IRestClient>();
            var response = new TicketResponse { Item = new Ticket { Subject = "blah blah" } };
            var request = new TicketRequest { Item = new Ticket { Subject = "blah blah", Id = 123 } };
            client.Setup(b => b.PutAsync<TicketResponse>(It.IsAny<Uri>(), request, "application/json", It.IsAny<string>(), It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));
            var ticketResource = new TicketResource(client.Object);

            // When
            var result = await ticketResource.PutAsync(request);

            // Then
            Assert.Equal(result, response);
        }

        [Fact]
        public void PutAsync_TicketHasNoId_ThrowsException()
        {
            // Given
            var client = new Mock<IRestClient>();
            var response = new TicketResponse { Item = new Ticket { Subject = "blah blah" } };
            var request = new TicketRequest { Item = new Ticket { Subject = "blah blah" } };
            client.Setup(b => b.PutAsync<TicketResponse>(It.IsAny<Uri>(), request, "application/json", It.IsAny<string>(), It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));
            var ticketResource = new TicketResource(client.Object);

            // When, Then
            Assert.Throws<ArgumentException>(async () => await ticketResource.PutAsync(request));
        }

        [Fact]
        public async void PostAsync_Called_BuildsUri()
        {
            // Given
            var client = new Mock<IRestClient>();
            var request = new TicketRequest { Item = new Ticket { Subject = "blah blah" } };
            var ticketResource = new TicketResource(client.Object);

            // When
            await ticketResource.PostAsync(request);

            // Then
            client.Verify(b => b.BuildUri(It.IsAny<string>(), ""));
        }

        [Fact]
        public async void PostAsync_CalledWithTicket_ReturnsTicketReponse()
        {
            // Given
            var client = new Mock<IRestClient>();
            var response = new TicketResponse { Item = new Ticket { Subject = "blah blah" } };
            var request = new TicketRequest { Item = new Ticket { Subject = "blah blah" } };
            client.Setup(b => b.PostAsync<TicketResponse>(It.IsAny<Uri>(), request, "application/json", It.IsAny<string>(), It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));
            var ticketResource = new TicketResource(client.Object);

            // When
            var result = await ticketResource.PostAsync(request);

            // Then
            Assert.Equal(result, response);
        }

        [Fact]
        public async void DeleteAsync_Called_CallsBuildUriWithFieldId()
        {
            // Given
            var client = new Mock<IRestClient>();
            client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://search"));
            var ticketResource = new TicketResource(client.Object);

            // When
            await ticketResource.DeleteAsync(321);

            // Then
            client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("321")), ""));
        }

        [Fact]
        public async void DeleteAsync_Called_CallsDeleteOnClient()
        {
            // Given
            var client = new Mock<IRestClient>();
            var response = new TicketResponse { Item = new Ticket { Id = 1 } };
            client.Setup(b => b.GetAsync<TicketResponse>(It.IsAny<Uri>(), It.IsAny<string>(), It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));
            var ticketResource = new TicketResource(client.Object);

            // When
            await ticketResource.DeleteAsync(321);

            // Then
            client.Verify(c => c.DeleteAsync<object>(It.IsAny<Uri>(), It.IsAny<object>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
        }
    }
}
