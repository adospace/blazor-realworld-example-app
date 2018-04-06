using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealWorld.Models
{
    /*
     {
  "user": {
    "email": "jake@jake.jake",
    "token": "jwt.token.here",
    "username": "jake",
    "bio": "I work at statefarm",
    "image": null
  }
}*/
    public class User : IEquatable<User>
    {
        public string email { get; set; }
        public string token { get; set; }
        public string username { get; set; }
        public string bio { get; set; }
        public string image { get; set; }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as User);
        }

        public bool Equals(User other)
        {
            if (other == null)
                return false;

            return
                this.email == other.email &&
                this.token == other.token &&
                this.username == other.username &&
                this.image == other.image &&
                this.bio == other.bio;
        }

        public override int GetHashCode()
        {
            var hashCode = -45217915;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(token);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(username);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(bio);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(image);
            return hashCode;
        }

        public static bool operator ==(User obj1, User obj2)
        {
            if (ReferenceEquals(obj1, obj2))
            {
                return true;
            }

            if (obj1 is null)
            {
                return false;
            }
            if (obj2 is null)
            {
                return false;
            }

            return obj1.Equals(obj2);
        }

        public static bool operator !=(User obj1, User obj2)
        {
            return !(obj1 == obj2);
        }

    }

    public class SingleUser
    {
        public User user { get; set; }
    }
}
