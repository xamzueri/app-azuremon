using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Azuremon.DataObjects;
using Microsoft.Azure.Mobile.Server.Tables;

namespace Azuremon.Backend.Models
{
    public class AzuremonContext
        : DbContext
    {
        private const string connectionStringName = "Name=MS_TableConnectionString";

        public AzuremonContext() : base(connectionStringName)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                    "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));
        }


        public DbSet<Favorite> Favorites { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Pokemon> Pokemons { get; set; }
    }
}