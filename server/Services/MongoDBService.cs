using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using MongoDB.Bson;
using MongoDB.Driver;

using Server.Models;

namespace Server.Services
{
    public class MongoDBService
    {
        private IMongoCollection<TodoModel> TodoCollection { get; set; }

        public MongoDBService(string databaseName, string collectionName, string databaseUri)
        {
            var mongoClient = new MongoClient(databaseUri);
            var mongoDatabase = mongoClient.GetDatabase(databaseName);

            TodoCollection = mongoDatabase.GetCollection<TodoModel>(collectionName);
        }

        public async Task AddTodo(TodoModel todo) => await TodoCollection.InsertOneAsync(todo);

        public async Task<List<TodoModel>> GetAllTodos()
        {
            var todos = new List<TodoModel>();
            var allDocuments = await TodoCollection.FindAsync(new BsonDocument());
            await allDocuments.ForEachAsync(todo => todos.Add(todo));

            return todos;
        }
    }
}
