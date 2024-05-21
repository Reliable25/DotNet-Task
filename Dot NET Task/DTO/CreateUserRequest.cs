using Dot_NET_Task.Model;

namespace Dot_NET_Task.DTO
{
    public class CreateUserRequest
    {
        public PersonalInformation PersonalInformation { get; set; }
        public bool YesNoAnswer { get; set; }
        public DateTime DateAnswer { get; set; }
        public string ParagraphAnswer { get; set; }
        public int NumericAnswer { get; set; }
        public string DropdownAnswer { get; set; }
        public string[] DropdownOptions { get; set; }
        public string MultipleChoiceAnswer { get; set; }
        public string[] MultipleChoiceOptions { get; set; }
        public int MaxChoiceAllowed { get; set; }
    }
}
