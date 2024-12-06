using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC2.Models
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Autoincremental
        public int Person_ID { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
        public string Person_Name { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(50, ErrorMessage = "El apellido no puede exceder los 50 caracteres")]
        public string Person_Last_Name { get; set; }

        [Required(ErrorMessage = "El número de identificación es obligatorio")]
        [StringLength(50, ErrorMessage = "El número de identificación no puede exceder los 50 caracteres")]
        public string ID_Number { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [StringLength(50, ErrorMessage = "El correo no puede exceder los 50 caracteres")]
        [EmailAddress(ErrorMessage = "Debe proporcionar un correo válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El tipo de identificación es obligatorio")]
        [StringLength(50, ErrorMessage = "El tipo de identificación no puede exceder los 50 caracteres")]
        public string ID_Type { get; set; }

        [Required(ErrorMessage = "La fecha de creación es obligatoria")]
        public DateTime Creation_Date { get; set; }
    }
}