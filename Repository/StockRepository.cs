using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using api.Data;
using Microsoft.EntityFrameworkCore;
using api.Dtos.Stock;
using api.Dtos.Comments;
using api.Controllers;
using api.Helpers;




namespace api.Repository
{
    public class StockRepository : IStockRepository
    {

        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context=context;
        }
        public async Task<List<Stock>>GetAllAsync()
        {
            return await _context.Stocks.Include(c => c.Comments).ToListAsync();
        }
        public async Task<List<Stock>>GetAllAsync(QueryObject query)
        {
            var stocks= _context.Stocks.Include(c => c.Comments).AsQueryable();


            if(!String.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks=stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }

            if(!String.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks=stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }
            if(!String.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy.Equals("Symbol",StringComparison.OrdinalIgnoreCase))
                {
                    stocks=query.IsDescending ? stocks.OrderByDescending(s =>s.Symbol) : stocks.OrderBy(s =>s.Symbol);
                }
            }

            var SkipNumber=(query.PageNumber -1)*query.PageSize;

            return await stocks.Skip(SkipNumber).Take(query.PageSize).ToListAsync();
        }
        
        public async Task<Stock?>GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id==id);

        }

        public async Task<Stock>CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;

        }
        public async Task<bool>StockExists(int id)
        {
            return await _context.Stocks.AnyAsync(s => s.Id == id);
        }

        public async Task<Stock?>UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var existingStock= await _context.Stocks.FirstOrDefaultAsync(X => X.Id==id);

            if(existingStock==null)
            {
                return null;
            }

            existingStock.Symbol=stockDto.Symbol;
            existingStock.CompanyName=stockDto.CompanyName;
            existingStock.Purchase=stockDto.Purchase;
            existingStock.LastDiv=stockDto.LastDiv;
            existingStock.Industry=stockDto.Industry;
            existingStock.MarketCap=stockDto.MarketCap;

            await _context.SaveChangesAsync();
            return existingStock;
        }

        public async Task<Stock?>DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(X => X.Id==id);

            if(stockModel== null)
            {
                return null;
            }
            _context.Stocks.Remove(stockModel);

            await _context.SaveChangesAsync();
            return stockModel;


        }
    }
}