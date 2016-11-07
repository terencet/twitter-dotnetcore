using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;


namespace ClassLibrary
{
    public class Post
    {
      // this is a comma seperate value of author, message
      public string Content {get; set;}
    }

    public class Tweet
    {
      public string Message {get; set;}
      public string Author {get; set;}
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
        if(post == null)
            throw new ArgumentNullException("post");

        queue.Enqueue(post);
      }

      public Post Pop()
      {
        return queue.Dequeue();
      }
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
}
