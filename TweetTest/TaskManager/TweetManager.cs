using CoreTweet;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using static TweetTest.Models.ReadToken;

namespace TweetTest.Models {
    public class TweetManager {
        //ReplyTweetと同じ
        public async Task<StatusResponse> PostTweet(string tweetStr) {
            var tokens = await CreateTokens();
            var res = tokens.Statuses.Update(status => tweetStr);
            return res;
        }

        //UpdateTaskTweetを分離して引数をstringにする
        //単純にリプライだけのメソッドにしたい。
        public async Task<StatusResponse> ReplyTweet(string tweetStr, long tweetId) {
            var tokens = await CreateTokens();
            var res = tokens.Statuses.Update(status => tweetStr
                                            , in_reply_to_status_id => tweetId);
            return res;
        }
        //ツイート埋め込みウィジェット用のjsonデータを取得
        public async Task<Embed> EmbedTweetGet(long tweetId) {
            var tokens = await CreateTokens();
            var emb = tokens.Statuses.Oembed(tweetId);
            return emb;
        }
        //claimテーブルの参照はコピペ AccessToken&Secretをテーブルから参照する。
        //コピペ元 » ASP.NET Identity：Twitter認証時の情報でツイートする方法 - なか日記 
        // http://blog.nakajix.jp/entry/2014/09/12/074000
        public async Task<Tokens> CreateTokens() {
            var claimkeys = new GetToken();

            //httpcontextクラス httpリクエストに反応していろいろしてくれるらしい
            var usermgr = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var claims = await usermgr.GetClaimsAsync(HttpContext.Current.User.Identity.GetUserId());

            var firstOrDefault = claims.FirstOrDefault(x => x.Type == "ExternalAccessToken");
            if (firstOrDefault != null) // TokenとTokenSecretはペアで登録されるのでnullチェックは片方のみ行う
            {
                claimkeys.accessToken = firstOrDefault.Value;
                claimkeys.accessTokenSecret = claims.FirstOrDefault(x => x.Type == "ExternalAccessTokenSecret").Value;
            }

            //Models.ReadToken APIキー取ってきてる。
            var keys = MyTokens;

            //ツイート用トークン生成
            var tokens = Tokens.Create(keys.ConsumerKey
                                     , keys.ConsumerSecret
                                     , claimkeys.accessToken    //テーブルから参照
                                     , claimkeys.accessTokenSecret);    //テーブルから参照
            return tokens;
        }
    }
}