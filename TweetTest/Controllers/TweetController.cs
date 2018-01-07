using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

            var tokens = CoreTweet.Tokens.Create("API-Key"
                                               , "API-Key"
                                               , accessToken    //テーブルから参照
                                               , accessTokenSecret);    //テーブルから参照
            //ツイート後、レスポンス取得
            var res = tokens.Statuses.Update(status => DateTime.Now + " " + tt.TweetText);
            Debug.Print(res.Text);
            return View(res);
        }
    }
}