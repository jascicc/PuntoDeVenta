using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoDeVenta.Models
{
    public class Category
    {
        [Key]
        [Required]
        [Display(Name = "ID Categoría")]
        public int CategoryId { get; set; }
        [Required]
        [Display(Name = "Nombre Categoría")]
        [StringLength(50)]
        public string CategoryName { get; set; }
        //public ICollection<Product> productos { get; set; }
    }
}