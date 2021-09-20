using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagazineStore.Core.Entidades;

namespace MagazineStore.Teste.Entidade
{
    public abstract class BaseEntidadeTest
    {
        protected KeyValuePair<bool, List<ValidationResult>> ValidarEntidade<T>(T entidade) where T : EntidadeBase
        {
            var context = new ValidationContext(entidade);
            var results = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(entidade, context, results, true);
            var retorno = new Dictionary<bool, List<ValidationResult>> { { actual, results } };
            return retorno.First();
        }
    }
}
