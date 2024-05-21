using Newtonsoft.Json;

namespace Dot_NET_Task.Model
{
    public class PersonalInformation
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("nationality")]
        public string Nationality { get; set; }
        [JsonProperty("currentResidence")]
        public string CurrentResidence { get; set; }
        [JsonProperty("idNumber")]
        public int IDNumber { get; set; }
        [JsonProperty("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }
        [JsonProperty("isPhoneInternal")]
        public bool IsPhoneInternal { get; set; }
        [JsonProperty("isPhoneHidden")]
        public bool IsPhoneHidden { get; set; }
        [JsonProperty("isNationalityInternal")]
        public bool IsNationalityInternal { get; set; }
        [JsonProperty("isNationalHidden")]
        public bool IsNationalityHidden { get; set; }
        [JsonProperty("isCurrentResidenceInternal")]
        public bool IsCurrentResidenceInternal { get; set; }
        [JsonProperty("isCurrentResidenceHidden")]
        public bool IsCurrentResidenceHidden { get; set; }
        [JsonProperty("isIDNumberInternal")]
        public bool IsIDNumberInternal { get; set; }
        [JsonProperty("isIDNumberHidden")]
        public bool IsIDNumberHidden { get; set; }
        [JsonProperty("isDateOfBirthInternal")]
        public bool IsDateOfBirthInternal { get; set; }
        [JsonProperty("isDateOfBirthHidden")]
        public bool IsDateOfBirthHidden { get; set; }
        [JsonProperty("isGenderInternal")]
        public bool IsGenderInternal { get; set; }
        [JsonProperty("isGenderHidden")]
        public bool IsGenderHidden { get; set; }
        [JsonProperty("customQuestions")]
        public List<Question> CustomQuestions { get; set; }
    }
}

