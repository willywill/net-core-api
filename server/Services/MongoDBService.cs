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

        /// <summary>
        /// Adds a todo to the database.
        /// </summary>
        /// <param name="todo"></param>
        /// <returns></returns>
        public async Task AddTodo(TodoModel todo) => await TodoCollection.InsertOneAsync(todo);

        /// <summary>
        /// Retrieves a single todo that matches the parameter.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<TodoModel>> GetTodo(string id)
        {
            var doc = await TodoCollection.Find(todo => todo.TodoId == id).ToListAsync();
            return doc; 
        }

        /// <summary>
        /// Deletes a single todo from the database, that matches the parameter.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteTodo(string id) => await TodoCollection.DeleteOneAsync(todo => todo.TodoId == id);

        /// <summary>
        /// Retrieves all todos in the database.
        /// </summary>
        /// <returns></returns>
        public async Task<List<TodoModel>> GetAllTodos()
        {
            var todos = new List<TodoModel>();
            var allDocuments = await TodoCollection.FindAsync(new BsonDocument());
            await allDocuments.ForEachAsync(todo => todos.Add(todo));

            return todos;
        }
    }
}
