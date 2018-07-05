using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PuntoDeVenta.Models
{
    public class StaticConfig
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Dólar")]
        [DataType(DataType.Currency)]
        public float Dollar { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "RFC")]
        public string RFC { get; set; }

        [Required]
        [Display(Name = "Dirección")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber)]
        public long Phone { get; set; }

    }
}