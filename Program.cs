using EF_Project.Models;
using System.Linq;

namespace EF_Simple_Project
{
    internal class Program
    {
        static int currentAuthorId;
        static void Main(string[] args)
        {
             var context = new ProjectContext();

            #region هنخزن بيانات في جدول المؤلف 
            //context.Authors.Add(new Author
            //{
            //    Name = "Ahmed hossam",
            //    Age = 20,
            //    UserName = "ahmed",
            //    Password = "123",
            //    JoinDate = DateTime.Now
            //});

            //context.Authors.Add(new Author
            //{
            //    Name = "Sara Mohamed",
            //    Age = 25,
            //    UserName = "sara",
            //    Password = "456",
            //    JoinDate = DateTime.Now
            //}); 
            //context.SaveChanges();

            #endregion

            #region   هنخزن بيانات في جدول الانواع 
            //context.Categories.Add(new Category
            //{
            //    Name = "Sports",
            //    Description = "All about sports"
            //});

            //context.Categories.Add(new Category
            //{
            //    Name = "Technology",
            //    Description = "Latest in tech"
            //});

            //context.SaveChanges(); 
            #endregion

            #region   هنحزن بيانات في جدول الأخبار
            //context.News.Add(new News
            //{
            //    Title = "Match Result",
            //    Brief = "Team A vs Team B",
            //    Description = "Team A won the match 2-0",
            //    Date = DateTime.Now,
            //    Time = DateTime.Now.ToShortTimeString(),
            //    AuthorId = 1,      // Ahmed
            //    CategoryId = 1     // Sports
            //});

            //context.News.Add(new News
            //{
            //    Title = "New Smartphone Released",
            //    Brief = "Tech Giant launches new phone",
            //    Description = "Features include AI camera, 5G, and more.",
            //    Date = DateTime.Now,
            //    Time = DateTime.Now.ToShortTimeString(),
            //    AuthorId = 2,      // Sara
            //    CategoryId = 2     // Technology
            //});

            //context.SaveChanges(); 
            #endregion


            #region  صفحة تسجيل الدخول  

            Console.Write("Enter UserName: ");
            string userName = Console.ReadLine();

            Console.Write("Enter Password : ");
            string password = Console.ReadLine();

            var author = context.Authors.SingleOrDefault(a => a.UserName == userName && a.Password == password);

            if (author != null)
            {
                Console.WriteLine($"\nWelcome {author.Name}!");
                Console.WriteLine("=== Profile Info ===");
                Console.WriteLine($"ID: {author.Id}");
                Console.WriteLine($"Name: {author.Name}");
                Console.WriteLine($"Age: {author.Age}");
                Console.WriteLine($"Username: {author.UserName}");
                Console.WriteLine($"Join Date: {author.JoinDate}\n");

                currentAuthorId = author.Id;
            }
            else
            {
                Console.WriteLine("\nInvalid username or password!");
                return;
            }
            #endregion

            #region Menu
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n=== News Management ===");
                Console.WriteLine("1. Add News");
                Console.WriteLine("2. Delete News");
                Console.WriteLine("3. Show All News");
                Console.WriteLine("0. Exit");
                Console.Write("Choose option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddNews(context);
                        break;
                    case "2":
                        DeleteNews(context);
                        break;
                    case "3":
                        ShowNews(context);
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            }
        }
        #endregion

        #region فانكشن إضافة خبر جديد 

        static void AddNews(ProjectContext context)
        {
            Console.Write("Enter Title: ");
            string title = Console.ReadLine();

            Console.Write("Enter Brief: ");
            string brief = Console.ReadLine();

            Console.Write("Enter Description: ");
            string desc = Console.ReadLine();

            Console.Write("Enter CategoryId: ");
            int catId = int.Parse(Console.ReadLine());

            var categoryExists = context.Categories.Any(c => c.Id == catId);
            if (!categoryExists)
            {
                Console.WriteLine("Invalid CategoryId! ");
                return;
            }

            var news = new News
            {
                Title = title,
                Brief = brief,
                Description = desc,
                Date = DateTime.Now,
                Time = DateTime.Now.ToShortTimeString(),
                AuthorId = currentAuthorId,
                CategoryId = catId
            };

            context.News.Add(news);
            context.SaveChanges();
            Console.WriteLine("News added successfully!");
        }
        #endregion

        #region فانكشن حذف خبر
        static void DeleteNews(ProjectContext context)
        {
            Console.Write("Enter News Id to delete: ");
            int id = int.Parse(Console.ReadLine());

            var news = context.News.Find(id);

            if (news != null)
            {
                context.News.Remove(news);
                context.SaveChanges();
                Console.WriteLine("News deleted successfully!");
            }
            else
            {
                Console.WriteLine("News not found!");
            }
        }
        #endregion

        #region فانكشن عرض الأخبار
        static void ShowNews(ProjectContext context)
        {
            var allNews = context.News.ToList();

            Console.WriteLine("\n--- All News ---");
            foreach (var n in allNews)
            {
                Console.WriteLine($"[{n.Id}] {n.Title} ({n.Date.ToShortDateString()}) - CategoryId: {n.CategoryId}, AuthorId: {n.AuthorId}");
            }
        } 
        #endregion
    }
}
    
