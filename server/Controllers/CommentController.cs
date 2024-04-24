using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Dtos.Comment;
using server.Interfaces;
using server.Mappers;
using server.Helpers;

namespace server.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ICommentRepository _commRepo;
        private readonly IStockRepository _stockRepo;
        public CommentController(ApplicationDBContext context, ICommentRepository commRepo, IStockRepository stockRepo)
        {
            _context = context;
            _commRepo = commRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] CommentQueryObject queryObject)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comments = await _commRepo.GetAllAsync(queryObject);
            var commentDto = comments.Select(x => x.ToCommentDto());
            
            return Ok(commentDto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commRepo.GetById(id);
            if (comment == null)
            {
                return NotFound();
            }
            
            return Ok(comment.ToCommentDto());
        }

        [HttpPost]
        [Route("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentDto commentDto)
        {
            if (!await _stockRepo.StockExists(stockId))
            {
                return BadRequest("Stock does not exist");
            }
            var commentModel = commentDto.ToCommentFromCreate(stockId);
            await _commRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateCommentDto updateCommentDto)
        {
            var comment = await _commRepo.UpdateAsync(id, updateCommentDto.ToCommentFromUpdate(id));

            if (comment == null)
            {
                return NotFound("Comment not found");
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var comment = await _commRepo.DeleteAsync(id);

            if (comment == null)
            {
                return NotFound("Comment does not Exist");
            }
            return Ok(comment.ToCommentDto());
        }
    }
}