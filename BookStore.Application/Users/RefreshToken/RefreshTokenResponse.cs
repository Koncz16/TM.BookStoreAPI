using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Users.RefreshToken
{
    public class RefreshTokenResponse
    {
        public bool IsSuccessful { get; set; }
        public string AccessToken { get; set; }
    }
}
