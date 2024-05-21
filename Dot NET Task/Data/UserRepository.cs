using Dot_NET_Task.Model;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
namespace Dot_NET_Task.Data
{
    public class UserRepository: IUserRepository
    {
        private readonly CosmosClient cosmosClient;
        private readonly IConfiguration configuration;
        private readonly Container _userContainer;
        public UserRepository(CosmosClient cosmosClient, IConfiguration configuration)
        {
            this.cosmosClient = cosmosClient;
            this.configuration = configuration;
            var databaseName = configuration["CosmosDbSettings:DatabaseName"];
            var userContainerName = "Users";
            _userContainer = cosmosClient.GetContainer(databaseName, userContainerName);
        }
     
        public async Task<PersonalInformation> CreateUserAsync(PersonalInformation personalInformation)
        {
            personalInformation.Id = Guid.NewGuid().ToString();
            var response = await _userContainer.CreateItemAsync(personalInformation);
            return response.Resource;
        }
    }
}
