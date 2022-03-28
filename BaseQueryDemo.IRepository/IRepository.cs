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
    public interface IRepository<T, I, A, M> where T : BaseEntity, new()
        where I : class, new()
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<int> InsertAsync(A data);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<int> InsertAsync(IEnumerable<A> data);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(M data);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(IEnumerable<M> data);

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
        Task<T> GetEntityAsync(I input);

        /// <summary>
        /// [单表]获取实体集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetEntityListAsync(I input);

        /// <summary>
        /// [单表]获取分页实体集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetPageAsync(PageEntity<I> input);
    }
}
