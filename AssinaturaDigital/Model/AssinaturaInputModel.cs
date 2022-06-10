using System.ComponentModel.DataAnnotations;

namespace AssinaturaDigital.Model
{
    public class AssinaturaInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "O Nome deve conter entre 4 e 100 caracteres")]
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Url { get; set; }
        public string Cargo { get; set; }
        public string Empresa { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
        public string Endereco { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }

    }
}
