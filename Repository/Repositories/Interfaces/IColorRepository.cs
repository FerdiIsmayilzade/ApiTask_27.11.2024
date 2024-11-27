using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IColorRepository : IBaseRepository<Color>
    {
        Task<IEnumerable<Color>> GetAllWithProductAsync(params Expression<Func<Color, object>>[] includes);

    }
}
