﻿using System;
using System.Linq;
using System.Data.Entity;
using System.Diagnostics;

namespace TweetTest.Models {
    public class TaskStoreManager {
        //タスクの初回登録
        public int CreateTask(TweetResult tr) {
            var tresult = new TweetResult {
                userId = tr.userId,
                myTasks = tr.myTasks,
                tweetId = tr.tweetId
            };
            try {
                using (MyContext db = new MyContext()) {
                    db.TweetResults.Add(tr);
                    db.SaveChanges();
                }
                return 0;   //正常終了値のつもり、あとでちゃんと考えようね
            } catch (Exception) {
                throw;
            }
        }

        public TweetResult ReadTask(string id) {
            try {
                using (MyContext db = new MyContext()) {
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
        public int UpdateTask(TweetResult tr) {
            try {
                using (MyContext db = new MyContext()) {
                    db.Database.Log = (log) => Debug.WriteLine(log);
                    var tresult = db.TweetResults.Where(x => x.id == tr.id && x.endFlag == 0)
                                             .Include("MyTasks")
                                             .SingleOrDefault();
                    //tresult = tr;
                    tresult.myTasks = tr.myTasks;
                    tresult.tweetId = tr.tweetId;
                    //db.Database.Log = sql => { Debug.Write(sql); };
                    db.SaveChanges();
                    return 0;   //正常終了値のつもり、あとでちゃんと考えようね
                }
            } catch (Exception) {
                throw;
            }
        }


        //タスクの削除（と言うかエンドフラグを立てて表示させないようにする。）
        public int DeleteTask(string id) {
            try {
                using (MyContext db = new MyContext()) {
                    TweetResult tr = db.TweetResults.Where(x => x.userId == id && x.endFlag == 0)
                                                    .Include("MyTasks")
                                                    .SingleOrDefault();
                    if (tr != null) {   //タスクが存在しなければ実行しない
                        tr.endFlag = 1;
                        db.SaveChanges();
                    }
                    return 0;
                }
            } catch (Exception) {
                throw;
            }
        }
    }
}
