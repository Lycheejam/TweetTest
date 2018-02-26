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

        public static string UpdateTasks(TweetViewModels tweet) {


            return null;
        }
    }
}