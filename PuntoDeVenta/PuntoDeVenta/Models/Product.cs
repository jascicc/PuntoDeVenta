using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace PuntoDeVenta.Models
{
    public class Product
    {
        [Key]
        [Required]
        [Display(Name = "ID Producto")]
        public int ProductID { get; set; }

        [Required]
        [Display(Name = "Nombre Platillo")]
        [StringLength(50)]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Categoría")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category category { get; set; }

        [Required]
        [Display(Name = "Precio")]
        [DataType(DataType.Currency)]
        public float Price { get; set; }

        [Display(Name = "Imagen")]
        public byte[] Image { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}