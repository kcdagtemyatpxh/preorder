﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sample.Common.Dto;
using Sample.Data.DataContexts;
using Sample.Data.Entities;
using TripleSix.Core.Dto;
using TripleSix.Core.Helpers;
using TripleSix.Core.Repositories;

namespace Sample.Data.Repositories
{
    public class PermissionGroupRepository : ModelRepository<PermissionGroupEntity>,
        IQueryBuilder<PermissionGroupEntity, PermissionGroupAdminDto.Filter>
    {
        public PermissionGroupRepository(DataContext context)
            : base(context)
        {
        }

        public async Task<IQueryable<PermissionGroupEntity>> BuildQuery(IIdentity identity, PermissionGroupAdminDto.Filter filter)
        {
            var query = await BuildQuery(identity, filter as ModelFilterDto);

            if (filter.Search.IsNotNullOrWhiteSpace())
            {
                query = query.WhereOrs(
                x => EF.Functions.Like(x.Code, $"%{filter.Search}%"),
                x => EF.Functions.Like(x.Name, $"%{filter.Search}%"));
            }

            return query;
        }
    }
}
