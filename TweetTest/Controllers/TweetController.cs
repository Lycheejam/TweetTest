using CoreTweet;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Mvc;
using TweetTest.Models;
using static TweetTest.Models.MakeTask;

namespace TweetTest.Controllers {
    public class TweetController : Controller {
        // GET: Tweet
        public ActionResult Index() {
            return View();
        }
        //テーブルを参照する際、awaitを使用しているのでasyncに変更？
        public async Task<ActionResult> TweetPost(TweetResult tt) {
            //nullのタスクを削除
            tt.myTasks.RemoveAll(x => x.myTask == null);
            //Tweet用文字列生成
            var tweet = CreateTaskTweet(tt);

            var tm = new TweetManager();
            var res = await tm.PostTweet(tweet);
            tt.tweetId = res.Id;
            tt.userId = User.Identity.GetUserId();

            var tsm = new TaskStoreManager();
            if (tsm.CreateTask(tt).Equals(0)) {
                return View(tt);    //DBへの登録が正常終了
            }
            //失敗の時
            return View();
        }

        public ActionResult Reply() {
            //return View(tr);
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> TweetUpdate(TweetResult tt) {
            //Tweet用文字列生成
            var tweet = UpdateTaskTweet(tt);

            var tm = new TweetManager();
            var res = await tm.ReplyTweet(tweet, tt.tweetId);
            tt.tweetId = res.Id;

            var tsm = new TaskStoreManager();
            if (tsm.UpdateTask(tt).Equals(0)) {
                return View("TweetPost", tt);    //DBへの登録が正常終了
            }
            //失敗の時
            return View();
        }
    }
}