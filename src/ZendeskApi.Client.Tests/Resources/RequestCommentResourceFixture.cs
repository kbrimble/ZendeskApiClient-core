﻿using System;
using System.Collections.Generic;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;
using Moq;
using Xunit;

namespace ZendeskApi.Client.Tests.Resources
{
    public class RequestCommentResourceFixture
    {
        [Fact]
        public async void GetAsync_Called_UrlIsCorrect()
        {
            //Given
            var client = new Mock<IRestClient>();
            var response = new TicketCommentListResponse();
            client.Setup(c => c.GetAsync<TicketCommentListResponse>(
                It.IsAny<Uri>(),
                It.IsAny<string>(),
                It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));

            var resource = new RequestCommentResource(client.Object);

            //When
            await resource.GetAsync(321, 123);

            //Then
            client.Verify(c => c.BuildUri(It.Is<string>(u => u.Contains("requests/123/comments/321")), It.IsAny<string>()));
        }

        [Fact]
        public async void GetAllAsync_CalledWithId_ReturnsListOfComments()
        {
            //Given
            var client = new Mock<IRestClient>();
            var response = new TicketCommentListResponse
            {
                Results = new List<TicketComment> { new TicketComment { Id = 123 } }
            };
            client.Setup(c => c.GetAsync<TicketCommentListResponse>(
                It.IsAny<Uri>(),
                It.IsAny<string>(),
                It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));

            var resource = new RequestCommentResource(client.Object);

            //When
            var result = await resource.GetAllAsync(123);

            //Then
            Assert.Equal(result, response);
        }

        [Fact]
        public async void GetAllAsync_Called_UrlIsCorrect()
        {
            //Given
            var client = new Mock<IRestClient>();
            var response = new TicketCommentListResponse();
            client.Setup(c => c.GetAsync<TicketCommentListResponse>(It.IsAny<Uri>(), It.IsAny<string>(), It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));
            var resource = new RequestCommentResource(client.Object);

            //When
            await resource.GetAllAsync(123);

            //Then
            client.Verify(c => c.BuildUri(It.Is<string>(u => u.Contains("requests/123/comments")), It.IsAny<string>()));
        }
    }
}
