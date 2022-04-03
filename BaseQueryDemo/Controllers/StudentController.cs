using BaseQueryDemo.BaseController;
using BaseQueryDemo.Entities;
using BaseQueryDemo.IRepository;
using BaseQueryDemo.Model.Input;
using BaseQueryDemo.Model.Response;
using Microsoft.AspNetCore.Mvc;

namespace BaseQueryDemo.Controllers
{
    public class StudentController : BaseController<StudentEntity, StudentAddInput, StudentModifyInput, StudentQueryInput>
    {

        private readonly ILogger<StudentController> _logger;
        private readonly IStudentRepository _studentRepository;

        public StudentController(ILogger<StudentController> logger, IStudentRepository studentRepository) : base(studentRepository)
        {
            _logger = logger;
            _studentRepository = studentRepository;
        }

        [HttpPost]
        public async Task<Response<IEnumerable<StudentEntity>>> GetListAsync(StudentQueryInput input)
        {
            return Success(await _studentRepository.GetListAsync(input));
        }

        [HttpPost]
        public async Task<Response<IEnumerable<StudentEntity>>> GetPageListAsync(PageEntity<StudentQueryInput> input)
        {
            return Success(await _studentRepository.GetPageListAsync(input));
        }

        [HttpPost]
        public async Task<Response<IEnumerable<StudentResponse>>> GetResponsePageListAsync(PageEntity<StudentQueryInput> input)
        {
            return Success(await _studentRepository.GetResponsePageListAsync(input));
        }
    }
}