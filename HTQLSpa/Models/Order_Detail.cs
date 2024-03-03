using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTQLSpa.Models
{
    public class Order_Detail
    {
        [Key]
        [Display(Name = "Mã số")]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Số lượng")]
        public int Amount { get; set; }

        [Required]
        [Display(Name = "Mã Đơn hàng")]
        [ForeignKey("Order")]
        public int OrderID { get; set; }
        public Order Order { get; set; }

        [Required]
        [Display(Name = "Mã Sản phẩm")]
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public Product Product { get; set; }

    }
}
