using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Users.AddBookToUser
{
    public class AddBookToUserResponse
    {
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
    }
}
