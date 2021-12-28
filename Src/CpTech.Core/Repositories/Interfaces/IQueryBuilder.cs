using System.Linq;
using System.Threading.Tasks;
using CpTech.Core.Dto;
using CpTech.Core.Entities;

namespace CpTech.Core.Repositories
{
    public interface IQueryBuilder<TEntity, TFilterDto>
        where TEntity : IEntity
        where TFilterDto : IFilterDto
    {
        Task<IQueryable<TEntity>> BuildQuery(IIdentity identity, TFilterDto filter);
    }
}