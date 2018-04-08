using System;
using System.Text;

namespace TweetTest.Models {
    public static class TaskModels {
        public static string CreateTasks(TweetResult tweet) {
            //string result = "";
            //result =  "┬[] " + tweet.Task1 + "\r\n";
            //result += "├[] " + tweet.Task2 + "\r\n";
            //result += "├[] " + tweet.Task3 + "\r\n";
            //result += "├[] " + tweet.Task4 + "\r\n";
            //result += "└[] " + tweet.Task5 + "\r\n";
            //result += "#3分間本気出す";

            var sb = new StringBuilder();
            foreach (var item in tweet.myTasks) {
                if (item.Equals(null)) {
                    break;
                }
                sb.Append("─[] " + item.myTask + "\r\n");
            }
            ////タスクが１つだけの場合
            //if (tweet.Task2 == null) {
            //    sb.Append("─[] " + tweet.Task1 + "\r\n");
            //} else {
            //    //2つ以上
            //    sb.Append("┬[] " + tweet.Task1 + "\r\n");
            //    if (tweet.Task3 != null) {
            //        sb.Append("├[] " + tweet.Task2 + "\r\n");
            //        if (tweet.Task4 != null) {
            //            sb.Append("├[] " + tweet.Task3 + "\r\n");
            //            if (tweet.Task5 != null) {
            //                sb.Append("├[] " + tweet.Task4 + "\r\n");
            //                sb.Append("└[] " + tweet.Task5 + "\r\n");
            //            } else {
            //                sb.Append("└[] " + tweet.Task4 + "\r\n");
            //            }
            //        } else {
            //            sb.Append("└[] " + tweet.Task3 + "\r\n");
            //        }
            //    } else {
            //        sb.Append("└[] " + tweet.Task2 + "\r\n");
            //    }
            //}
            sb.Append("開始時間:" + DateTime.Now + "\r\n");
            sb.Append("#3分間本気出す");

            return sb.ToString();
        }

        public static string UpdateTasks(TweetResult tweet) {
            //string result = "";
            //result =  "┬[" + CodeConv(tweet.Task1chk) + "] " + tweet.Task1 + "\r\n";
            //result += "├[" + CodeConv(tweet.Task2chk) + "] " + tweet.Task2 + "\r\n";
            //result += "├[" + CodeConv(tweet.Task3chk) + "] " + tweet.Task3 + "\r\n";
            //result += "├[" + CodeConv(tweet.Task4chk) + "] " + tweet.Task4 + "\r\n";
            //result += "└[" + CodeConv(tweet.Task5chk) + "] " + tweet.Task5 + "\r\n";
            //result += "#3分間本気出す";

            var sb = new StringBuilder();
            foreach (var item in tweet.myTasks) {
                if (item.Equals(null)) {
                    break;
                }
                sb.Append("─[" + item.state + "] " + item.myTask + "\r\n");
            }
            //タスクが１つだけの場合
            //if (tweet.Task2 == null) {
            //    sb.Append("─[" + tweet.Task1chk + "] " + tweet.Task1 + "\r\n");
            //} else {
            //    //2つ以上
            //    sb.Append("┬[" + tweet.Task1chk + "] " + tweet.Task1 + "\r\n");
            //    if (tweet.Task3 != null) {
            //        sb.Append("├[" + tweet.Task2chk + "] " + tweet.Task2 + "\r\n");
            //        if (tweet.Task4 != null) {
            //            sb.Append("├[" + tweet.Task3chk + "] " + tweet.Task3 + "\r\n");
            //            if (tweet.Task5 != null) {
            //                sb.Append("├[" + tweet.Task4chk + "] " + tweet.Task4 + "\r\n");
            //                sb.Append("└[" + tweet.Task5chk + "] " + tweet.Task5 + "\r\n");
            //            } else {
            //                sb.Append("└[" + tweet.Task4chk + "] " + tweet.Task4 + "\r\n");
            //            }
            //        } else {
            //            sb.Append("└[" + tweet.Task3chk + "] " + tweet.Task3 + "\r\n");
            //        }
            //    } else {
            //        sb.Append("└[" + tweet.Task2chk + "] " + tweet.Task2 + "\r\n");
            //    }
            //}
            sb.Append("開始時間:" + DateTime.Now + "\r\n");
            sb.Append("#3分間本気出す");

            return sb.ToString();
        }

        private static string CodeConv(int code) {
            switch (code) {
                case 1:
                    return "";
                case 2:
                    return "○";
                case 3:
                    return "×";
                default:
                    return null;
            }
        }
    }
}