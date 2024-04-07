using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PostService.Controllers;
using PostService.Core.Entities;
using PostService.Core.Services.DTOs;
using PostService.Core.Services.Interfaces;

namespace PostService.Service.Test;

public class TestPostServiceAPI
{
    [Fact]  
    public async Task TestCreatePost_ReturnsOkayAndPost()  
    {  
        // Arrange    
        var testPost = new Post  
        {  
            Id = 1,  
            UserId = 1,  
            Content = "This is a test post",  
            CreatedAt = DateTime.UtcNow,  
        };  
        
        var dtoPost = new AddPostDTO  
        {  
            UserId = 1,  
            Content = "This is a test post",  
        };
  
        var mockService = new Mock<IPostService>();  
        mockService.Setup(service => service.AddPost(It.IsAny<AddPostDTO>()))  
            .ReturnsAsync(testPost);
  
        var claims = new List<Claim> { new Claim("UserId", testPost.UserId.ToString()) };  
        var identity = new ClaimsIdentity(claims, "Bearer");  
        var user = new ClaimsPrincipal(identity);  
  
        var controller = new PostController(mockService.Object);  
  
        // Act    
        var result = await controller.AddPost(dtoPost);  
  
        // Assert    
        var objectResult = Assert.IsType<ObjectResult>(result);
        var postResult = Assert.IsType<Post>(objectResult.Value);
        Assert.Equal(201, objectResult.StatusCode);
        Assert.Equal(testPost, postResult);
    }  
    
    [Fact]  
    public async Task TestUpdatePost_ReturnsOkay()  
    {  
        // Arrange  
        var postId = 1;  
        var updatePostDto = new UpdatePostDTO  
        {  
            Content = "Updated content",  
        };  
  
        var mockService = new Mock<IPostService>();  
        mockService.Setup(service => service.UpdatePost(postId, updatePostDto)).Returns(Task.CompletedTask);  
  
        var claims = new List<Claim> { new Claim("UserId", postId.ToString()) };  
        var identity = new ClaimsIdentity(claims, "Bearer");  
        var user = new ClaimsPrincipal(identity);

        var controller = new PostController(mockService.Object);
  
        // Act  
        var result = await controller.UpdatePost(postId, updatePostDto);  
  
        // Assert  
        var okResult = Assert.IsType<OkResult>(result);  
        Assert.Equal(200, okResult.StatusCode);  
    }  
    
    [Fact]  
    public async Task TestGetPosts_ReturnsListOfPosts()  
    {  
        // Arrange  
        var posts = new List<Post>  
        {  
            new Post  
            {  
                Id = 1,  
                UserId = 1,  
                Content = "Test content 1",  
                CreatedAt = DateTime.UtcNow,  
            },  
            new Post  
            {  
                Id = 2,  
                UserId = 2,  
                Content = "Test content 2",  
                CreatedAt = DateTime.UtcNow,  
            },  
        };  
  
        var mockService = new Mock<IPostService>();  
        mockService.Setup(service => service.GetPosts()).ReturnsAsync(posts);  
  
        var controller = new PostController(mockService.Object);  
  
        // Act  
        var result = await controller.GetPosts();  
  
        // Assert  
        var okResult = Assert.IsType<OkObjectResult>(result);  
        var returnedPosts = Assert.IsType<List<Post>>(okResult.Value);  
        Assert.Equal(posts.Count, returnedPosts.Count);  
        for (int i = 0; i < posts.Count; i++)  
        {  
            Assert.Equal(posts[i].Id, returnedPosts[i].Id);  
            Assert.Equal(posts[i].UserId, returnedPosts[i].UserId);  
            Assert.Equal(posts[i].Content, returnedPosts[i].Content);  
        }  
    }  

    
    [Fact]  
    public async Task TestGetPostById_ReturnsPost()  
    {  
        // Arrange  
        var postId = 1;  
        var post = new Post  
        {  
            Id = postId,  
            UserId = 1,  
            Content = "Test content",  
            CreatedAt = DateTime.UtcNow,  
        };  
  
        var mockService = new Mock<IPostService>();  
        mockService.Setup(service => service.GetPostById(postId)).ReturnsAsync(post);  
  
        var controller = new PostController(mockService.Object);  
  
        // Act  
        var result = await controller.GetPostById(postId);  
  
        // Assert  
        var okResult = Assert.IsType<OkObjectResult>(result);  
        var returnedPost = Assert.IsType<Post>(okResult.Value);  
        Assert.Equal(post.Id, returnedPost.Id);  
        Assert.Equal(post.UserId, returnedPost.UserId);  
        Assert.Equal(post.Content, returnedPost.Content);  
    }  
    
    
    [Fact]  
    public async Task TestDeletePost_ReturnsOkay()  
    {  
        // Arrange    
        var postId = 1;
        
        var mockService = new Mock<IPostService>();  
        mockService.Setup(service => service.DeletePost(postId)).Returns(Task.CompletedTask);
        
        var claims = new List<Claim> { new Claim("UserId", postId.ToString()) };  
        var identity = new ClaimsIdentity(claims, "Bearer");  
        var user = new ClaimsPrincipal(identity);  
  
        var controller = new PostController(mockService.Object);  
  
        // Act    
        var result = await controller.DeletePost(postId);  
  
        // Assert    
        var objectResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, objectResult.StatusCode);
    }  

}