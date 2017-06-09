using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;
using MyBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Controllers
{
    [Route("posts")]
    public class PostsController : Controller
    {
        private BlogDbContext _dbContext;

        public PostsController(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet, Route("")]
        public IActionResult GetPosts()
        {
            var posts = _dbContext.Posts.ToList();

            return Ok(posts);
        }

        [HttpGet, Route("{id:int}")]
        public IActionResult GetPost(int id)
        {
            var post = _dbContext.Posts.FirstOrDefault(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpPost, Route("")]
        public IActionResult CreatePost([FromBody]CreatePostModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = new Post
            {
                Title = model.Title,
                Content = model.Content,
                CreatedAt = DateTime.Now,
                UserId = 1
            };

            _dbContext.Posts.Add(post);

            _dbContext.SaveChanges();

            return Ok(post);
        }

        [HttpPut, Route("{id:int}")]
        public IActionResult UpdatePost(int id, [FromBody]UpdatePostModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var post = _dbContext.Posts.FirstOrDefault(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(model.Title))
            {
                post.Title = model.Title;
            }

            if (!string.IsNullOrEmpty(model.Content))
            {
                post.Content = model.Content;
            }

            post.UpdatedAt = DateTime.Now;

            _dbContext.Update(post);

            _dbContext.SaveChanges();

            return Ok(post);
        }

        [HttpDelete, Route("{id:int}")]
        public IActionResult DeletePost(int id)
        {
            var post = _dbContext.Posts.FirstOrDefault(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            _dbContext.Remove(post);

            _dbContext.SaveChanges();

            return Ok();
        }


        //comments controller

        [HttpGet, Route("{id}")]
        public IActionResult GetComments(int id)
        {
            var post = _dbContext.Posts.FirstOrDefault(p => p.Id == id);

            var comments = _dbContext.Comments.ToList();
            
            return Ok(comments);
        }


        [HttpGet, Route("{id:int}/comments/{commentId:int}")]
        public IActionResult GetComment(int id, int commentId)
        {
            var comment = _dbContext.Comments.FirstOrDefault(p => p.Id == commentId && p.PostId == id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }


       // [HttpPost, Route("{id:int}/comments")]
        [HttpPost, Route("comments/{id:int}")]
        public IActionResult CreateComment(int id, [FromBody] CreateCommentModel model)
        {
            var post = _dbContext.Comments.FirstOrDefault(p => p.Id == id);

            //if (post == null)
            //{
            //    return NotFound();

            //}
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = new Comment
            {
                //Title = model.Title,
                //Content = model.Content,
                //CreatedAt = DateTime.Now,
                //UserId = 1
                
                PostId = id,
                UserId = 1,
                Content = model.Content,
                CreatedAt = DateTime.Now,

            };

            _dbContext.Comments.Add(comment);

            _dbContext.SaveChanges();

            return Ok(comment);

        }

    }
}
