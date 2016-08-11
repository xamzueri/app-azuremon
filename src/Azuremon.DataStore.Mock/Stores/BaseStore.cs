using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azuremon.DataStore.Abstractions;
using Xamarin.Forms;

namespace Azuremon.DataStore.Mock.Stores
{
    public class BaseStore<T> : IBaseStore<T>
    {
        protected IStoreManager StoreManager => DependencyService.Get<IStoreManager>();

        public virtual Task InitializeStore()
        {
            return Task.FromResult(false);
        }

        public virtual Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> InsertAsync(T item)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> UpdateAsync(T item)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> RemoveAsync(T item)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> SyncAsync()
        {
            throw new NotImplementedException();
        }

        public virtual void DropTable()
        {
            
        }

        public string Identifier => "store";
    }
}
