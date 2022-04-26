using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace TOTINH.Models
{
    public class LoiNhan
    {
        
        [Required(AllowEmptyStrings = false,ErrorMessage = "không được bỏ trống")]
        [StringLength(255, ErrorMessage = "Không được nhập quá 255 kí tự", MinimumLength = 1)]
        public string yourname { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "không được bỏ trống")]
        [StringLength(255, ErrorMessage = "Không được nhập quá 255 kí tự",MinimumLength = 1)]
        //[RegularExpression(@"[_-,/`~().\r]",ErrorMessage = "Không có các kí tự đặc biệt")]
        
        //khai báo biến cho tên và tin nhắn
        public string message { get; set; }

        public int id_message;

        //khai báo mật khẩu
        [Required(AllowEmptyStrings = false, ErrorMessage = "không được bỏ trống")]
        [StringLength(255, ErrorMessage = "Không được nhập quá 255 kí tự", MinimumLength = 1)]
        public string password { get; set; }

        //khai báo chuỗi kết nối sql

        private string ConSqlStr = @"Data Source=DESKTOP-63PGR1N\SQLEXPRESS;Initial Catalog=NHANGUIYEUTHUONG;Integrated Security=True";

        //hàm thêm tin nhắn vào data
        public void add_message (string name, string message)
        {

            //tạo đối tượng random
            Random random = new Random();            
            int ma = 1;
            //kiểm tra mã vừa random
            using (SqlConnection sql = new SqlConnection(ConSqlStr))
            {
                sql.Open();
                while (true)
                {

                    
                    //tạo mã
                    ma = random.Next(1, 1000);
                    //truy vấn để kiểm tra mã
                    SqlCommand sqlcmd1 = new SqlCommand($"select * from LOINHAN where id_message = {ma}", sql);
                    var reader = sqlcmd1.ExecuteReader();
                    //nếu ko có thì dừng lại
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        
                        break;
                    }
                    else
                    {
                        reader.Close();
                        
                    }
                }
                //thêm mã
                SqlCommand sqlcmd = new SqlCommand($"INSERT INTO LOINHAN Values (@name,@message,'{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}',{ma})", sql);
                id_message = ma;
                sqlcmd.Parameters.Add("name",SqlDbType.NVarChar).Value = name;
                sqlcmd.Parameters.Add("message", SqlDbType.NVarChar).Value = message;
                sqlcmd.ExecuteNonQuery();
                sql.Close();
            }
               
        }

        //hàm lấy tin nhắn
        public string get_message(int id)
        {
            //mở kết nối sql
            using(SqlConnection sql = new SqlConnection(ConSqlStr))
            {
                sql.Open();
                SqlCommand sqlcmd = new SqlCommand($"select message from LOINHAN where id_message = {id}", sql);
                string reader = sqlcmd.ExecuteScalar().ToString();
                return reader;
            }
        }

        //hàm lấy tên
        public string get_password(int id)
        {
            using (SqlConnection sql = new SqlConnection(ConSqlStr))
            {
                sql.Open();
                SqlCommand sqlcmd = new SqlCommand($"select name from LOINHAN where id_message = {id}", sql);
                string reader = sqlcmd.ExecuteScalar().ToString();
                return reader;
            }
        }


        
    }
}