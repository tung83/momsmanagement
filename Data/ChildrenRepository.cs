using Microsoft.Extensions.Options;
using momsManagement.Interfaces;
using momsManagement.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace momsManagement.Data
{
    public class ChildrenRepository : IChildrenRepository
    {
        private readonly DataContext _context = null;

        public ChildrenRepository(IOptions<Settings> settings)
        {
            _context = new DataContext(settings);
        }

        public async Task<IEnumerable<Children>> GetAllChildren()
        {
            try
            {
                return await _context.ChildrenCollection.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // query after internal or internal id
        //
        public async Task<Children> GetChildren(string id)
        {
            try
            {
                ObjectId internalId = GetInternalId(id);
                return await _context.ChildrenCollection
                                .Find(Children => Children.Id == id || Children._id == internalId)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        private ObjectId GetInternalId(string id)
        {
            ObjectId internalId;
            if (!ObjectId.TryParse(id, out internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }

        public async Task AddChildren(Children item)
        {
            try
            {
                await _context.ChildrenCollection.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> RemoveChildren(string id)
        {
            try
            {
                DeleteResult actionResult = await _context.ChildrenCollection.DeleteOneAsync(
                     Builders<Children>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdateChildren(string id, string name, DateTime birthday, string address, string parent1, string parent2)
        {
            var filter = Builders<Children>.Filter.Eq(s => s.Id, id);
            var update = Builders<Children>.Update
                            .Set(s => s.Name, name)
                            .Set(s => s.Birthday, birthday)
                            .Set(s => s.Address, address)
                            .Set(s => s.Parent1, parent1)
                            .Set(s => s.Parent2, parent2)
                            .CurrentDate(s => s.UpdatedOn);

            try
            {
                UpdateResult actionResult = await _context.ChildrenCollection.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdateChildren(string id, Children item)
        {
            try
            {
                ReplaceOneResult actionResult = await _context.ChildrenCollection
                                                .ReplaceOneAsync(n => n.Id.Equals(id)
                                                                , item
                                                                , new UpdateOptions { IsUpsert = true });
                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // Demo function - full document update
        public async Task<bool> UpdateChildrenDocument(string id, string name, DateTime birthday, string address, string parent1, string parent2)
        {
            var item = await GetChildren(id) ?? new Children();
            item.Name = name;
            item.Birthday = birthday;
            item.Address = address;
            item.Parent1 = parent1;
            item.Parent2 = parent2;
            item.UpdatedOn = DateTime.Now;
            return await UpdateChildren(id, item);
        }

        public async Task<bool> RemoveAllChildren()
        {
            try
            {
                DeleteResult actionResult = await _context.ChildrenCollection.DeleteManyAsync(new BsonDocument());

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // it creates a compound index (first using UserId, and then Body)
        // MongoDb automatically detects if the index already exists - in this case it just returns the index details
        public async Task<string> CreateIndex()
        {
            try
            {
                return await _context.ChildrenCollection.Indexes
                                           .CreateOneAsync(Builders<Children>
                                                                .IndexKeys
                                                                .Ascending(item => item.Id)
                                                                .Ascending(item => item.Name));
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}