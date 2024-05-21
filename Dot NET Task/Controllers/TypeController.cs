using Dot_NET_Task.Data;
using Dot_NET_Task.DTO;
using Dot_NET_Task.Model;
using Microsoft.AspNetCore.Mvc;

namespace Dot_NET_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : Controller
    {
        private readonly ITypeRepository _typeRepository;
        public TypeController(ITypeRepository typeRepository)
        {
            this._typeRepository = typeRepository;
        }
        [HttpPost]
        public async Task<ActionResult<Question>> CreateType(QuestionDTO type)
        {
            var existingType = await _typeRepository.GetTypeAsync(type.Type);
            if (existingType != null)
            {
                return Conflict("Type already exists.");
            }
            
            var questionType = new Question
            {
                Id = Guid.NewGuid().ToString(),
                Type = type.Type
            };
            var createdTask = await _typeRepository.CreateTypeAsync(questionType);
            return Ok(createdTask);
        }
        [HttpPut]
        public async Task<ActionResult<Question>> UpdateType(Question type)
        {
            var existingTask = await _typeRepository.GetByIdAsync(type.Id);
            if (existingTask == null)
            {
                return NotFound();
            }
            type.Id = existingTask.Id;
            var updatedTask = await _typeRepository.UpdateTypeAsync(type);
            return Ok(updatedTask);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetType(string type)
        {
            var questionType = await _typeRepository.GetTypeAsync(type);
            if (questionType == null )
            {
                return NotFound();
            }
            return Ok(questionType);
        }
    }
}
