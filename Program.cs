using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class Program
{

    public class BloggingContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public string DbPath { get; }

        public BloggingContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "blogging.db");

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options) =>

            options.UseSqlite($"Data Source ={DbPath}");

    }



    public static void Main(string[] args)
    {


        // Creating users
        User user1 = new User { UserId = 1, Name = "John", Password = "password1" };
        User user2 = new User { UserId = 2, Name = "Alice", Password = "password2" };
        User user3 = new User { UserId = 3, Name = "Bob", Password = "password3" };
        User user4 = new User { UserId = 4, Name = "Emma", Password = "password4" };
        User user5 = new User { UserId = 5, Name = "Michael", Password = "password5" };
        User user6 = new User { UserId = 6, Name = "Sophia", Password = "password6" };

        // Creating articles
        Article article1 = new Article { ArticleId = 1, Name = "Introduction to C#" };
        Article article2 = new Article { ArticleId = 2, Name = "Entity Framework Core Basics" };
        Article article3 = new Article { ArticleId = 3, Name = "ASP.NET Core MVC" };
        Article article4 = new Article { ArticleId = 4, Name = "Python for Data Science" };
        Article article5 = new Article { ArticleId = 5, Name = "JavaScript Fundamentals" };
        Article article6 = new Article { ArticleId = 6, Name = "Java Programming Basics" };

        // Creating posts
        Post post1 = new Post { PostId = 1, postname = "Post 1", ArticleId = 1, User = user1, Article = article1 };
        Post post2 = new Post { PostId = 2, postname = "Post 2", ArticleId = 2, User = user2, Article = article2 };
        Post post3 = new Post { PostId = 3, postname = "Post 3", ArticleId = 1, User = user3, Article = article1 };
        Post post4 = new Post { PostId = 4, postname = "Post 4", ArticleId = 3, User = user4, Article = article3 };
        Post post5 = new Post { PostId = 5, postname = "Post 5", ArticleId = 4, User = user5, Article = article4 };
        Post post6 = new Post { PostId = 6, postname = "Post 6", ArticleId = 5, User = user6, Article = article5 };

        // Creating blogs
        Blog blog1 = new Blog { BlogId = 1, Name = "Tech Blog", User = user1 };
        Blog blog2 = new Blog { BlogId = 2, Name = "Programming Blog", User = user2 };
        Blog blog3 = new Blog { BlogId = 3, Name = "Developer's Diary", User = user3 };
        Blog blog4 = new Blog { BlogId = 4, Name = "Data Science Insights", User = user4 };
        Blog blog5 = new Blog { BlogId = 5, Name = "Web Development Hub", User = user5 };
        Blog blog6 = new Blog { BlogId = 6, Name = "Software Engineering Weekly", User = user6 };

        var users = new List<User> { user1, user2, user3, user4, user5, user6 };

        foreach (var user in users)
        {
            if (user.UserId == 1)
            {
                user.Articles.Add(article1);
                user.Articles.Add(article2);
                user.Posts.Add(post1);
                user.Posts.Add(post3);
            }
            else if (user.UserId == 2)
            {
                user.Articles.Add(article1);
                user.Articles.Add(article2);
                user.Posts.Add(post2);
            }
            else if (user.UserId == 3)
            {
                user.Articles.Add(article1);
                user.Posts.Add(post3);
            }
            else if (user.UserId == 4)
            {
                user.Articles.Add(article3);
                user.Posts.Add(post4);
            }
            else if (user.UserId == 5)
            {
                user.Articles.Add(article4);
                user.Posts.Add(post5);
            }
            else if (user.UserId == 6)
            {
                user.Articles.Add(article5);
                user.Posts.Add(post6);
            }
        }


        Console.Write("Enter Username: ");
        string? username = Console.ReadLine();

        User? us = null;
        foreach (var u in users)
        {
            if (u.Name == username)
            {
                us = u;
                break;
            }
        }

        if (us != null)
        {
            Console.WriteLine($"User: {us.Name}, UserId: {us.UserId}");
            foreach (var post in us.Posts)
            {
                Article? article = post.Article;
                if (article != null)
                {
                    Blog? blog = GetBlogByArticleId(article.ArticleId);
                    Console.WriteLine($"PostId: {post.PostId}, PostName: {post.postname}, ArticleName: {article.Name}, ArticleId: {article.ArticleId}, BlogName: {blog?.Name}");
                }
            }
        }
        else
        {
            Console.WriteLine("user not found.");
        }
    }

    public static Blog? GetBlogByArticleId(int articleId)
    {
        return articleId switch
        {
            1 => new Blog { BlogId = 1, Name = "Tech Blog" },
            2 => new Blog { BlogId = 2, Name = "Programming Blog" },
            3 => new Blog { BlogId = 3, Name = "Developer's Diary" },
            4 => new Blog { BlogId = 4, Name = "Data Science Insights" },
            5 => new Blog { BlogId = 5, Name = "Web Development Hub" },
            6 => new Blog { BlogId = 6, Name = "Software Engineering Weekly" },
            _ => null,
        }; ;
    }
}

public class User
{
    public int UserId { get; set; }
    public string? Name { get; set; }
    public string? Password { get; set; }
    public List<Article> Articles { get; } = new();
    public List<Post> Posts { get; } = new();
}

public class Article
{
    public int ArticleId { get; set; }
    public string? Name { get; set; }
    public List<Post> Posts { get; } = new();
}

public class Post
{
    public string? postname { get; set; }
    public int PostId { get; set; }
    public int ArticleId { get; set; }
    public User? User { get; set; }
    public Article? Article { get; set; }
}

public class Blog
{
    public int BlogId { get; set; }
    public string? Name { get; set; }
    public User? User { get; set; }
}
