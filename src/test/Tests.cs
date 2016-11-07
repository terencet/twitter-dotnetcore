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
            public Tweet Tweet {get; set;}
        }

        public MessageStreamData GetData()
        {
            return new MessageStreamData
            {
                Post = new Post(),
                Tweet = new Tweet()
            };
        }

        [Fact]
        public void Queue_NullArgument_ThrowsArgumentNullException() 
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
        public void Queue_ValidArgument_StreamHasPosts() 
        {
            //ARRANGE
            var testData = GetData();
            var messageStream = new MessageStream();

            //ACT
            messageStream.Queue(testData.Post);

            //ASSERT
            Assert.True(messageStream.HasPosts);
        }        
    }
}
