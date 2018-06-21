using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PuntoDeVenta.Models
{
    public class Table
    {
        [Key]
        public int TableId { get; set; }

        [Required]
        [Display(Name = "Mesa")]
        public int TableNumber { get; set; }
    }
}