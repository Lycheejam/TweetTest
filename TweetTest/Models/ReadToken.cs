using Newtonsoft.Json;
using System.IO;

namespace TweetTest.Models {
    public class ReadToken {
        /* 
         * ちょまどさんのGitHubソースをコピペ
         * https://github.com/chomado/TwitterBot_InCSharp/blob/master/CSharpTwitterBot/BotMain.cs
         */
         
        /// <summary>
        /// アクセストークンなど、投稿に必要なキーを取得。（キャッシュがあればキャッシュから。無かったらファイルから読み込む）
        /// </summary>
        public static Keys MyTokens => _myTokens ?? (_myTokens = ReadTokens());
        static Keys _myTokens = null;

        /// <summary>
        /// key.json ファイルから、アクセストークンなどを読み込む。
        /// </summary>
        private static Keys ReadTokens() {
            /var json = File.ReadAllText(@"..\..\..\TweetTest\App_Data\keys.json");
            

            return JsonConvert.DeserializeObject<Keys>(json);
        }

        /// <summary>
        /// 投稿に使う各種キーの集まり。（型定義）
        /// </summary>
        public class Keys {
            public string ConsumerKey { get; set; }
            public string ConsumerSecret { get; set; }
        }
    }
}