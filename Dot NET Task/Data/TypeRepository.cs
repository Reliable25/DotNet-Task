using Dot_NET_Task.Model;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System.Collections.Generic;

namespace Dot_NET_Task.Data
{
    public class TypeRepository : ITypeRepository
    {
        private readonly CosmosClient cosmosClient;
        private readonly IConfiguration configuration;
        private readonly Container _typeContainer;
        public TypeRepository(CosmosClient cosmosClient, IConfiguration configuration)
        {
            this.cosmosClient = cosmosClient;
            this.configuration = configuration;
            var databaseName = configuration["CosmosDbSettings:DatabaseName"];
            var typeContainerName = "QuestionTypes";
            _typeContainer = cosmosClient.GetContainer(databaseName, typeContainerName);
        }

        public async Task<Question> CreateTypeAsync(Question questionType)
        {
            var response = await _typeContainer.CreateItemAsync(questionType);
            return response.Resource;
        }

        public async Task<Question> GetByIdAsync(string id)
        {
            var query = _typeContainer.GetItemLinqQueryable<Question>()
               .Where(t => t.Id == id)
               .Take(1)
               .ToQueryDefinition();
            var sqlQuery = query.QueryText; //Retrieve the SQL Query here
            var response = await _typeContainer.GetItemQueryIterator<Question>(query).ReadNextAsync();
            return response.FirstOrDefault();
        }

        public async Task<Question> GetTypeAsync(string type)
        {
            var query = _typeContainer.GetItemLinqQueryable<Question>()
               .Where(t => t.Type == type)
               .Take(1)
               .ToQueryDefinition();
            var sqlQuery = query.QueryText; //Retrieve the SQL Query here
            var response = await _typeContainer.GetItemQueryIterator<Question>(query).ReadNextAsync();
            return response.FirstOrDefault();
        }


        public async Task<Question> UpdateTypeAsync(Question type)
        {
            var response = await _typeContainer.ReplaceItemAsync(type, type.Id);
            return response.Resource;
        }
    }
}
