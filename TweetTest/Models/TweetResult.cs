using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TweetTest.Models
{
    public class MyContext : DbContext {
        public DbSet<TweetResult> TweetResults { get; set; }
    }

    public class TweetResult : TweetViewModels
    {
        public int id { get; set; }
        public string userId { get; set; }
        public long tweetId { get; set; }
    }
    public class TweetViewModels {
        public string Task1 { get; set; }
        public string Task2 { get; set; }
        public string Task3 { get; set; }
        public string Task4 { get; set; }
        public string Task5 { get; set; }
    }
    public class TaskResult : TweetViewModels {
        public int Task1chk { get; set; }
        public int Task2chk { get; set; }
        public int Task3chk { get; set; }
        public int Task4chk { get; set; }
        public int Task5chk { get; set; }
        public long replyid { get; set; }
    }

}