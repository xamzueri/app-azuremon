using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azuremon.DataObjects;
using Azuremon.DataStore.Abstractions;

namespace Azuremon.DataStore.Mock.Stores
{
    public class CategoryStore : BaseStore<Category>, ICategoryStore
    {
        private readonly HashSet<Category> _categorieDb = new HashSet<Category>();

        public override Task InitializeStore()
        {
            if (_categorieDb.Count > 0) return Task.FromResult(true);

            _categorieDb.Add(new Category
            {
                Id = Guid.NewGuid().ToString(),
                Color = "#78c850",
                Name = "Grass"
            });
            _categorieDb.Add(new Category
            {
                Id = Guid.NewGuid().ToString(),
                Color = "#a040a0",
                Name = "Poison"
            });
            _categorieDb.Add(new Category
            {
                Id = Guid.NewGuid().ToString(),
                Color = "#6890f0",
                Name = "Water"
            });
            _categorieDb.Add(new Category
            {
                Id = Guid.NewGuid().ToString(),
                Color = "#f08030",
                Name = "Fire"
            });
            _categorieDb.Add(new Category
            {
                Id = Guid.NewGuid().ToString(),
                Color = "#f8d030",
                Name = "Electric"
            });


            return Task.FromResult(true);
        }

        public override Task<IEnumerable<Category>> GetItemsAsync(bool forceRefresh = false)
        {
            return Task.FromResult(_categorieDb.AsEnumerable());
        }

        public override Task<Category> GetItemAsync(string id)
        {
            return Task.FromResult(_categorieDb.SingleOrDefault(t => t.Id == id));
        }
    }
}

