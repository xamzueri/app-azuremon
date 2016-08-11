using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azuremon.DataObjects;
using Azuremon.DataStore.Abstractions;

namespace Azuremon.DataStore.Azure.Stores
{
    public class CategoryStore : BaseStore<Category>, ICategoryStore
    {
        public override string Identifier => nameof(Category);
        
    }
}
