using AutoMapper;
using AutoPostOffice.API.Data.Repositories;
using AutoPostOffice.API.Models;
using LoggerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AutoPostOffice.API.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostsController : ControllerBase
    {
        private readonly ILoggerManager logger;
        private readonly IPostRepository postRepository;
        private readonly IMapper mapper;

        public PostsController(ILoggerManager logger, IPostRepository postRepository, IMapper mapper)
        {
            this.logger = logger;
            this.postRepository = postRepository;
            this.mapper = mapper;
        }

        [HttpGet()]
        public IActionResult GetPosts()
        {
            try
            {
                var postsFromRepo = postRepository.GetAllPosts();
                if (postsFromRepo == null)
                {
                    return NotFound();
                }
                IEnumerable<PostModel> models = mapper.Map<IEnumerable<PostModel>>(postsFromRepo);
                return Ok(models);
            }
            catch (Exception ex)
            {
                logger.LogError($"Message: {ex.Message}");
                logger.LogError($"StackTrace: {ex.StackTrace}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }            
        }

        [HttpGet("{postNumber}")]
        public IActionResult GetPost(string postNumber)
        {
            try
            {
                var postFromRepo = postRepository.GetPost(postNumber);

                if (postFromRepo == null)
                {
                    return NotFound();
                }
                return Ok(postFromRepo);
            }
            catch (Exception ex)
            {
                logger.LogError($"Message: {ex.Message}");
                logger.LogError($"StackTrace: {ex.StackTrace}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }            
        }
    }
}
