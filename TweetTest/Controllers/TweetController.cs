using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TweetTest.Models;

namespace TweetTest.Controllers
{
    public class TweetController : Controller
    {
        //private 
        // GET: Tweet
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TweetPost(TweetViewModels tt)
        {
            var tokens = CoreTweet.Tokens.Create("API-Key", "API-Secret", "AccessToken", "AccessTokenSecret");
            tokens.Statuses.Update(status => DateTime.Now + " " + tt.TweetText);
            return View(tt);
        }
    }
}