using Dot_NET_Task.Model;

namespace Dot_NET_Task.Data
{
    public interface IUserRepository
    {
        Task<PersonalInformation> CreateUserAsync(PersonalInformation personalInformation);
    }
}
