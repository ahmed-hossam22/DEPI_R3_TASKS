using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using static LINQtoObject.SampleData;

namespace LINQtoObject
{
 
    internal class Program
    {
        static void Main(string[] args)
        {
            #region 1-Display book title and its ISBN.
            //var res1 = SampleData.Books.Select(a => new { a.Title, a.Isbn });
            //foreach (var item in res1)
            //{
            //    Console.WriteLine(item);
            //}
            #endregion

            #region 2-Display the first 3 books with price more than 25.
            //var res2 = SampleData.Books.Take(3).Where(a => a.Price > 25).Select(a => new { a.Title, a.Price });
            //foreach (var item in res2)
            //{
            //    Console.WriteLine(item);
            //}
            #endregion

            #region  3-Display Book title along with its publisher name (query operator&&query expression)
            //var res3 = SampleData.Books.Select(a => new {a.Title, a.Publisher });
            //foreach (var item in res3)
            //{
            //    Console.WriteLine(item);
            //}

            //  (query expression)

            //var res3 = 
            //    (from a in SampleData.Books
            //           select new { a.Title, a.Publisher })
            //           .ToList();
            //foreach (var item in res3)
            //{
            //    Console.WriteLine(item);
            //}
            #endregion

            #region  4-Find the number of books which cost more than 20.
            //var res4 = SampleData.Books.Count(a=>a.Price > 20 );
            //Console.WriteLine("Num of Books : " +res4+ " Books");
            #endregion

            #region  5-Display book title, price and subject name sorted by its subject name ascending and by its price descending.

            //var res5 = SampleData.Books.Select(a => new { a.Title, a.Price, a.Subject })
            //    .OrderBy(b => b.Subject.Name)
            //   .ThenByDescending(b => b.Price);

            //foreach (var item in res5)
            //{
            //    Console.WriteLine(item);
            //}

            #endregion

            #region  6-Display All subjects with books related to this subject. (Using 2 methods).
            // SubQuery 

            //var res6 = SampleData.Subjects
            //         .Select(a => new
            //             {
            //                SubjectName = a.Name,
            //                Books = SampleData.Books
            //                  .Where(b => b.Subject.Name == a.Name)
            //                  .Select(b => b.Title)
            //                            });
            //foreach (var subject in res6)
            //{
            //    Console.WriteLine($"Subject: {subject.SubjectName}");
            //    foreach (var book in subject.Books)
            //    {
            //        Console.WriteLine($"{book}");
            //    }
            //}


            //Group By

            //var res6 = SampleData.Books
            //            .GroupBy(b => b.Subject.Name)
            //            .Select(g => new
            //            {
            //                SubjectName = g.Key,
            //                Books = g.Select(b => b.Title)
            //            });

            //foreach (var subject in res6)
            //{
            //    Console.WriteLine($"Subject: {subject.SubjectName}");
            //    foreach (var book in subject.Books)
            //    {
            //        Console.WriteLine($"   {book}");
            //    }
            //}

            #endregion

            #region  7-Try to display book title & price (from book objects) returned from GetBooks Function.

            //var books = SampleData.GetBooks();

            //foreach (Book b in books) 
            //{
            //    Console.WriteLine($"{b.Title} - {b.Price}");
            //}

            #endregion

            #region  8-Display books grouped by publisher & Subject.

            //var res = SampleData.Books
            //        .GroupBy(b => b.Publisher.Name) 
            //        .Select(pubGroup => new
            //        {
            //            PublisherName = pubGroup.Key,
            //            Subjects = pubGroup
            //                .GroupBy(b => b.Subject.Name) 
            //                .Select(subGroup => new
            //                {
            //                    SubjectName = subGroup.Key,
            //                    Books = subGroup.Select(b => new { b.Title, b.Price })
            //                })
            //        });

            //foreach (var publisher in res)
            //{
            //    Console.WriteLine($"Publisher: {publisher.PublisherName}");
            //    foreach (var subject in publisher.Subjects)
            //    {
            //        Console.WriteLine($"   Subject: {subject.SubjectName}");
            //        foreach (var book in subject.Books)
            //        {
            //            Console.WriteLine($"      {book.Title} - {book.Price}");
            //        }
            //    }
            //}

            #endregion

        }
    }
}

