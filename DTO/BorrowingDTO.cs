using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BorrowingDTO
    {
        public int BorrowingId { get; set; }
        public int BookId { get; set; }
        public int ReaderId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
