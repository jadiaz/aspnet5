﻿using System.ComponentModel.DataAnnotations;

namespace MvcApplication.Models
{
    public class User
    {
        [Required]
        [MinLength(4)]
        public string Name { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
    }
}