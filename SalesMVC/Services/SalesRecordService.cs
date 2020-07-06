using Microsoft.EntityFrameworkCore;
using SalesMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesMVC.Services
{
    public class SalesRecordService
    {
        private readonly SalesMVCContext _context;

        public SalesRecordService(SalesMVCContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from sales in _context.SalesRecord select sales;
            //result = _context.SalesRecord.Select(sales => sales);

            if (minDate.HasValue)
                result = result.Where(x => x.Date >= minDate.Value);

            if (maxDate.HasValue)
                result = result.Where(x => x.Date >= maxDate.Value);

            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }
    }
}
