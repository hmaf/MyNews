using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class AdminLogin
    { 
       [Key]
        public int LoginID { get; set; }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage ="لطقا {0} را وارد کنید")]        
        [MaxLength(200)]

        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطقا {0} را وارد کنید")]
        [MaxLength(250)]
        public string Email { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطقا {0} را وارد کنید")]
        [MaxLength(200)]
        public string PassWord { get; set; }
    }
}
