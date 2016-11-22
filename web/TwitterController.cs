using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CachingFramework.Redis;
using System.Linq;
using System.Net;

namespace Web
{
    [Route("api/[controller]")]
    public class TwitterController : Controller
    {

        [HttpGet]
        public IEnumerable<Tweet> Get()
        {
            var context = GetContext();

            var tweets = context.Collections.GetRedisList<Tweet>("tweets");

            return tweets.ToArray();
        }

        public void Post(Post post)
        {
            var context = GetContext();
            context.PubSub.Publish<Post>("posts", post);
        }

        public static Context GetContext()
        {
            IPHostEntry ip = Dns.GetHostEntryAsync("redis").Result;

            return new Context($"{ip.AddressList.First()}:6379");
        }
    }

    public class Post
    {
        // this is a comma seperate value of author, message
        public string Content { get; set; }
    }

    public class Tweet
    {
        public string Message { get; set; }
        public string Author { get; set; }
    }
}