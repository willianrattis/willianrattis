
namespace MagazineStore.Utils.Mensagem
{
    public interface IEnviarEmail
    {
        bool notificarCriacaoConta(string email, string nomeTratamento);
    }
}
