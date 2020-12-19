using System;
using System.Collections;
namespace Bingo.Models
{
    public class AuthResponse
    {
        public long Id{ get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
    }

}