﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyFirstMVC.Models
{
    public class ContactViewModel
    {
        [Display(Name="Ad")]
        [MaxLength(50)]
        [Required(ErrorMessage ="Ad alanı gereklidir.")]
        public string FirstName { get; set; }
        [Display(Name = "Soyad")]
        [MaxLength(50)]
        [Required(ErrorMessage = "Soyad alanı gereklidir.")]
        public string LastName { get; set; }
        [Display(Name = "Telephone")]
        [MaxLength(20)]
        [Required]
        [Phone]
        public string TelePhone { get; set; }
        [Display(Name = "E-Posta")]
        [MaxLength(100)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Mesaj")]
        [MaxLength(4000)]
        [Required]       
        public string Message { get; set; }
        [Display(Name = "Kvkk")]
        [Required]
        public bool PrivacyPolicyAccepted { get; set; } //checkbox
    }
}