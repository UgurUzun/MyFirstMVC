using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyFirstMVC.Models
{
    public class Project
    {
        public int Id { get; set; }         
        [Required]
        [MaxLength(100)]
        [Display(Name ="Proje")]
        public string Title { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Açıklama")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DataType(DataType.Html)]
        [Display(Name = "Body")]
        public string Body { get; set; }
        [Display(Name = "Resim")]
        public string Photo { get; set; }

        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

    }
}