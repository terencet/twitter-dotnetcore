using System;
using ClassLibrary;

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
}
