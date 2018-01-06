using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            CoreTweet.StatusResponse res;
            var tokens = CoreTweet.Tokens.Create("API-Key", "API-Secret", "AccessToken", "AccessTokenSecret");
            res = tokens.Statuses.Update(status => DateTime.Now + " " + tt.TweetText);
            Debug.Print(res.Text);
            return View(res);
        }
    }
}