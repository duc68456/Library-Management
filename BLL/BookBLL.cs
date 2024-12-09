using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class BookBLL
    {
        private readonly BookDAL bookDAL = new BookDAL();

        public void AddBook(BookDTO book)
        {
            if (string.IsNullOrEmpty(book.Title))
                throw new Exception("Tên sách không được để trống.");
            bookDAL.AddBook(book);
        }

        public void UpdateBook(BookDTO book)
        {
            bookDAL.UpdateBook(book);
        }
        public void DeleteBook(int bookId)
        {
            if (bookId <= 0)
                throw new Exception("Mã sách không hợp lệ.");
            bookDAL.DeleteBook(bookId);
        }
        public List<BookDTO> GetAllBooks()
        {
            return bookDAL.GetAllBooks();
        }

        //
        public List<BookDTO> GetBooks()
        {
            return bookDAL.GetBooks();
        }
        public BookDTO GetBookById(int bookId)
        {
            return bookDAL.GetBookById(bookId);
        }

    }
}
