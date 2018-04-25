using momsManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace momsManagement.Interfaces
{
    public interface IChildrenRepository
    {
        Task<IEnumerable<Children>> GetAllChildren();
        Task<Children> GetChildren(string id);

        // add new Children document
        Task AddChildren(Children item);

        // remove a single document / Children
        Task<bool> RemoveChildren(string id);

        // update just a single document / Children
        Task<bool> UpdateChildren(string id, string name, DateTime birthday, string address, string parent1, string parent2);

        // demo interface - full document update
        Task<bool> UpdateChildrenDocument(string id, string name, DateTime birthday, string address, string parent1, string parent2);

        // should be used with high cautious, only in relation with demo setup
        Task<bool> RemoveAllChildren();

        // creates a sample index
        Task<string> CreateIndex();
    }
}
