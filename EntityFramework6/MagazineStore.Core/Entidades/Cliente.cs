using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagazineStore.Core.Entidades.Enum;
using MagazineStore.Utils.Attributes;

namespace MagazineStore.Core.Entidades
{
    [Table("tbCliente")]
    public class Cliente : EntidadeBase, IValidatableObject
    {
        public Cliente()
        {
            sID = Guid.NewGuid().ToString();
        }


        [StringLength(100)]
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string sNome { get; set; }

        public string sNomeTratamento { get; set; }

        [StringLength(11)]
        [CPF(ErrorMessage = "Informe um CPF válido")]
        public string sCPF { get; set; }

        [EmailAddress]
        public string sEmail { get; set; }

        [Required(ErrorMessage = "Endereço é obrigatório")]
        public Endereco Endereco { get; set; }

        [Range(1, 2, ErrorMessage = "Informe o sexo")]
        public Sexo Sexo { get; set; }

        public ICollection<Movimentacao> Movimentacoes { get; set; }

        #region Implementation of IValidatableObject

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (sNome == sNomeTratamento)
            {
                yield return new ValidationResult("Nome não pode ser igual ao Nome de tratamento", new[] { "sNome", "sNomeTratamento" });
            }
        }
        #endregion
    }
}
