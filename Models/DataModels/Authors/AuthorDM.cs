using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBooks.Models.DataModels
{
    public class AuthorDM
    {
        public decimal AuthorId { get; set; }

        [StringLength(80,ErrorMessage = "El nombre del autor contener entre {1} y {2} caractéres.", MinimumLength = 2)]
        [Required(ErrorMessage = "El nombre del autor es requerido.")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "La fecha de nacimiento del Autor es requerida.")]
        public DateTime Birthday { get; set; }

        [StringLength(65, ErrorMessage = "El nombre del pueblo debe contener entre {1} y {2} caractéres.", MinimumLength = 0)]
        public string Hometown { get; set; }

        [EmailAddress(ErrorMessage = "El email ingresado no es válido.")]
        [Required(ErrorMessage = "El email del autor es requerido.")]
        public string Email { get; set; }
    }
}
