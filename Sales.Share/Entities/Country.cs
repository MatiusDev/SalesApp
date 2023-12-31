using System.ComponentModel.DataAnnotations;

namespace Sales.Share.Entities
{
    public class Country
    {
        #region Props
        public int Id { get; set; }

        [Display(Name = "País")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caractéres")]
        public string? Name { get; set; }
        #endregion

    }
}
