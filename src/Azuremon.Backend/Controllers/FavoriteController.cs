using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Azuremon.Backend.Extensions;
using Azuremon.Backend.Models;
using Azuremon.DataObjects;
using Microsoft.Azure.Mobile.Server;

namespace Azuremon.Backend.Controllers
{
    public class FavoriteController : TableController<Favorite>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            AzuremonContext context = new AzuremonContext();
            DomainManager = new EntityDomainManager<Favorite>(context, Request, true);

        }

        public async Task<IQueryable<Favorite>> GetAllFavorite()
        {
            var items = Query();
            var id = await User.UniversalUserId(Request);

            var final = items.Where(favorite => favorite.UniversalId == id);

            return final;
        }

        [Authorize]
        public SingleResult<Favorite> GetFavorite(string id)
        {
            return Lookup(id);
        }

        [Authorize]
        public Task<Favorite> PatchFavorite(string id, Delta<Favorite> patch)
        {
            return UpdateAsync(id, patch);
        }

        [Authorize]
        public async Task<IHttpActionResult> PostFavorite(Favorite item)
        {
            item.UniversalId = await User.UniversalUserId(Request);

            if (item.UniversalId == Guid.Empty.ToString())
                return BadRequest();

            var current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        [Authorize]
        public Task DeleteFavorite(string id)
        {
            return DeleteAsync(id);
        }

    }
}