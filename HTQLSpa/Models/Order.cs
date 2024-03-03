using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTQLSpa.Models
{
    public class Order
    {
        [Key]
        [Display(Name = "Mã Đơn hàng")]
        public int OrderID { get; set; }

        [Display(Name = "Ngày lập"),
         DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}"),
         DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        [Required]
        [Display(Name = "Tổng tiền")]
        public float OrderTotal { get; set; }

        [Required]
        [Display(Name = "Thanh toán")]
        public bool Paid { get; set; }

        [Required]
        [Display(Name = "Hoàn thành")]
        public bool Completed { get; set; }

        [Required]
        [ForeignKey("Customer")]
        [Display(Name = "Mã Khách hàng")]
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }

        [Display(Name = "Mã Nhân viên")]
        public int EmployeeID { get; set; }
    }
}
