using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealWorld.Models
{
    /*{
  "errors":{
    "body": [
      "can't be empty"
    ]
  }
}*/
    public class Error
    {
        public IDictionary<string, string[]> errors { get; set; }
    }

}
