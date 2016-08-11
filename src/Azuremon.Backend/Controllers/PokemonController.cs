using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Azuremon.Backend.Helpers;
using Azuremon.Backend.Models;
using Azuremon.DataObjects;
using Microsoft.Azure.Mobile.Server;

namespace Azuremon.Backend.Controllers
{
    public class PokemonController : TableController<Pokemon>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            AzuremonContext context = new AzuremonContext();
            DomainManager = new EntityDomainManager<Pokemon>(context, Request, true);
        }

        [QueryableExpand("Categories")]
        [EnableQuery(MaxTop = 500)]
        public IQueryable<Pokemon> GetAllCategory()
        {
            return Query();
        }

        [QueryableExpand("Categories")]
        public SingleResult<Pokemon> GetCategory(string id)
        {
            return Lookup(id);
        }

        [Authorize]
        public Task<Pokemon> PatchCategory(string id, Delta<Pokemon> patch)
        {
            return UpdateAsync(id, patch);
        }

        [Authorize]
        public async Task<IHttpActionResult> PostCategory(Pokemon item)
        {
            Pokemon current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        [Authorize]
        public Task DeleteCategory(string id)
        {
            return DeleteAsync(id);
        }

    }
}