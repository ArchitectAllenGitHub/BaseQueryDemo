using AllenDynamicExpressions;
using BaseQuery.Common;
using BaseQueryDemo.Entities;
using SqlSugar;
using System.Linq.Expressions;

namespace BaseQueryDemo.IRepository
{
    /// <summary>
    /// 基础仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="I"></typeparam>
    /// <typeparam name="A"></typeparam>
    /// <typeparam name="M"></typeparam>
    public class BaseRepository<T, I, A, M> : BaseDynamicExpression<T, I>, IRepository<T, I, A, M> where T : BaseEntity, new()
        where I : class, new()
    {
        protected readonly ISqlSugarClient _db;

        public BaseRepository(ISqlSugarClient db)
        {
            _db = db;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public async Task<int> InsertAsync(A input)
        {

            return await _db.Insertable<T>(input.MapTo<A, T>()).ExecuteCommandAsync();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public async Task<int> InsertAsync(IEnumerable<A> input)
        {
            return await _db.Insertable<T>(input.MapTo<A, T>()).ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public async Task<int> UpdateAsync(M input)
        {
            return await _db.Updateable<T>(input.MapTo<M, T>()).ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public async Task<int> UpdateAsync(IEnumerable<M> input)
        {
            return await _db.Updateable<T>(input.MapTo<M, T>()).ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(Expression<Func<T, bool>> input)
        {
            return await _db.Updateable<T>(input).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public async Task<int> DeleteAsync(IEnumerable<T> data)
        {
            return await _db.Deleteable<T>(data).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public async Task<int> DeleteAsync(T data)
        {
            return await _db.Deleteable<T>(data).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(Expression<Func<T, bool>> input)
        {
            return await _db.Deleteable<T>(input).ExecuteCommandAsync();
        }

        /// <summary>
        /// [单表]获取集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetEntityListAsync(I input)
        {
            return await this.GetSqlSugarExpression(input).ToListAsync();
        }

        /// <summary>
        /// [单表]获取单个
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<T> GetEntityAsync(I input)
        {
            return await this.GetSqlSugarExpression(input).FirstAsync();
        }

        /// <summary>
        /// [单表]获取分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetPageAsync(PageEntity<I> input)
        {
            var res = this.GetSqlSugarExpression(input.Data);

            foreach (var item in this.GetOrderBy(input.OrderByEntity))
                res.OrderBy(item.OrderByExpression, item.Ascending ? OrderByType.Asc : OrderByType.Desc);

            return await res.ToPageListAsync(input.PageIndex, input.PageSize, input.Total);
        }

        /// <summary>
        /// 获取SqlSugar的表达式目录树
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private ISugarQueryable<T> GetSqlSugarExpression(I input)
        {
            var res = GetExpression(input);
            return _db.Queryable<T>().WhereIF(res.Condition, res.Expression);
        }
    }
}