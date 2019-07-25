using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyFirstMVC.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name ="Kategori Adı")]
        public string CategoryName { get; set; }


        public List<Project> Projects { get; set; }
    }
}