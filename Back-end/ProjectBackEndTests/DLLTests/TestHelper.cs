using DLL;
using DLL.Entities;
using DLL.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBackEndTests.DLLTests
{
    public static class TestHelper
    {
        public static void SeedData(DataContext context)
        {
            User user1 = new User { Email = "TestEmail1@mail.com", NickName = "TestNickName1", Password = "qwe123" };
            context.Users.Add(user1);
            User user2 = new User { Email = "TestEmail2@mail.com", NickName = "TestNickName2", Password = "qwe123" };
            context.Users.Add(user2);
            Category category1 = new Category { Name = "TestCategoryName1" };
            Category category2 = new Category { Name = "TestCategoryName2" };
            Category category3 = new Category { Name = "TestCategoryName3" };
            context.Categories.Add(category1);
            context.Categories.Add(category2);
            context.Categories.Add(category3);
            context.Cards.Add(new Card
            {
                Title = "TestTitle1",
                Text = "Some special test text for testing dataBase, some special test text for testing dataBase, ",
                NumberOfLikes = 0,
                User = user1,
                Category = category1
            }); ;
            context.Cards.Add(new Card
            {
                Title = "TestTitle2",
                Text = "Some special test text for testing dataBase, some special test text for testing dataBase, ",
                NumberOfLikes = 0,
                User = user2,
                Category = category1
            });
            context.Cards.Add(new Card
            {
                Title = "TestTitle3",
                Text = "Some special test text for testing dataBase, some special test text for testing dataBase, ",
                NumberOfLikes = 0,
                User = user1,
                Category = category2
            });
            context.SaveChanges();
        }

    }
}
