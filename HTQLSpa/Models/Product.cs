using System.ComponentModel.DataAnnotations;

namespace HTQLSpa.Models
{
    public class Product
    {
        [Key]
        [Display(Name = "Mã Sản phẩm")]
        public int ProductID { get; set; }

        [Required]
        [Display(Name = "Tên Sản phẩm")]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Mô tả")]
        public string ProductDescription { get; set; }

        [Required]
        [Display(Name = "Đơn giá")]
        public float ProductPrice { get; set; }

        [Required]
        [Display(Name = "Đường dẫn Ảnh")]
        public string ImagePath { get; set; }

        [Required]
        [Display(Name = "Đường dẫn Ảnh (Giỏ hàng)")]
        public string BasketImagePath { get; set; }

        [Required]
        [Display(Name = "Đường dẫn ảnh (Xem thêm)")]
        public string DetailImagePath { get; set; }

    }
}
