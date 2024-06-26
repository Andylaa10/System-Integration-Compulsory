﻿using AutoMapper;
using Messaging;
using Messaging.SharedMessages;
using PostService.Core.Entities;
using PostService.Core.Repositories.Interfaces;
using PostService.Core.Services.DTOs;
using PostService.Core.Services.Interfaces;

namespace PostService.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly MessageClient _messageClient;

        public PostService(IPostRepository postRepository, IMapper mapper, MessageClient messageClient)
        {
            _postRepository = postRepository ?? throw new ArgumentException("Post repository cannot be null");
            _mapper = mapper ?? throw new ArgumentException("Automapper cannot be null");
            _messageClient = messageClient ?? throw new ArgumentException("MessageClient cannot be null");
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _postRepository.GetPosts();
        }

        public async Task<Post> GetPostById(int postId)
        {
            if (postId < 1) throw new ArgumentException("Post ID cannot be 0 or null");

            return await _postRepository.GetPostById(postId);
        }

        public async Task<Post> AddPost(AddPostDTO dto)
        {
            if (dto.UserId <= 0) throw new ArgumentException("User ID cannot be 0 or null");
            
            var post = await _postRepository.AddPost(_mapper.Map<Post>(dto));
            await _messageClient.Send(new AddPostToTimelineIfPostIsCreated("Adding post to timeline", post.Id), "AddPostToTimelineIfPostIsCreated");
            return post;
        }

        public async Task UpdatePost(int postId, UpdatePostDTO post)
        {
            if (postId != post.Id)
                throw new ArgumentException("Id in the route must match the posts' id");
            if (postId < 1) throw new ArgumentException("Id cannot be less than 1");

            if (!await _postRepository.DoesPostExists(postId))
            {
                throw new KeyNotFoundException($"No such post with id of {postId}");
            }

            await _postRepository.UpdatePost(postId, _mapper.Map<Post>(post));
        }

        public async Task DeletePost(int postId)
        {
            if (postId < 1) throw new ArgumentException("Id cannot be less than 1");

            if (!await _postRepository.DoesPostExists(postId))
            {
                throw new KeyNotFoundException($"No such post with id of {postId}");
            }
            await _postRepository.DeletePost(postId);
            await _messageClient.Send(new DeleteCommentOnPostIfPostIsDeleted("Deleting comments on post", postId), "DeleteCommentsOnPostIfPostIsDeleted");

        }
    }
}
