using System.Formats.Tar;
using System.Runtime.InteropServices.ComTypes;
using EFUppgift;
using entity_framework_uppgift.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

#region start and retrieve data

using BloggingContext db = new();

Console.WriteLine($"Database file found ({db.DbPath})\n");

string[] blogInfo = File.ReadAllLines($"{Environment.CurrentDirectory}" + "/data/blog.csv");

string[] userInfo = File.ReadAllLines($"{Environment.CurrentDirectory}" + "/data/user.csv");

string[] postInfo = File.ReadAllLines($"{Environment.CurrentDirectory}" + "/data/post.csv");

#endregion


for (int i = 0; i < postInfo.Length; i++)
{ 
    string[] strInfo = postInfo[i].Split(",");
    
   Post newPost = new Post
   {     
       PostId = int.Parse(strInfo[0]),
       Title = strInfo[1], 
       Content = strInfo[2], 
       BlogId = int.Parse(strInfo[3]),
       UserId = int.Parse(strInfo[4])
   };

   db.Add(newPost);
   db.SaveChanges();

}


Dictionary<int, Blog> blogs = new Dictionary<int, Blog>();

for (int i = 0; i < blogInfo.Length; i++)
{
    string[] strInfo = blogInfo[i].Split(",");

    if (!blogs.ContainsKey(int.Parse(strInfo[0])))
    {
        blogs[int.Parse(strInfo[0])] = new Blog
        {
            BlogId = int.Parse(strInfo[0]),
            Url = strInfo[1],
            Name = strInfo[2],

        };
    }

    db.Add(new Post { BlogId = int.Parse(strInfo[0]) });
}


Dictionary<int, User> users = new Dictionary<int, User>();

for (int i = 0; i < blogInfo.Length; i++)
{
    string[] strInfo = userInfo[i].Split(",");

    if (!users.ContainsKey(int.Parse(strInfo[0])))
    {
        users[int.Parse(strInfo[0])] = new User()
        {
            UserId = int.Parse(strInfo[0]),
            Username = strInfo[1],
            Password = strInfo[2]
        };
    }
    
    db.Add(new Post { UserId = int.Parse(strInfo[0]) });
    
}

foreach (var p in db.Posts)
{
    db.Add(p);
}

foreach (var b in blogs.Values)
{
    db.Add(b);
}

foreach (var u in users.Values)
{
    db.Add(u);
}

foreach (User u in db.Users)
{
    Console.WriteLine($"username: {u.Username}\n");

    foreach (Post p in u.Posts)
    {
        foreach (Blog b in db.Blogs)
        {
            if (b.BlogId == p.BlogId)
            {
                Console.WriteLine($"Blog: {b.Name}, Url: {b.Url}");
                if (p.Title != null)
                {
                    Console.WriteLine($"Title: {p.Title}");
                    Console.WriteLine($"Content: {p.Content}\n");
                }
            }
            
            
        }
    }
    
    Console.WriteLine();
}

foreach (Blog b in db.Blogs)
{
    db.Remove(b);
}

foreach (Post p in db.Posts)
{
    db.Remove(p);
}

foreach (User u in db.Users)
{
    db.Remove(u);
}

db.SaveChanges();
