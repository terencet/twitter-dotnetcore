using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ClassLibrary;

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

}
