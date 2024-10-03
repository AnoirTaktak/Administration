﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Administration.Models
{
    public class Service
    {
        [Key]
        public int ID_Service { get; set; }
        [Required]
        [StringLength(50)]
        public required string Designation_Service { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,3)")]
        public decimal PrixHT { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,3)")]
        public decimal PrixTTC { get; set; }
        [Required]
        public int TVA { get; set; }
    }
}