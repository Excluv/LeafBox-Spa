using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HTQLSpa.Models
{
    public class Customer
    {
        [Key]
        [Display(Name = "Mã Khách hàng")]
        public int CustomerID { get; set; }

        [Required]
        [Display(Name = "Họ & Tên Khách hàng")]
        public string CustomerName { get; set; }

        [Required]
        [Display(Name = "Ngày sinh"), 
         DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}"),
         DataType(DataType.Date)]
        public DateTime DoB { get; set; }

        [Required]
        [Display(Name = "Địa chỉ")]
        public string CustomerAddress { get; set; }

        [Required]
        [Display(Name = "SĐT")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Tên đăng nhập")]
        public string AccountID { get; set; }

        [Required]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
    }
}
