﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DataLayer
{
    public class Page
    {
        [Key]
        public int PageID { get; set; }
        [Display(Name = "کروه صفحه")]
        [Required(ErrorMessage = "لطقا {0} را وارد کنید")]
        public int GroupID { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطقا {0} را وارد کنید")]
        [MaxLength(250)]
        public string Title { get; set; }
        [Display(Name = "توضیح مختصر")]
        [Required(ErrorMessage = "لطقا {0} را وارد کنید")]
        [MaxLength(350)]
        [DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }
        [Display(Name = "متن")]
        [Required(ErrorMessage = "لطقا {0} را وارد کنید")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Text { get; set; }
        [Display(Name = "بازدید")]
        public int Visit { get; set; }
        [Display(Name = "تصویر")]
        public string ImageName { get; set; }
        [Display(Name = "اسلایدر")]
        public bool ShowInSlider { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        [DisplayFormat(DataFormatString ="{0:yyyy/mm/dd}")]
        public DateTime CreateDate { get; set; }
        [Display(Name ="کلمات کلیدی")]
        public string Tags { get; set; }

        public virtual PageGroup PageGoroup { get; set; }

        public virtual List<PageComment> pageComments { get; set; }
        public Page()
        {

        }
    }
}