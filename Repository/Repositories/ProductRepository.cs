using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<Product> _dbSet;

        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Product>();
        }

        public async Task<IEnumerable<Product>> GetAllWithCategoryAsync(params Expression<Func<Product, object>>[] includes)
        {
            IQueryable<Product> query = this._dbSet;
            foreach (Expression<Func<Product, object>> include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync().ConfigureAwait(false);
        }
    }
}
