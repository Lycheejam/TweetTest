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
    public class TweetController : Controller {
        private MyContext db = new MyContext();

        // GET: Tweet
        public ActionResult Index() {
            return View();
        }

        //テーブルを参照する際、awaitを使用しているのでasyncに変更？
        public async Task<ActionResult> TweetPost(TweetViewModels tt) {
            var tokens = await CreateTokens();
            
            //ツイート後、レスポンス取得
            var res = tokens.Statuses.Update(status => TaskModels.CreateTasks(tt));
            Debug.Print(res.Text);

            var tr = new TweetResult {
                userId = User.Identity.GetUserId(),
                Task1 = tt.Task1,
                Task2 = tt.Task2,
                Task3 = tt.Task3,
                Task4 = tt.Task4,
                Task5 = tt.Task5,
                tweetId = res.Id
            };

            db.TweetResults.Add(tr);
            db.SaveChanges();

            return View(tr);
        }
        //これ使わずに最新タスクだけ見れればいいんだよね？
        public ActionResult Result() {
            var tr = db.TweetResults.ToList();
            return View(tr);
        }

        //public ActionResult Reply(int? id) {
        //    TweetResult tr = db.TweetResults.Find(id);
        //    return View(tr);
        //}
        public ActionResult Reply() {
            //var tr = db.TweetResults.Last();
            var maxid = db.TweetResults.Max(x => x.userId);
            TweetResult tr = db.TweetResults.SingleOrDefault(x => x.userId == maxid);
            return View(tr);
        }

        [HttpPost]
        public async Task<ActionResult> TweetUpdate(TweetResult tresult) {
            //ツイート用トークン生成
            var tokens = await CreateTokens();
            //ツイート後、レスポンス取得
            var res = tokens.Statuses.Update(status => TaskModels.UpdateTasks(tresult)
                                            , in_reply_to_status_id => tresult.tweetId);
            Debug.Print(res.Text);

            var tr = new TweetResult {
                userId = User.Identity.GetUserId(),
                Task1 = tresult.Task1,
                Task1chk = tresult.Task1chk,
                Task2 = tresult.Task2,
                Task2chk = tresult.Task2chk,
                Task3 = tresult.Task3,
                Task3chk = tresult.Task3chk,
                Task4 = tresult.Task4,
                Task4chk = tresult.Task4chk,
                Task5 = tresult.Task5,
                Task5chk = tresult.Task5chk,
                tweetId = res.Id
            };

            db.TweetResults.Add(tr);
            db.SaveChanges();
            return View("TweetPost", tr);
        }

        /*
         * javaみたいにセッションスコープとかインスタンス保存しておくものってないのかな？
         * この生成されたトークンを使いまわすみたいに思っていたんだけどできない？
         */

        //claimテーブルの参照はコピペ AccessToken&Secretをテーブルから参照する。
        //コピペ元 » ASP.NET Identity：Twitter認証時の情報でツイートする方法 - なか日記 
        // http://blog.nakajix.jp/entry/2014/09/12/074000
        public async Task<Tokens> CreateTokens() {
            var claimkeys = new GetToken();

            //httpcontextクラス httpリクエストに反応していろいろしてくれるらしい
            var usermgr = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var claims = await usermgr.GetClaimsAsync(User.Identity.GetUserId());

            var firstOrDefault = claims.FirstOrDefault(x => x.Type == "ExternalAccessToken");
            if (firstOrDefault != null) // TokenとTokenSecretはペアで登録されるのでnullチェックは片方のみ行う
            {
                claimkeys.accessToken = firstOrDefault.Value;
                claimkeys.accessTokenSecret = claims.FirstOrDefault(x => x.Type == "ExternalAccessTokenSecret").Value;
            }

            //Models.ReadToken APIキー取ってきてる。
            var keys = MyTokens;

            //ツイート用トークン生成
            var tokens = Tokens.Create(keys.ConsumerKey
                                               , keys.ConsumerSecret
                                               , claimkeys.accessToken    //テーブルから参照
                                               , claimkeys.accessTokenSecret);    //テーブルから参照
            return tokens;
        }
    }
}