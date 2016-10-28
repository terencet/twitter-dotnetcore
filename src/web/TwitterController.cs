using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Web
{
  [Route("api/[controller]")]
  public class TwitterController: Controller
  {
    [HttpGet]
    public IEnumerable<Tweet> Get()
    {
       return new List<Tweet>{ new Tweet
       {
         Author = "Ceola",
         Message = "Hi"
       }};
    }

    public void Post(Post post)
    {
    }
  }

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
}
