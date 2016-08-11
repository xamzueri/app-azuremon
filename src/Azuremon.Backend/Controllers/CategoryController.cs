using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Azuremon.Backend.Models;
using Azuremon.DataObjects;
using Microsoft.Azure.Mobile.Server;

namespace Azuremon.Backend.Controllers
{
    public class CategoryController : TableController<Category>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            AzuremonContext context = new AzuremonContext();
            DomainManager = new EntityDomainManager<Category>(context, Request, true);
        }


        public IQueryable<Category> GetAllCategory()
        {
            return Query();
        }

        public SingleResult<Category> GetCategory(string id)
        {
            return Lookup(id);
        }

        [Authorize]
        public Task<Category> PatchCategory(string id, Delta<Category> patch)
        {
            return UpdateAsync(id, patch);
        }

        [Authorize]
        public async Task<IHttpActionResult> PostCategory(Category item)
        {
            Category current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        [Authorize]
        public Task DeleteCategory(string id)
        {
            return DeleteAsync(id);
        }

    }
}