﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities
{
    [Table("Menu")]
    public class Menu
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}