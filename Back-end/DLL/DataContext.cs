using System;
using System.Collections.Generic;
using System.Text;
using DLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DLL
{
    public class DataContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            if (Database.EnsureCreated())
            {
                addSeedData(this);
            }
        }
        private void addSeedData(DataContext context)
        {
            User user1 = new User { Email = "TestEmail1@mail.com", NickName = "TestNickName1", Password = "qwe123", Role = "admin" };
            User user2 = new User { Email = "TestEmail2@mail.com", NickName = "TestNickName2", Password = "qwe123", Role = "user" };
            User user3 = new User { Email = "TestEmail3@mail.com", NickName = "TestNickName3", Password = "qwe123", Role = "user" };
            User user4 = new User { Email = "TestEmail4@mail.com", NickName = "TestNickName4", Password = "qwe123", Role = "user" };
            User user5 = new User { Email = "TestEmail5@mail.com", NickName = "TestNickName5", Password = "qwe123", Role = "user" };
            User user6 = new User { Email = "TestEmail6@mail.com", NickName = "TestNickName6", Password = "qwe123", Role = "user" };
            
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
                NumberOfLikes = 0,
                User = user1,
                Category = category1
            };
            Card card2 = new Card
            {
                Title = "TestTitle2",
                Text = "Some special test text for testing dataBase, some special test text for testing dataBase, ",
                NumberOfLikes = 0,
                User = user2,
                Category = category2
            };
            Card card3 = new Card
            {
                Title = "TestTitle3",
                Text = "Some special test text for testing dataBase, some special test text for testing dataBase, ",
                NumberOfLikes = 0,
                User = user3,
                Category = category3
            };
            Card card4 = new Card
            {
                Title = "TestTitle4",
                Text = "Some special test text for testing dataBase, some special test text for testing dataBase, ",
                NumberOfLikes = 0,
                User = user4,
                Category = category4
            };
            Card card5 = new Card
            {
                Title = "TestTitle5",
                Text = "Some special test text for testing dataBase, some special test text for testing dataBase, ",
                NumberOfLikes = 0,
                User = user5,
                Category = category1
            };
            Card card6 = new Card
            {
                Title = "TestTitle6",
                Text = "Some special test text for testing dataBase, some special test text for testing dataBase, ",
                NumberOfLikes = 0,
                User = user1,
                Category = category2
            };

            context.Cards.AddRange(card1, card2, card3, card4, card5, card6);
            context.SaveChanges();
        }
    }


}
