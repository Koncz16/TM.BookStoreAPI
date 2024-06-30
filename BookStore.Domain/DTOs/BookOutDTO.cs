using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.DTOs
{
    public class BookOutDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public List<string> Genres { get; set; }
        public AuthorDto Author { get; set; }
    }
}
