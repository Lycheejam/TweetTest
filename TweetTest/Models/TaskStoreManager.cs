using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using System.Web;
using System.Web.Mvc;

namespace TweetTest.Models {
    public class TaskStoreManager {
        private MyContext db = new MyContext();

        //タスクの初回登録
        public int CreateTask(TweetResult tr) {
            var tresult = new TweetResult {
                userId = tr.userId,
                myTasks = tr.myTasks,
                tweetId = tr.tweetId
            };
            try {
                db.TweetResults.Add(tr);
                db.SaveChanges();
                return 0;   //正常終了値のつもり、あとでちゃんと考えようね
            } catch (Exception) {
                throw;
            }
        }

        public TweetResult ReadTask() {
            //IDの最大値を取得
            //これ複数のuserの場合どうなる？他のユーザーのID取得してしまうんでない？
            var maxid = db.TweetResults.Max(x => x.id);
            //検索条件
            //userId,終了フラグ = 0
            string id = HttpContext.Current.User.Identity.GetUserId();
            //TweetResult tr = db.TweetResults.SingleOrDefault(x => x.userId == id);
            TweetResult tr = db.TweetResults
                                    .Where(x => x.userId == id)
                                    .Include(nameof(MyTasks));
            return tr;
        }

        //タスクのステータス更新とツイートID（リプライ先）の更新
        public int UpdateTask(TweetResult tr) {
            var tresult = new TweetResult {
                //アップデートにした場合ここも変更
                userId = tr.userId,
                myTasks = tr.myTasks,
                tweetId = tr.tweetId
            };
            try {
                //ここをアップデートに変更しないといけない？
                db.TweetResults.Add(tr);
                db.SaveChanges();
                return 0;   //正常終了値のつもり、あとでちゃんと考えようね
            } catch (Exception) {
                throw;
            }
        }

        public string DeleteTask() {
            return null;
        }
    }
}