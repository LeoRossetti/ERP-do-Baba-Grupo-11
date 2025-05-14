namespace ProjetoFutebol.Interfaces
{
    public interface IRegistravel
    {
        void RegistrarInteressado(string nome);
        List<string> ListarInteressados();
    }
}