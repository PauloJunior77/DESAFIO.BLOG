using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESAFIO.BLOG.Domain.Entities
{
    public class Chat
    {
        public Guid Id { get; set; }
        public string NameRoom { get; set; }
        public int LenghtRoom { get; set; }
    }
}
