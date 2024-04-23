using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Data;
using server.Models;
using server.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace server.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;

        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }
        public async Task<Comment?> GetById(int id)
        {
            return await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }
        public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
            var existingModel = await _context.Comments.FindAsync(id);
            if (existingModel == null)
            {
                return null;
            }

            existingModel.Title = commentModel.Title;
            existingModel.Content = commentModel.Content;
            
            await _context.SaveChangesAsync();
            return existingModel;

        }
        public async Task<Comment?> DeleteAsync(int id)
        {
            var existingModel = await _context.Comments.FindAsync(id);
            if (existingModel == null)
            {
                return null;
            }

            _context.Remove(existingModel);
            await _context.SaveChangesAsync();
            return existingModel;
        }
    }
}