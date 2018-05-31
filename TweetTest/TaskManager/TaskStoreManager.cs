using System;
using System.Linq;
using System.Data.Entity;
using System.Diagnostics;

namespace TweetTest.Models {
    public class TaskStoreManager {
        //タスクの初回登録
        public bool CreateTask(TweetResult tr) {
            var tresult = new TweetResult {
                userId = tr.userId,
                tasks = tr.tasks,
                tweetId = tr.tweetId
            };
            try {
                using (var db = new ApplicationDbContext()) {
                    db.TweetResults.Add(tr);
                    db.SaveChanges();
                }
                return true;   //正常終了値のつもり、あとでちゃんと考えようね
            } catch (Exception) {
                throw;
            }
        }

        public TweetResult ReadTask(string id) {
            try {
                using (var db = new ApplicationDbContext()) {
                    //データ消さない方針ならendflag別にいらなくね？
                    TweetResult tr = db.TweetResults.Where(x => x.userId == id && x.endFlag == 0)
                                                    .Include("MyTasks")
                                                    .SingleOrDefault();
                    return tr;  //最後に見つかったレコードは必ずendFlagが0?
                }
            } catch (Exception) {
                throw;
            }
        }

        //タスクのステータス更新とツイートID（リプライ先）の更新
        public bool UpdateTask(TweetResult tr) {
            try {
                using (var db = new ApplicationDbContext()) {
                    db.Database.Log = (log) => Debug.WriteLine(log);
                    var tresult = db.TweetResults.Where(x => x.id == tr.id && x.endFlag == 0)
                                             .Include("MyTasks")
                                             .SingleOrDefault();
                    //tresult = tr;
                    tresult.tasks = tr.tasks;
                    tresult.tweetId = tr.tweetId;
                    //db.Database.Log = sql => { Debug.Write(sql); };
                    db.SaveChanges();
                    return true;   //正常終了値のつもり、あとでちゃんと考えようね
                }
            } catch (Exception) {
                throw;
            }
        }


        //タスクの削除（と言うかエンドフラグを立てて表示させないようにする。）
        public bool DeleteTask(string id) {
            try {
                using (var db = new ApplicationDbContext()) {
                    TweetResult tr = db.TweetResults.Where(x => x.userId == id && x.endFlag == 0)
                                                    .Include("MyTasks")
                                                    .SingleOrDefault();
                    if (tr != null) {   //タスクが存在しなければ実行しない
                        tr.endFlag = 1;
                        db.SaveChanges();
                    }
                    return true;
                }
            } catch (Exception) {
                throw;
            }
        }
    }
}
