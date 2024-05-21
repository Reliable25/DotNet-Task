using Dot_NET_Task.Data;
using Dot_NET_Task.DTO;
using Dot_NET_Task.Model;
using Microsoft.AspNetCore.Mvc;

namespace Dot_NET_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
       
        [HttpPost("createUser")]
        public async Task<ActionResult<CreateUserResponse>> Create([FromBody] CreateUserRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.PersonalInformation.FirstName) ||
                string.IsNullOrWhiteSpace(request.PersonalInformation.LastName) ||
                string.IsNullOrWhiteSpace(request.PersonalInformation.Email))
            {
                return BadRequest("FirstName, LastName, and Email are mandatory fields.");
            }

            if (!string.IsNullOrWhiteSpace(request.PersonalInformation.Phone))
            {
                if (!IsNumeric(request.PersonalInformation.Phone) )
                {
                    return BadRequest("Phone should only contain numeric characters");
                }
            }

            request.PersonalInformation.CustomQuestions = new List<Question>
            {
                new YesNoQuestion { Id = Guid.NewGuid().ToString(), Type = "YesNo", Answer = request.YesNoAnswer },
                new DateQuestion { Id = Guid.NewGuid().ToString(), Type = "Date"},
                new ParagraphQuestion { Id = Guid.NewGuid().ToString(), Type = "Paragraph", Answer = request.ParagraphAnswer },
                new NumericQuestion { Id = Guid.NewGuid().ToString(), Type = "NumericQuestion", Answer = request.NumericAnswer },
                new DropdownQuestion { Id = Guid.NewGuid().ToString(), Type = "Dropdown", Choice = request.DropdownOptions, Answer = request.DropdownAnswer },
                new MultipleChoiceQuestion { Id = Guid.NewGuid().ToString(), Type = "Multiple Choice", Choice = request.DropdownOptions, Answer = request.DropdownAnswer, MaxChoiceAllowed = request.MaxChoiceAllowed}
            };
            var yesNoQuestions = request.PersonalInformation.CustomQuestions.OfType<YesNoQuestion>().ToList();
            var dateQuestions = request.PersonalInformation.CustomQuestions.OfType<DateQuestion>().ToList();
            var paragraphQuestions = request.PersonalInformation.CustomQuestions.OfType<ParagraphQuestion>().ToList();
            var numericQuestions = request.PersonalInformation.CustomQuestions.OfType<NumericQuestion>().ToList();
            var dropdownQuestions = request.PersonalInformation.CustomQuestions.OfType<DropdownQuestion>().ToList();
            var multipleChoiceQuestions = request.PersonalInformation.CustomQuestions.OfType<MultipleChoiceQuestion>().ToList();

            var createdPersonalInfo = await _userRepository.CreateUserAsync(request.PersonalInformation);

            var response = new CreateUserResponse
            {
                PersonalInformation = createdPersonalInfo,
                YesNoQuestions = yesNoQuestions,
                DateQuestions = dateQuestions,
                ParagraphQuestions = paragraphQuestions,
                NumericQuestions = numericQuestions,
                DropdownQuestions = dropdownQuestions,
                MultipleChoiceQuestions = multipleChoiceQuestions
            };

            return Ok(response);
        }
        private bool IsNumeric(string value)
        {
            return long.TryParse(value, out _);
        }
    }
}
