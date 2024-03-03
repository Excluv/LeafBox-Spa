using System.ComponentModel.DataAnnotations;

namespace HTQLSpa.Models
{
    public class Employee
    {
        [Key]
        [Display(Name = "Mã Nhân viên")]
        public int EmployeeID { get; set; }

        [Required]
        [Display(Name = "Họ & Tên Nhân viên")]
        public string EmployeeName { get; set; }

        [Required]
        [Display(Name = "Ngày sinh"), 
         DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}"),
         DataType(DataType.Date)]
        public DateTime DoB { get; set; }

        [Required]
        [Display(Name = "Địa chỉ")]
        public string EmployeeAddress { get; set; }

        [Required]
        [Display(Name = "SĐT")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Chức vụ")]
        public string Position { get; set; }

        [Required]
        [Display(Name = "Tên đăng nhập")]
        public string AccountID { get; set; }

        [Required]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Loại Tài khoản")]
        public string Role { get; set; }
    }
}
