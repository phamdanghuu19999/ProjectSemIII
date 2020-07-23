using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Net.Models.DataModel
{
    public class UserRegisterModel
    {
        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Tên phải từ 8 - 50 kí tự!")]
        [Display(Name = "Tài khoản")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tài khoản!")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Tài khoản phải từ 8 - 16 kí tự!")]
        [Display(Name = "Tài khoản")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu!")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu!")]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp!")]
        [Display(Name = "Xác nhận mật khẩu")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập email!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Vui lòng nhập đúng định dạng email")]
        [RegularExpression(@"[\w]+@[\w]+\.[a-zA-Z]{2,4}", ErrorMessage = "Vui lòng nhập đúng định dạng email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập phone!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Vui lòng nhập đúng định dạng phone")]
        [RegularExpression(@"^[0]{1}\\d{10}$", ErrorMessage = "Vui lòng nhập đúng định dạng phone")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập ngày sinh!")]
        [DataType(DataType.Date, ErrorMessage = "Vui lòng nhập đúng định dạng ngày tháng!")]
        public DateTime Birthday { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ !")]
        public string Address { get; set; }

    }
}