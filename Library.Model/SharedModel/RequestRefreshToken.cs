using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class RequestRefreshToken
    {
        public string UserID { get; set; }
        public string RefreshToken { get; set; }

    }
}
