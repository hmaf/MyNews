using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class LoginViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطقا {0} را وارد کنید")]
        [MaxLength(200)]
        public string UserName { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطقا {0} را وارد کنید")]
        [MaxLength(200)]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
        [Display(Name ="مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }
}
