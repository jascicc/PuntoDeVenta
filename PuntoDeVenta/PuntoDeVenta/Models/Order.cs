using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoDeVenta.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        [Display(Name ="Fecha")]
        public DateTime Date { get; set; }
        
        [Required]
        [Display(Name ="Mesa")]
        public int TableId { get; set; }

        [ForeignKey("TableId")]
        public Table Table { get; set; }


        [Required]
        [Display(Name ="Producto")]
        public int ProductID { get; set; }

        [ForeignKey("ProductID")]
        public Product Product { get; set; }

        [Required]
        [Display(Name ="Cantidad")]
        public int Quantity { get; set; }

        [Required]
        public string Active { get; set; }

    }
}