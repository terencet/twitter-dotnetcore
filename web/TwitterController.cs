using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CachingFramework.Redis;
using System.Linq;
using System.Net;
using System;
using Microsoft.Extensions.Configuration;

namespace Web
{
    [Route("api/[controller]")]
    public class TwitterController : Controller
    {
        private IConfiguration configuration;
        public TwitterController(IConfiguration configuration)
        {
            this.configuration = configuration; 
        }
        [HttpGet]
        public IEnumerable<Tweet> Get()
        {
            var context = GetContext();

            var tweets = context.Collections.GetRedisList<Tweet>("tweets");

            return tweets.ToArray();
        }

        [HttpPost]
        public void Post(Post post)
        {
            var context = GetContext();
            context.PubSub.Publish<Post>("posts", post);
        }

        public Context GetContext()
        {
            var redisHost = configuration["REDIS_HOST"];
            IPHostEntry ip = Dns.GetHostEntryAsync(redisHost).Result;

            Console.WriteLine($"{ip.AddressList.First()} is ipaddress being used");

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