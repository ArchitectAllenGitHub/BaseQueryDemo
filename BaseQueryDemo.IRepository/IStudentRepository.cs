using BaseQueryDemo.Entities;
using BaseQueryDemo.Model.Input;
using BaseQueryDemo.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseQueryDemo.IRepository
{
    /// <summary>
    /// 学生抽象仓储
    /// </summary>
    public interface IStudentRepository : IRepository<StudentEntity, StudentAddInput, StudentModifyInput, StudentQueryInput>
    {
        /// <summary>
        /// 多表关联
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<StudentEntity>> GetListAsync(StudentQueryInput input);

        /// <summary>
        /// 多表关联-返回学生
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<StudentEntity>> GetPageListAsync(PageEntity<StudentQueryInput> input);

        /// <summary>
        /// 多表关联-返回自定义
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<StudentResponse>> GetResponsePageListAsync(PageEntity<StudentQueryInput> input);
    }
}
