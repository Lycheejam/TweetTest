using System;
using System.Text;

namespace TweetTest.Models {
    public class MakeTask {
        public static string CreateTaskTweet(TweetResult tweet) {
            var sb = new StringBuilder();
            if (tweet.myTasks.Count == 1) {
                sb.Append("─[] " + tweet.myTasks[0].myTask + "\r\n");
            } else if (tweet.myTasks.Count == 2) {
                sb.Append("┬[] " + tweet.myTasks[0].myTask + "\r\n");
                sb.Append("└[] " + tweet.myTasks[1].myTask + "\r\n");
            } else if (tweet.myTasks.Count == 3) {
                sb.Append("┬[] " + tweet.myTasks[0].myTask + "\r\n");
                sb.Append("├[] " + tweet.myTasks[1].myTask + "\r\n");
                sb.Append("└[] " + tweet.myTasks[2].myTask + "\r\n");
            } else if (tweet.myTasks.Count == 4) {
                sb.Append("┬[] " + tweet.myTasks[0].myTask + "\r\n");
                sb.Append("├[] " + tweet.myTasks[1].myTask + "\r\n");
                sb.Append("├[] " + tweet.myTasks[2].myTask + "\r\n");
                sb.Append("└[] " + tweet.myTasks[3].myTask + "\r\n");
            } else if (tweet.myTasks.Count == 5) {
                sb.Append("┬[] " + tweet.myTasks[0].myTask + "\r\n");
                sb.Append("├[] " + tweet.myTasks[1].myTask + "\r\n");
                sb.Append("├[] " + tweet.myTasks[2].myTask + "\r\n");
                sb.Append("├[] " + tweet.myTasks[3].myTask + "\r\n");
                sb.Append("└[] " + tweet.myTasks[4].myTask + "\r\n");
            }
            
            sb.Append("開始時間:" + DateTime.Now + "\r\n");
            sb.Append("#3分間本気出す");

            return sb.ToString();
        }

        public static string UpdateTaskTweet(TweetResult tweet) {
            var sb = new StringBuilder();
            if (tweet.myTasks.Count == 1) {
                sb.Append("─[" + CodeConv(tweet.myTasks[0].state) + "] " + tweet.myTasks[0].myTask + "\r\n");
            } else if (tweet.myTasks.Count == 2) {
                sb.Append("┬[" + CodeConv(tweet.myTasks[0].state) + "] " + tweet.myTasks[0].myTask + "\r\n");
                sb.Append("└[" + CodeConv(tweet.myTasks[1].state) + "] " + tweet.myTasks[1].myTask + "\r\n");
            } else if (tweet.myTasks.Count == 3) {
                sb.Append("┬[" + CodeConv(tweet.myTasks[0].state) + "] " + tweet.myTasks[0].myTask + "\r\n");
                sb.Append("├[" + CodeConv(tweet.myTasks[1].state) + "] " + tweet.myTasks[1].myTask + "\r\n");
                sb.Append("└[" + CodeConv(tweet.myTasks[2].state) + "] " + tweet.myTasks[2].myTask + "\r\n");
            } else if (tweet.myTasks.Count == 4) {
                sb.Append("┬[" + CodeConv(tweet.myTasks[0].state) + "] " + tweet.myTasks[0].myTask + "\r\n");
                sb.Append("├[" + CodeConv(tweet.myTasks[1].state) + "] " + tweet.myTasks[1].myTask + "\r\n");
                sb.Append("├[" + CodeConv(tweet.myTasks[2].state) + "] " + tweet.myTasks[2].myTask + "\r\n");
                sb.Append("└[" + CodeConv(tweet.myTasks[3].state) + "] " + tweet.myTasks[3].myTask + "\r\n");
            } else if (tweet.myTasks.Count == 5) {
                sb.Append("┬[" + CodeConv(tweet.myTasks[0].state) + "] " + tweet.myTasks[0].myTask + "\r\n");
                sb.Append("├[" + CodeConv(tweet.myTasks[1].state) + "] " + tweet.myTasks[1].myTask + "\r\n");
                sb.Append("├[" + CodeConv(tweet.myTasks[2].state) + "] " + tweet.myTasks[2].myTask + "\r\n");
                sb.Append("├[" + CodeConv(tweet.myTasks[3].state) + "] " + tweet.myTasks[3].myTask + "\r\n");
                sb.Append("└[" + CodeConv(tweet.myTasks[4].state) + "] " + tweet.myTasks[4].myTask + "\r\n");
            }

            sb.Append("開始時間:" + DateTime.Now + "\r\n");
            sb.Append("#3分間本気出す");

            return sb.ToString();
        }

        private static string CodeConv(int code) {
            switch (code) {
                case 0:
                    return "";
                case 1:
                    return "○";
                case 2:
                    return "×";
                default:
                    return null;
            }
        }
    }
}