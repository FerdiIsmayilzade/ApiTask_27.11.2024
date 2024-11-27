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
    public class ColorRepository : BaseRepository<Color>, IColorRepository
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<Color> _dbSet;
        public ColorRepository(AppDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Color>();
        }

        public async Task<IEnumerable<Color>> GetAllWithProductAsync(params Expression<Func<Color, object>>[] includes)
        {
            IQueryable<Color> query = this._dbSet;
            foreach (Expression<Func<Color, object>> include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync().ConfigureAwait(false);
        }
    }
}
