using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_store
{
    internal class ListAuthorization
    {
        public string login { get; set; }
        public string password { get; set; }
        public string id { get; set; }
     
        public ListAuthorization(string id, string login, string password)
        {
            this.login = login;
            this.password = password;
            this.id = id;
        }
    }
}
