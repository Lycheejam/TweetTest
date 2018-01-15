using CoreTweet;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TweetTest.Models;
using static TweetTest.Models.ReadToken;

namespace TweetTest.Controllers {
    public class TweetController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Tweet
        public ActionResult Index()
        {
            return View();
        }

        //テーブルを参照する際、awaitを使用しているのでasyncに変更？
        public async Task<ActionResult> TweetPost(TweetViewModels tt)
        {
            //コピペ AccessToken&Secretをテーブルから参照する。
            //コピペ元 » ASP.NET Identity：Twitter認証時の情報でツイートする方法 - なか日記 
            // http://blog.nakajix.jp/entry/2014/09/12/074000
            var usermgr = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var claims = await usermgr.GetClaimsAsync(User.Identity.GetUserId());
            

            string accessToken = "", accessTokenSecret = "";
            
            var firstOrDefault = claims.FirstOrDefault(x => x.Type == "ExternalAccessToken");
            if (firstOrDefault != null) // TokenとTokenSecretはペアで登録されるのでnullチェックは片方のみ行う
            {
                accessToken = firstOrDefault.Value;
                accessTokenSecret = claims.FirstOrDefault(x => x.Type == "ExternalAccessTokenSecret").Value;
            }
            //コピペここまで

            var keys = MyTokens;

            var tokens = CoreTweet.Tokens.Create(keys.ConsumerKey
                                               , keys.ConsumerSecret
                                               , accessToken    //テーブルから参照
                                               , accessTokenSecret);    //テーブルから参照
            //ツイート後、レスポンス取得
            var res = tokens.Statuses.Update(status => DateTime.Now + " " + tt.TweetText);
            Debug.Print(res.Text);

            var tr = new TweetResult
            {
                userId = User.Identity.GetUserId(),
                tweet = res.Text,
                tweetId = res.Id
            };

            db.TweetResults.Add(tr);
            db.SaveChanges();

            return View(tr);
        }
        public ActionResult Result()
        {
            var tr = db.TweetResults.ToList();
            return View(tr);
        }

        public ActionResult Reply(int? id)
        {
            TweetResult tr = db.TweetResults.Find(id);
            return View(tr);
        }
        [HttpPost]
        public async Task<ActionResult> Reply(string TweetText, long replyId)
        {
            //ここを共通化したいんだけどどうすんだ
            //コピペ AccessToken&Secretをテーブルから参照する。
            //コピペ元 » ASP.NET Identity：Twitter認証時の情報でツイートする方法 - なか日記 
            // http://blog.nakajix.jp/entry/2014/09/12/074000
            var usermgr = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var claims = await usermgr.GetClaimsAsync(User.Identity.GetUserId());

            string accessToken = "", accessTokenSecret = "";

            var firstOrDefault = claims.FirstOrDefault(x => x.Type == "ExternalAccessToken");
            if (firstOrDefault != null) // TokenとTokenSecretはペアで登録されるのでnullチェックは片方のみ行う
            {
                accessToken = firstOrDefault.Value;
                accessTokenSecret = claims.FirstOrDefault(x => x.Type == "ExternalAccessTokenSecret").Value;
            }
            //コピペここまで

            var keys = MyTokens;

            var tokens = CoreTweet.Tokens.Create(keys.ConsumerKey
                                               , keys.ConsumerSecret
                                               , accessToken    //テーブルから参照
                                               , accessTokenSecret);    //テーブルから参照
            //ツイート後、レスポンス取得
            var res = tokens.Statuses.Update(status => DateTime.Now + " " + TweetText
                                            , in_reply_to_status_id => replyId);
            Debug.Print(res.Text);

            var tr = new TweetResult
            {
                userId = User.Identity.GetUserId(),
                tweet = res.Text,
                tweetId = res.Id
            };

            db.TweetResults.Add(tr);
            db.SaveChanges();
            return View("TweetPost", tr);
        }
    }
}