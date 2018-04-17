using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TweetTest.Models
{
    public class MyContext : DbContext {
        public DbSet<TweetResult> TweetResults { get; set; }
        public DbSet<MyTask> MyTasks { get; set; }
    }

    public class TweetResult
    {
        public int id { get; set; }
        public string userId { get; set; }
        public long tweetId { get; set; }
        public List<MyTask> myTasks { get; set; }
        public int endFlag { get; set; }
    }
    public class MyTask {
        public int id { get; set; }
        public string myTask { get; set; }
        public int state { get; set; }
    }
}