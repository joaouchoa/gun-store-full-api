using GunCatalog.Domain.Enumerable;
using System.ComponentModel.DataAnnotations;

namespace GunCatalog.Domain.ImputModel
{
    public class GunImputModel
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O nome do jogo deve conter entre 2 e 50 carateres")]
        public string Modelo { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O nome do jogo deve conter entre 2 e 50 carateres")]
        public string Fabricante { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O nome do jogo deve conter entre 2 e 50 carateres")]
        public string  NumeroDeSerie{ get; set; }

        [Required]
        [Range(1,50, ErrorMessage = "A capacidade não pode ser abaixo de 1 e nem acima de 50")]
        public int Capacidade { get; set; }

        [Required]
        public double Preco { get; set; }

        [Required]
        public ECalibre Calibre { get; set; }

    }
}
