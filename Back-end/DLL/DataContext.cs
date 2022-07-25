using DLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace DLL
{
    /// <summary>
    /// Data context for working data base.
    /// </summary>
    public class DataContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Like> Likes { get; set; }
        /// <summary>
        /// Constructor for creating context, based on options in parameters. 
        /// Method log warning information to console.
        /// </summary>
        /// <param name="options">Parameter for database options</param>
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) {}

        /// <summary>
        /// Method create model contections with entities, using fluent api.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Like>()
            .HasOne(p => p.Card)
            .WithMany(t => t.Likes)
            .OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<Like>()
            .HasOne(p => p.User)
            .WithMany(t => t.Likes)
            .OnDelete(DeleteBehavior.ClientCascade);
        }

        /// <summary>
        /// Create simple data for testing db.
        /// </summary>
        /// <param name="context">context for data</param>
        private void addSeedData(DataContext context)
        {
            User user1 = new User { Email = "TestEmail1@mail.com", NickName = "TestNickName1", Password = "qwe123", Role = "admin",RegistrationDate = DateTime.Now };
            User user2 = new User { Email = "TestEmail2@mail.com", NickName = "TestNickName2", Password = "qwe123", Role = "user", RegistrationDate = DateTime.Now };
            User user3 = new User { Email = "TestEmail3@mail.com", NickName = "TestNickName3", Password = "qwe123", Role = "user", RegistrationDate = DateTime.Now };
            User user4 = new User { Email = "TestEmail4@mail.com", NickName = "TestNickName4", Password = "qwe123", Role = "user", RegistrationDate = DateTime.Now };
            User user5 = new User { Email = "TestEmail5@mail.com", NickName = "TestNickName5", Password = "qwe123", Role = "user", RegistrationDate = DateTime.Now };
            User user6 = new User { Email = "TestEmail6@mail.com", NickName = "TestNickName6", Password = "qwe123", Role = "user", RegistrationDate = DateTime.Now };

            context.Users.AddRange(user1, user2, user3, user4, user5, user6);
            Category category1 = new Category { Name = "TestCategoryName1" };
            Category category2 = new Category { Name = "TestCategoryName2" };
            Category category3 = new Category { Name = "TestCategoryName3" };
            Category category4 = new Category { Name = "TestCategoryName4" };

            context.Categories.AddRange(category1, category2, category3, category4);

            Card card1 = new Card
            {
                Title = "TestTitle1",
                Text = "Some special test text for testing dataBase, some special test text for testing dataBase, ",
                User = user1,
                Category = category1,
                CreationDate = DateTime.Now
            };
            Card card2 = new Card
            {
                Title = "TestTitle2",
                Text = "Some special test text for testing dataBase, some special test text for testing dataBase, ",
                User = user2,
                Category = category2,
                CreationDate = DateTime.Now
            };
            Card card3 = new Card
            {
                Title = "TestTitle3",
                Text = "Some special test text for testing dataBase, some special test text for testing dataBase, ",
                User = user3,
                Category = category3,
                CreationDate = DateTime.Now
            };
            Card card4 = new Card
            {
                Title = "TestTitle4",
                Text = "Some special test text for testing dataBase, some special test text for testing dataBase, ",
                User = user4,
                Category = category4,
                CreationDate = DateTime.Now
            };
            Card card5 = new Card
            {
                Title = "TestTitle5",
                Text = "Some special test text for testing dataBase, some special test text for testing dataBase, ",
                User = user5,
                Category = category1,
                CreationDate = DateTime.Now
            };
            Card card6 = new Card
            {
                Title = "TestTitle6",
                Text = "Some special test text for testing dataBase, some special test text for testing dataBase, ",
                User = user1,
                Category = category2,
                CreationDate = DateTime.Now
            };
            context.Cards.AddRange(card1, card2, card3, card4, card5, card6);

            Like like1 = new Like()
            {
                IsDislike = false,
                User = user1,
                Card = card1
            };
            Like like2 = new Like()
            {
                IsDislike = true,
                User = user2,
                Card = card1
            };
            Like like3 = new Like()
            {
                IsDislike = false,
                User = user3,
                Card = card1
            };
            Like like4 = new Like()
            {
                IsDislike = false,
                User = user4,
                Card = card1
            };

            context.Likes.AddRange(like1, like2, like3, like4);
            context.SaveChanges();
        }
    }


}
