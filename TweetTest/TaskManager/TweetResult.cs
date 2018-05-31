﻿using System.Collections.Generic;
using System.Data.Entity;

namespace TweetTest.Models {
    public class TweetResult
    {
        public int id { get; set; }
        public string userId { get; set; }
        public long tweetId { get; set; }
        public virtual List<MyTask> myTasks { get; set; }
        public int endFlag { get; set; }
    }
    public class MyTask {
        public int id { get; set; }
        public string myTask { get; set; }
        public int state { get; set; }
    }
}