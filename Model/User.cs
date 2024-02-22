using System.Collections.Generic;
using System.Net;

namespace WebConnectMongoDBAndSQL.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
