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

 4. Open *Program.cs* an
 5. d add ASP.NET web server bootstrap code.
 5. Add *Startup.cs* to register and initialize WebApi framework.
 6. Create *TwitterController.cs* and paste the following code.
 7. Add reference to your lib project. Copy paste the highlighted line into your *project.json* in web.
 8. Run `dotnet restore` on lib and web to restore NuGet packages. Alternatively you can run `dotnet restore` on the parent folder.
 9. Run `dotnet build` to compile (optional).
 10. Run `dotnet run` to start web server.
 11. Test it by running `curl -i http://localhost:5000/api/twitter`.


Create Worker Project
-------------

 1. `cd worker`.
 2. Run `dotnet new` to create a new dotnet core project.
 3. Add the following to *Program.cs*.
 4. Add reference to your lib project. Copy paste the highlighted line into your *project.json* in web.
 5. Run `dotnet restore` to restore NuGet packages.
 6. Run `dotnet build` to compile (optional).
 7. Run `dotnet run` to start the worker service.

Create Test Project
-------------

 1. Navigate to your test folder `cd test`.
 2. Run `dotnet new -t xunittest` to create a new test project.
 3. Open *project.json* and add reference to lib and test watch tools
 4. Add these tests to your *Tests.cs*
 5. `dotnet restore` 
 6. Run `dotnet watch test` to run the tests