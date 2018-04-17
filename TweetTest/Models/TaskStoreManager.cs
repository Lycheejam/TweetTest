using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Data.Entity;

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

        public TweetResult ReadTask(string id) {
            db.Database.Log = sql => { Debug.Write(sql); };
            TweetResult tr = db.TweetResults.Where(x => x.userId == id && x.endFlag == 0)
                                            .Include("MyTasks")
                                            .SingleOrDefault();
            return tr;  //最後に見つかったレコードは必ずendFlagが0?
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