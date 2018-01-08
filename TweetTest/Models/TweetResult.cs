using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TweetTest.Models
{
    public class TweetResult
    {
        public int id { get; set; }
        public string tweet { get; set; }
        public string tweetId { get; set; }
    }
    public class MyContext : DbContext
    {
        public DbSet<TweetResult> TweetResults { get; set; }
    }
}