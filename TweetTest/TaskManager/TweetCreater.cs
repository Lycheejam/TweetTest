using System.Text;

namespace TweetTest.Models {
    public class MakeTask {
        public static string CreateTaskTweet(TweetResult tweet) {
            var sb = new StringBuilder();
            if (tweet.tasks.Count == 1) {
                sb.Append("─[] " + tweet.tasks[0].task + "\r\n");
            } else if (tweet.tasks.Count == 2) {
                sb.Append("┬[] " + tweet.tasks[0].task + "\r\n");
                sb.Append("└[] " + tweet.tasks[1].task + "\r\n");
            } else if (tweet.tasks.Count == 3) {
                sb.Append("┬[] " + tweet.tasks[0].task + "\r\n");
                sb.Append("├[] " + tweet.tasks[1].task + "\r\n");
                sb.Append("└[] " + tweet.tasks[2].task + "\r\n");
            } else if (tweet.tasks.Count == 4) {
                sb.Append("┬[] " + tweet.tasks[0].task + "\r\n");
                sb.Append("├[] " + tweet.tasks[1].task + "\r\n");
                sb.Append("├[] " + tweet.tasks[2].task + "\r\n");
                sb.Append("└[] " + tweet.tasks[3].task + "\r\n");
            } else if (tweet.tasks.Count == 5) {
                sb.Append("┬[] " + tweet.tasks[0].task + "\r\n");
                sb.Append("├[] " + tweet.tasks[1].task + "\r\n");
                sb.Append("├[] " + tweet.tasks[2].task + "\r\n");
                sb.Append("├[] " + tweet.tasks[3].task + "\r\n");
                sb.Append("└[] " + tweet.tasks[4].task + "\r\n");
            }
            sb.Append("#3分間本気出す");

            return sb.ToString();
        }

        public static string UpdateTaskTweet(TweetResult tweet) {
            var sb = new StringBuilder();
            if (tweet.tasks.Count == 1) {
                sb.Append("─[" + CodeConv(tweet.tasks[0].state) + "] " + tweet.tasks[0].task + "\r\n");
            } else if (tweet.tasks.Count == 2) {
                sb.Append("┬[" + CodeConv(tweet.tasks[0].state) + "] " + tweet.tasks[0].task + "\r\n");
                sb.Append("└[" + CodeConv(tweet.tasks[1].state) + "] " + tweet.tasks[1].task + "\r\n");
            } else if (tweet.tasks.Count == 3) {
                sb.Append("┬[" + CodeConv(tweet.tasks[0].state) + "] " + tweet.tasks[0].task + "\r\n");
                sb.Append("├[" + CodeConv(tweet.tasks[1].state) + "] " + tweet.tasks[1].task + "\r\n");
                sb.Append("└[" + CodeConv(tweet.tasks[2].state) + "] " + tweet.tasks[2].task + "\r\n");
            } else if (tweet.tasks.Count == 4) {
                sb.Append("┬[" + CodeConv(tweet.tasks[0].state) + "] " + tweet.tasks[0].task + "\r\n");
                sb.Append("├[" + CodeConv(tweet.tasks[1].state) + "] " + tweet.tasks[1].task + "\r\n");
                sb.Append("├[" + CodeConv(tweet.tasks[2].state) + "] " + tweet.tasks[2].task + "\r\n");
                sb.Append("└[" + CodeConv(tweet.tasks[3].state) + "] " + tweet.tasks[3].task + "\r\n");
            } else if (tweet.tasks.Count == 5) {
                sb.Append("┬[" + CodeConv(tweet.tasks[0].state) + "] " + tweet.tasks[0].task + "\r\n");
                sb.Append("├[" + CodeConv(tweet.tasks[1].state) + "] " + tweet.tasks[1].task + "\r\n");
                sb.Append("├[" + CodeConv(tweet.tasks[2].state) + "] " + tweet.tasks[2].task + "\r\n");
                sb.Append("├[" + CodeConv(tweet.tasks[3].state) + "] " + tweet.tasks[3].task + "\r\n");
                sb.Append("└[" + CodeConv(tweet.tasks[4].state) + "] " + tweet.tasks[4].task + "\r\n");
            }
            
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