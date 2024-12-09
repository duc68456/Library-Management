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
    public class ReaderBLL
    {
        private ReaderDAL readerDAL = new ReaderDAL();

        public List<ReaderDTO> GetAllReaders()
        {
            return readerDAL.GetAllReaders();
        }

        public bool AddReader(ReaderDTO reader)
        {
            if (string.IsNullOrEmpty(reader.Name) || string.IsNullOrEmpty(reader.IDNumber))
            {
                throw new ArgumentException("Name and IDNumber are required.");
            }
            return readerDAL.AddReader(reader);
        }

        public void UpdateReader(ReaderDTO reader)
        {
            if (reader.ReaderId <= 0 || string.IsNullOrEmpty(reader.Name) || string.IsNullOrEmpty(reader.IDNumber))
                throw new Exception("Dữ liệu độc giả không hợp lệ.");

            readerDAL.UpdateReader(reader); // Gọi hàm cập nhật từ DAL
        }

        public void DeleteReader(int readerId)
        {
            if (readerId <= 0)
                throw new Exception("Mã độc giả không hợp lệ.");
            readerDAL.DeleteReader(readerId); // Gọi hàm xóa từ DAL
        }
        public ReaderDTO GetReaderById(int readerId)
        {
            if (readerId <= 0)
                throw new Exception("Mã độc giả không hợp lệ.");

            return readerDAL.GetReaderById(readerId);
        }
    }
}
