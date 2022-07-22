﻿using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TripleSix.Core.Entities;
using TripleSix.Core.Exceptions;
using TripleSix.Core.Helpers;
using TripleSix.Core.Persistences;
using TripleSix.Core.Types;

namespace TripleSix.Core.Services
{
    /// <summary>
    /// Service xử lý strong entity.
    /// </summary>
    /// <typeparam name="TDbDataContext">Loại db data context xử lý.</typeparam>
    /// <typeparam name="TEntity">Loại entity xử lý.</typeparam>
    public abstract class StrongService<TDbDataContext, TEntity> : BaseService<TDbDataContext, TEntity>,
        IStrongService<TEntity>
        where TDbDataContext : IDbDataContext
        where TEntity : class, IStrongEntity
    {
        protected StrongService(TDbDataContext db)
            : base(db)
        {
        }

        /// <summary>
        /// Khởi tạo entity kèm code tự phát sinh.
        /// </summary>
        /// <param name="entity"><inheritdoc/></param>
        /// <param name="cancellationToken"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public override sealed Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken = default)
        {
            return Create(entity, true, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual Task<string?> GenerateCode(TEntity? entity = default, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<string?>(null);
        }

        /// <inheritdoc/>
        public virtual async Task<TEntity> Create(TEntity entity, bool generateCode, CancellationToken cancellationToken = default)
        {
            // tự phát sinh mã nếu không được nhập
            if (generateCode && entity.Code.IsNullOrWhiteSpace())
            {
                entity.Code = await GenerateCode(entity);
                if (entity.Code.IsNullOrWhiteSpace())
                    entity.Code = null;
            }

            return await base.Create(entity, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task Update(Guid id, bool includeDeleted, Action<TEntity> updateMethod, CancellationToken cancellationToken = default)
        {
            var entity = await GetById(id, includeDeleted, cancellationToken);
            await Update(entity, updateMethod, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task UpdateWithMapper(Guid id, bool includeDeleted, IDataDto input, CancellationToken cancellationToken = default)
        {
            var entity = await GetById(id, includeDeleted, cancellationToken);
            await UpdateWithMapper(entity, input, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task HardDelete(Guid id, bool includeDeleted, CancellationToken cancellationToken = default)
        {
            var entity = await GetById(id, includeDeleted, cancellationToken);
            await HardDelete(entity, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task SoftDelete(TEntity entity, CancellationToken cancellationToken = default)
        {
            using var activity = StartTraceMethodActivity();

            entity.IsDeleted = true;
            Db.Set<TEntity>().Update(entity);

            await Db.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task SoftDelete(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetById(id, true, cancellationToken);
            await SoftDelete(entity, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task Restore(TEntity entity, CancellationToken cancellationToken = default)
        {
            using var activity = StartTraceMethodActivity();

            entity.IsDeleted = false;
            Db.Set<TEntity>().Update(entity);

            await Db.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task Restore(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetById(id, true, cancellationToken);
            await Restore(entity, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<bool> Any(bool includeDeleted, CancellationToken cancellationToken = default)
        {
            var query = Db.Set<TEntity>()
                .WhereIf(includeDeleted == false, x => !x.IsDeleted);

            return Any(query, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<long> Count(bool includeDeleted, CancellationToken cancellationToken = default)
        {
            var query = Db.Set<TEntity>()
                .WhereIf(includeDeleted == false, x => !x.IsDeleted);

            return Count(query, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<TResult?> GetOrDefaultById<TResult>(Guid id, bool includeDeleted, CancellationToken cancellationToken = default)
            where TResult : class
        {
            using var activity = StartTraceMethodActivity();

            var query = Db.Set<TEntity>()
                .WhereIf(includeDeleted == false, x => !x.IsDeleted)
                .Where(x => x.Id == id);

            return typeof(TResult) == typeof(TEntity) ?
                await query.SingleOrDefaultAsync(cancellationToken) as TResult :
                await query.ProjectTo<TResult>(Mapper.ConfigurationProvider).SingleOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public Task<TEntity?> GetOrDefaultById(Guid id, bool includeDeleted, CancellationToken cancellationToken = default)
        {
            return GetOrDefaultById<TEntity>(id, includeDeleted, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<TResult> GetById<TResult>(Guid id, bool includeDeleted, CancellationToken cancellationToken = default)
            where TResult : class
        {
            var data = await GetOrDefaultById<TResult>(id, includeDeleted, cancellationToken);
            if (data == null) throw new EntityNotFoundException(typeof(TEntity));

            return data;
        }

        /// <inheritdoc/>
        public Task<TEntity> GetById(Guid id, bool includeDeleted, CancellationToken cancellationToken = default)
        {
            return GetById<TEntity>(id, includeDeleted, cancellationToken);
        }
    }
}
