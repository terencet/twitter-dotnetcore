Autonomous service workshop - Session 1: dotnet core hands on
===================


Please follow the steps in this worksheet to create a dotnet core project.

----------


Create Solution and Project Folders
-------------

 1. Open Command Prompt and navigate to your workspace
 2. Clone `git@github.com:PageUpPeopleOrg/twitter-donetcore.git`
 3. `cd twitter-dotnetcore`

Create lib Project
-------------

 1. Navigate to your lib project folder  `cd lib` 
 2. Run `dotnet new -t lib` to create a new dotnet core shared library
 3. Replace *Library.cs* with the following
	

```
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
        if(tweet == null)
          throw new ArgumentNullException("tweet");


        Tweets.Add(tweet);
      }
    }    
}
```


Create Web Project
-------------

 1. Navigate to your web project folder  `cd web`.
 2. Run `dotnet new` to create a new dotnet core project.
 3. Open *project.json* and add WebApi framework references.
```
{
  "version": "1.0.0-*",
  "buildOptions": {
    "debugType": "portable",
    "emitEntryPoint": true
  },
  "dependencies": {
        "Microsoft.AspNetCore.Mvc": "1.0.0",
        "Microsoft.AspNetCore.Server.Kestrel": "1.0.0",
        "Microsoft.Extensions.DependencyInjection": "1.0.0"
  },
  "frameworks": {
    "netcoreapp1.0": {
      "dependencies": {
        "Microsoft.NETCore.App": {
          "type": "platform",
          "version": "1.0.0"
        }
      },
      "imports": "dnxcore50"
    }
  }
}
```

 4. Open *Program.cs* and add ASP.NET web server bootstrap code.
 
 ```
 using System;
	using Microsoft.AspNetCore.Hosting;

	namespace ConsoleApplication
	{
 	   public class Program
    		{
   	     		public static void Main(string[] args)
   	     		{
		           var host = new WebHostBuilder()
		               .UseKestrel()
		               .UseStartup<Startup>()
		               .Build();
	
		           host.Run();
		     }
    		}
	}
	```

 5. Add *Startup.cs* to register and initialize WebApi framework.

```
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApplication
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
```

 6. Create *TwitterController.cs* and paste the following code.
```
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ClassLibrary;


namespace ConsoleApplication
{
  [Route("api/[controller]")]
  public class TwitterController: Controller
  {
    [HttpGet]
    public IEnumerable<Tweet> Get()
    {
       return new List<Tweet>{ new Tweet
       {
         Author = "John",
         Message = "Hi"
       }};
    }


    public void Post(Post post)
    {
    }
  }
}
```
 7. Add reference to your lib project. Copy paste the highlighted line into your *project.json* in web.
```
  "dependencies": {
    "lib": "*",
      "Microsoft.AspNetCore.Mvc": "1.0.0",
      "Microsoft.AspNetCore.Server.Kestrel": "1.0.0",
      "Microsoft.Extensions.DependencyInjection": "1.0.0"
   },
   ```
 8. Run `dotnet restore` on lib and web to restore NuGet packages. Alternatively you can run `dotnet restore` on the parent folder.
 9. Run `dotnet build` to compile (optional).
 10. Run `dotnet run` to start web server.
 11. Test it by running `curl -i http://localhost:5000/api/twitter`.


Create Worker Project
-------------

 1. `cd worker`.
 2. Run `dotnet new` to create a new dotnet core project.
 3. Add the following to *Program.cs*.
 
```
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
              Content = "John, This is the first tweet"
            });
            return messageStream;
        }
    }
}
```

 4. Add reference to your lib project. Copy paste the highlighted line into your *project.json* in web.
 ```
  "dependencies": {
    "lib": "*"
       },
```
 
 5. Run `dotnet restore` to restore NuGet packages.
 6. Run `dotnet build` to compile (optional).
 7. Run `dotnet run` to start the worker service.

Create Test Project
-------------

 1. Navigate to your test folder `cd test`.
 2. Run `dotnet new -t xunittest` to create a new test project.
 3. Open *project.json* and add reference to lib and test watch tools
 
```
{
  "version": "1.0.0-*",
  "buildOptions": {
    "debugType": "portable"
  },
  "dependencies": {
    "System.Runtime.Serialization.Primitives": "4.1.1",
    "xunit": "2.1.0",
    "dotnet-test-xunit": "1.0.0-rc2-192208-24",
    "lib": "*"
  },
  "testRunner": "xunit",
  "frameworks": {
    "netcoreapp1.0": {
      "dependencies": {
        "Microsoft.NETCore.App": {
          "type": "platform",
          "version": "1.0.1"
        }
      },
      "imports": [
        "dotnet5.4",
        "portable-net451+win8"
      ]
    }
  },


  "tools": {
     "Microsoft.DotNet.Watcher.Tools": "1.0.0-preview2-final"
  }  
}
```


 4. Add these tests to your *Tests.cs*

```
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
```

 5. `dotnet restore` 
 6. Run `dotnet watch test` to run the tests