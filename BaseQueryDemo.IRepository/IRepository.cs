using BaseQueryDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseQueryDemo.IRepository
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="T">实体</typeparam>
    /// <typeparam name="C">新增参数</typeparam>
    /// <typeparam name="U">修改参数</typeparam>
    /// <typeparam name="R">查询参数</typeparam>
    public interface IRepository<T, C, U, R> where T : BaseEntity, new()
        where R : class, new()
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<int> InsertAsync(C data);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<int> InsertAsync(IEnumerable<C> data);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(U data);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(IEnumerable<U> data);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(T data);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(IEnumerable<T> data);

        /// <summary>
        /// [单表]获取实体
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<T> GetEntityAsync(R input);

        /// <summary>
        /// [单表]获取实体集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetEntityListAsync(R input);

        /// <summary>
        /// [单表]获取分页实体集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetPageEntityListAsync(PageEntity<R> input);
    }
}
