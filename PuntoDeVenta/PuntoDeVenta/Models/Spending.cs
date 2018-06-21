using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using PuntoDeVenta.Models;

namespace PuntoDeVenta.Models
{
    public class Spending
    {
        [Key]
        [Required]
        public int SpendingID { get; set; }
        [Display(Name = "Concepto")]
        [Required]
        public string Description { get; set; }
        [Display(Name = "Costo")]
        [DataType(DataType.Currency)]
        [Required]
        public float Cost { get; set; }
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime Date { get; set; }
    }
}