namespace BaseQueryDemo.BaseController
{
    /// <summary>
    /// 返回类型
    /// </summary>
    public class Response : Response<string>
    {

    }


    /// <summary>
    /// 返回类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Response<T>
    {
        /// <summary>
        /// 提示消息
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// 业务编码
        /// </summary>
        public double Code { get; set; } = 200;

        /// <summary>
        /// 返回结果
        /// </summary>
        public T Result { get; set; }

        public Response()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="result"></param>
        public Response(double code, T result = default)
        {
            this.Code = code;
            this.Result = result;
        }
    }
}
