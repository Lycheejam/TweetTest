using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TweetTest.Models {
    public static class TaskModels {
        public static string CreateTasks(TweetViewModels tweet) {
            string result = "";
            result =  "┬[] " + tweet.Task1 + "\r\n";
            result += "├[] " + tweet.Task2 + "\r\n";
            result += "├[] " + tweet.Task3 + "\r\n";
            result += "├[] " + tweet.Task4 + "\r\n";
            result += "└[] " + tweet.Task5 + "\r\n";
            result += "#3分間本気出す";

            return result;
        }

        public static string UpdateTasks(TaskResult tweet) {
            string result = "";
            result =  "┬[" + CodeConv(tweet.Task1chk) + "] " + tweet.Task1 + "\r\n";
            result += "├[" + CodeConv(tweet.Task2chk) + "] " + tweet.Task2 + "\r\n";
            result += "├[" + CodeConv(tweet.Task3chk) + "] " + tweet.Task3 + "\r\n";
            result += "├[" + CodeConv(tweet.Task4chk) + "] " + tweet.Task4 + "\r\n";
            result += "└[" + CodeConv(tweet.Task5chk) + "] " + tweet.Task5 + "\r\n";
            result += "#3分間本気出す";

            return result;
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