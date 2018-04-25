using Microsoft.AspNetCore.Mvc;
using momsManagement.Infrastructure;
using momsManagement.Interfaces;
using momsManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace momsManagement.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ChildrenController : Controller
    {
        private readonly IChildrenRepository _childrenRepository;

        public ChildrenController(IChildrenRepository childrenRepository)
        {
            _childrenRepository = childrenRepository;
        }

        [NoCache]
        [HttpGet]
        public async Task<IEnumerable<Children>> Get()
        {
            return await _childrenRepository.GetAllChildren();
        }

        // GET api/Children/5
        [HttpGet("{id}")]
        public async Task<Children> Get(string id)
        {
            return await _childrenRepository.GetChildren(id) ?? new Children();
        }

        // POST api/Children
        [HttpPost]
        public void Post([FromBody] ChildrenParam newChildren)
        {
            _childrenRepository.AddChildren(new Children
            {
                Id = newChildren.Id,
                Name = newChildren.Name,
                Birthday = newChildren.Birthday,
                Address = newChildren.Address,
                Parent1 = newChildren.Parent1,
                Parent2 = newChildren.Parent2,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,

            });
        }

        // PUT api/Children/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]ChildrenParam childrenParam)
        {
            _childrenRepository.UpdateChildrenDocument(id, childrenParam.Name, childrenParam.Birthday, childrenParam.Address, 
                childrenParam.Parent1, childrenParam.Parent2);
        }

        // DELETE api/Children/23243423
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _childrenRepository.RemoveChildren(id);
        }
    }
}

