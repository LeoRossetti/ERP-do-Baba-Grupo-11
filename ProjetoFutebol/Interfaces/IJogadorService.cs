using ProjetoFutebol.Models;
namespace ProjetoFutebol.Interfaces
{
    public interface IJogadorService
    {
        void AdicionarJogador(Jogador jogador);
        void RemoverJogador(int codigo);
        List<Jogador> ListarJogadores();
        List<Jogador> Classificacao();
    }
}