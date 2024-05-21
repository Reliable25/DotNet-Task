using Dot_NET_Task.Model;

namespace Dot_NET_Task.Data
{
    public interface ITypeRepository
    {
        Task<Question> CreateTypeAsync(Question type);
        Task<Question> GetByIdAsync(string id);
        Task<Question> UpdateTypeAsync(Question type);
        Task<Question> GetTypeAsync(string type);
    }
}
