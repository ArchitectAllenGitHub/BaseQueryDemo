using BaseQueryDemo.Entities;
using BaseQueryDemo.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BaseQueryDemo.BaseController
{
    /// <summary>
    /// 基础控制器
    /// </summary>
    /// <typeparam name="T">实体参数</typeparam>
    /// <typeparam name="C">新增参数</typeparam>
    /// <typeparam name="U">修改参数</typeparam>
    /// <typeparam name="R">查询参数</typeparam>
    [ApiController]
    [Route("[controller]/[action]")]
    public class BaseController<T, C, U, R> : ControllerBase where T : BaseEntity, new()
        where R : class, new()
    {
        private readonly IRepository<T, C, U, R> _repository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="repository"></param>
        public BaseController(IRepository<T, C, U, R> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut(Name = "Add")]
        public async Task<Response> AddAsync(C input)
        {
            await _repository.InsertAsync(input);

            return Success();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPatch(Name = "Update")]
        public async Task<Response> UpdateAsync(U input)
        {
            await _repository.UpdateAsync(input);

            return Success();
        }

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost(Name = "GetEntity")]
        public async Task<Response<T>> GetEntityAsync(R input)
        {
            return Success(await _repository.GetEntityAsync(input));
        }

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost(Name = "GetEntityList")]
        public async Task<Response<IEnumerable<T>>> GetEntityListAsync(R input)
        {
            return Success(await _repository.GetEntityListAsync(input));
        }

        /// <summary>
        /// 获取实体分页集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response<IEnumerable<T>>> GetPageEntityListAsync(PageEntity<R> input)
        {
            return Success(await _repository.GetPageEntityListAsync(input));
        }

        /// <summary>
        /// 先随便弄一下,根据自己的业务需求变更
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public Response Success()
        {
            return new Response();
        }

        /// <summary>
        /// 先随便弄一下,根据自己的业务需求变更
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public Response<S> Success<S>(S data)
        {
            return new Response<S>(200, data);
        }
    }
}
