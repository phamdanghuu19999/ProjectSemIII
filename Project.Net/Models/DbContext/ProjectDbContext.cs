namespace Project.Net.Models.DbContext
{
    using Project.Net.Models.DataModel;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ProjectDbContext : DbContext
    {
       
        public ProjectDbContext(): base("name=ProjectDbContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ProjectDbContext, Migrations.Configuration>("ProjectDbContext"));
        }

        public System.Data.Entity.DbSet<Project.Net.Models.DataModel.Product> Products { get; set; }

        public System.Data.Entity.DbSet<Project.Net.Models.DataModel.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<Project.Net.Models.DataModel.Supplier> Suppliers { get; set; }

        public System.Data.Entity.DbSet<Project.Net.Models.DataModel.Banner> Banners { get; set; }

        public System.Data.Entity.DbSet<Project.Net.Models.DataModel.AttributeType> AttributeTypes { get; set; }

        public System.Data.Entity.DbSet<Project.Net.Models.DataModel.Attributes> Attributes { get; set; }


        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Business> Businesses { get; set; }
        public virtual DbSet<GroupRoles> GroupRoles { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public System.Data.Entity.DbSet<Project.Net.Models.DataModel.Note> Notes { get; set; }
        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

  
}