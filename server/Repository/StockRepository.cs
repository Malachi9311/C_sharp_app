using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Data;
using server.Models;
using server.Interfaces;
using server.Dtos.Stock;
using Microsoft.EntityFrameworkCore;

namespace server.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stocks.ToListAsync();
        }
        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _context.Stocks.FirstOrDefaultAsync(x => x.Symbol == symbol);
        }
        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }
        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var existingModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingModel == null)
            {
                return null;
            }

            existingModel.Symbol = stockDto.Symbol;
            existingModel.CompanyName = stockDto.CompanyName;
            existingModel.Purchase = stockDto.Purchase;
            existingModel.LastDiv = stockDto.LastDiv;
            existingModel.Industry = stockDto.Industry;
            existingModel.MarketCap = stockDto.MarketCap;

            await _context.SaveChangesAsync();
            return existingModel;
        }
        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null)
            {
                return null;
            }
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }
    }
}