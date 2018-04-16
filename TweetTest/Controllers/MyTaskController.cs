using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Mvc;
using TweetTest.Models;
using static TweetTest.Models.MakeTask;

namespace TweetTest.Controllers {
    public class MyTaskController : Controller {
        // GET: Tweet
        public ActionResult Index() {
            var tsm = new TaskStoreManager();
            //現状表示？
            return View(tsm.ReadTask());
        }
        //タスク登録画面初期表示
        [HttpGet]
        public ActionResult Regist() {
            return View();
        }
        //タスク登録画面からタスクをツイート&登録
        [HttpPost]
        public async Task<ActionResult> Regist(TweetResult tt) {
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
                return View("Index", tt);    //DBへの登録が正常終了
            }
            //失敗の時
            return View("Index");
        }

        //タスク更新画面の初期表示
        [HttpGet]
        public ActionResult Update() {
            var tsm = new TaskStoreManager();
            //現状表示？
            return View(tsm.ReadTask());
        }
        //タスク更新画面からタスクのステータスを更新
        [HttpPost]
        public async Task<ActionResult> Update(TweetResult tt) {
            //Tweet用文字列生成
            var tweet = UpdateTaskTweet(tt);

            var tm = new TweetManager();
            var res = await tm.ReplyTweet(tweet, tt.tweetId);
            tt.tweetId = res.Id;

            var tsm = new TaskStoreManager();
            if (tsm.UpdateTask(tt).Equals(0)) {
                return View("Index", tt);    //DBへの登録が正常終了
            }
            //失敗の時
            return View("Index");
        }
        //タスクの削除
        public ActionResult DeleteMyTask() {
            return View("Index");
        }
    }
}