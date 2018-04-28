using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TweetTest.Models;
using static TweetTest.Models.MakeTask;

namespace TweetTest.Controllers {
    [Authorize]
    public class MyTaskController : Controller {
        TaskStoreManager tsm = new TaskStoreManager();
        // GET: Tweet
        public async Task<ActionResult> Index() {
            var id = User.Identity.GetUserId();
            var res = tsm.ReadTask(id);

            var tm = new TweetManager();
            var emb = await tm.EmbedTweetGet(res.tweetId);
            //現状表示？
            return View(emb);
        }

        //タスク登録画面初期表示
        [HttpGet]
        public ActionResult Regist() {
            //最新タスクの削除処理
            var id = User.Identity.GetUserId();
            if (tsm.DeleteTask(id) == 0) {
                return View();  //削除正常終了
            }
            //なにかによって失敗した場合
            return View("Index");
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
            
            if (tsm.CreateTask(tt).Equals(0)) {
                return View("Index", await tm.EmbedTweetGet(tt.tweetId));    //DBへの登録が正常終了
            }
            //失敗の時
            return View("Index");
        }

        //タスク更新画面の初期表示
        [HttpGet]
        public ActionResult Update() {
            var id = User.Identity.GetUserId();
            //現状表示？
            return View(tsm.ReadTask(id));
        }

        //タスク更新画面からタスクのステータスを更新
        [HttpPost]
        public async Task<ActionResult> Update(TweetResult tt) {
            //Tweet用文字列生成
            var tweet = UpdateTaskTweet(tt);

            var id = User.Identity.GetUserId();
            var tr = tsm.ReadTask(id);

            var tm = new TweetManager();
            var res = await tm.ReplyTweet(tweet, tr.tweetId);
            tt.userId = tr.userId;
            tt.id = tr.id;
            tt.tweetId = res.Id;
            
            if (tsm.UpdateTask(tt).Equals(0)) {
                return View("Index", tm.EmbedTweetGet(tt.tweetId));    //DBへの登録が正常終了
            }
            //失敗の時
            return View("Index");
        }

        //これ別にいらなくね？
        //タスクの削除
        public ActionResult DeleteMyTask() {
            return View("Index");
        }
    }
}