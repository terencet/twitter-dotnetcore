using System;
using CachingFramework.Redis;
using StackExchange.Redis;
using System.Linq;
using System.Net;

namespace Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var context = GetContext();
            var tweets = context.Collections.GetRedisList<Tweet>("tweets");

            Console.WriteLine("Worker starting to listen to posts");

            context.PubSub.Subscribe<Post>("posts", post =>
            {

                var contentArray = post.Content.Split(',');

                tweets.Add(new Tweet()
                {
                    Author = contentArray[0],
                    Message = contentArray[1]
                });
                
                Console.WriteLine($"Processed Message {contentArray[0]}, {contentArray[1]}");
            });

            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
        }

        public static Context GetContext()
        {
            IPHostEntry ip = Dns.GetHostEntryAsync("redis").Result;

            return new Context( $"{ip.AddressList.First()}:6379");
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
