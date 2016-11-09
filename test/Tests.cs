using System;
using Xunit;
using ClassLibrary;

namespace Tests
{
    public class MessageStreamTests
    {
        public struct MessageStreamData
        {
            public Post Post {get; set;}
        }

        public MessageStreamData GetData()
        {
            return new MessageStreamData
            {
                Post = new Post()
            };
        }

        [Fact]
        public void Queue_WithNullArgument_ThrowsArgumentNullException() 
        {
            //ARRANGE
            var testData = GetData();
            var messageStream = new MessageStream();

            //ACT
            var exception = Assert.Throws<ArgumentNullException>(() => messageStream.Queue(null));

            //ASSERT
            Assert.Equal("post", exception.ParamName);
        }

        [Fact]
        public void Queue_WithValidArgument_StreamHasPosts() 
        {
            //ARRANGE
            var testData = GetData();
            var messageStream = new MessageStream();

            //ACT
            messageStream.Queue(testData.Post);

            //ASSERT
            Assert.True(messageStream.HasPosts);
        }

        [Fact]
        public void Pop_WithValidState_StreamHasNoPosts() 
        {
            //ARRANGE
            var testData = GetData();
            var messageStream = new MessageStream();
            messageStream.Queue(testData.Post);

            //ACT
            messageStream.Pop();

            //ASSERT
            Assert.False(messageStream.HasPosts);
        }                   
    }

    public class MessageFeedTests
    {
        public struct MessageFeedData
        {
            public Tweet Tweet {get; set;}
        }

        public MessageFeedData GetData()
        {
            return new MessageFeedData
            {
                Tweet = new Tweet()
            };
        }

        [Fact]
        public void Add_WithNullArgument_ThrowsArgumentNullException() 
        {
            //ARRANGE
            var testData = GetData();
            var messageFeed = new MessageFeed();

            //ACT
            var exception = Assert.Throws<ArgumentNullException>(() => messageFeed.Add(null));

            //ASSERT
            Assert.Equal("tweet", exception.ParamName);
        }

        [Fact]
        public void Add_WithValidArgument_FeedHasTweets() 
        {
            //ARRANGE
            var testData = GetData();
            var messageFeed = new MessageFeed();

            //ACT
            messageFeed.Add(testData.Tweet);

            //ASSERT
            Assert.Equal(1, messageFeed.Tweets.Count);
        }
                  
    }    
}
