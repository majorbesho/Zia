using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Zia.Models;

namespace Zia.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Family> Families { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ShopingCart> ShopingCarts { get; set; }
        
        public DbSet<Uislide> Uislides { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<VideoUploader> VideoUploaders { get; set; }

        public DbSet<OrderDetails> OrderDetailses { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Team> Teams { get; set; }
        
        public DbSet<Clinet> Clinets { get; set; }  





    }
}
