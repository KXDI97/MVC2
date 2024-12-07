using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC2.Models
{
    public class User
    {
        

        [Key]// llave//
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//identity aunto incremental//
        public int User_ID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = " Este campo es obligaorio")] //requerido sin espacios//
        [StringLength(50, ErrorMessage = " la cantidad max de caracteres es de 50")] //numero de caracteres//
        public string Username { get; set; }

        [Required(ErrorMessage = " Este campo es obligaorio")]
        [StringLength(50, ErrorMessage = " la cantidad max de caracteres es de 50")]
        public string Password { get; set; }

        [Required(ErrorMessage = " Este campo es obligaorio")]
        public DateTime Creation_Date { get; set; } = DateTime.Now;

        public byte[] HashKey { get; set; } 
        public byte[] HashIV { get; set; } 
    }
}