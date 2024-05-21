using Dot_NET_Task.Model;

namespace Dot_NET_Task.DTO
{
    public class CreateUserResponse
    {
        public PersonalInformation PersonalInformation { get; set; }
        public List<YesNoQuestion> YesNoQuestions { get; set; }
        public List<DateQuestion> DateQuestions { get; set; }
        public List<ParagraphQuestion> ParagraphQuestions { get; set; }
        public List<NumericQuestion> NumericQuestions { get; set; }
        public List<DropdownQuestion> DropdownQuestions { get; set; }
        public List<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }
    }
}
