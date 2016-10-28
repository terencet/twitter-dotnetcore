using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var messageStream = InitializeMessageStream();
            var messageFeed = new MessageFeed();

            while(messageStream.HasPosts)
            {
               var post = messageStream.Pop();
               if (post != null)
               {
                 var contentArray = post.Content.Split(',');
                 messageFeed.Add(new Tweet{
                    Author = contentArray[0],
                    Message = contentArray[1]
                  });
                  Console.WriteLine($"Processed Message {contentArray[0]}, {contentArray[1]}");
               }
            }
            Console.Read();
        }

        public static MessageStream InitializeMessageStream()
        {
            var messageStream = new MessageStream();
            messageStream.Queue(new Post
            {
              Content = "Ceola,This is the first tweet"
            });
            return messageStream;
        }
    }

    public class MessageStream
    {
      private Queue<Post> queue;
      public MessageStream()
      {
        queue = new Queue<Post>();
      }

      public bool HasPosts { get { return queue.Any(); }}

      public void Queue(Post post)
      {
        queue.Enqueue(post);
      }

      public Post Pop()
      {
        return queue.Dequeue();
      }
    }

    public class Post
    {
      // this is a comma seperate value of author, message
      public string Content {get; set;}
    }

    public class MessageFeed
    {
      public IList<Tweet> Tweets {get; set;}

      public MessageFeed()
      {
        Tweets = new List<Tweet>();
      }

      public void Add(Tweet tweet)
      {
        Tweets.Add(tweet);
      }

    }

    public class Tweet
    {
      public string Message {get; set;}
      public string Author {get; set;}
    }
}
