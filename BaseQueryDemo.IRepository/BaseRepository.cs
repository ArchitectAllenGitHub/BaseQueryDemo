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
    /// <typeparam name="T">实体参数</typeparam>
    /// <typeparam name="C">新增参数</typeparam>
    /// <typeparam name="U">修改参数</typeparam>
    /// <typeparam name="R">读取参数</typeparam>
    public class BaseRepository<T, C, U, R> : BaseDynamicExpression<T, R>, IRepository<T, C, U, R>
        where T : BaseEntity, new()
        where R : class, new()
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
        public async Task<int> InsertAsync(C input)
        {

            return await _db.Insertable<T>(input.MapTo<C, T>()).ExecuteCommandAsync();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public async Task<int> InsertAsync(IEnumerable<C> input)
        {
            return await _db.Insertable<T>(input.MapTo<C, T>()).ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public async Task<int> UpdateAsync(U input)
        {
            return await _db.Updateable<T>(input.MapTo<U, T>()).ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public async Task<int> UpdateAsync(IEnumerable<U> input)
        {
            return await _db.Updateable<T>(input.MapTo<U, T>()).ExecuteCommandAsync();
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
        public async Task<IEnumerable<T>> GetEntityListAsync(R input)
        {
            return await this.GetSqlSugarExpression(input).ToListAsync();
        }

        /// <summary>
        /// [单表]获取单个
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<T> GetEntityAsync(R input)
        {
            return await this.GetSqlSugarExpression(input).FirstAsync();
        }

        /// <summary>
        /// [单表]获取分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetPageEntityListAsync(PageEntity<R> input)
        {
            var res = this.GetSqlSugarExpression(input.Data);

            if (!string.IsNullOrEmpty(input.OrderField))
                res.OrderBy(input.OrderField);

            return await res.ToPageListAsync(input.PageIndex, input.PageSize, input.Total);
        }

        /// <summary>
        /// 获取SqlSugar的表达式目录树
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private ISugarQueryable<T> GetSqlSugarExpression(R input)
        {
            var res = GetExpression(input);
            return _db.Queryable<T>().WhereIF(res.Condition, res.Expression);
        }
    }
}