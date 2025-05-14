using ProjetoFutebol.Models;
using ProjetoFutebol.Abstractions;
namespace ProjetoFutebol.Services
{
    public class GerenciadorDeJogos
    {
        private List<Jogo> jogos = new List<Jogo>();

        public void AdicionarJogo(Jogo jogo)
        {
            jogos.Add(jogo);
        }

        public List<Jogo> ListarJogos()
        {
            return new List<Jogo>(jogos);
        }

        public void RemoverJogo(Jogo jogo)
        {
            jogos.Remove(jogo);
        }

        public void RegistrarInteressado(Jogo jogo, string nome)
        {
            jogo.RegistrarInteressado(nome);
        }

    }
}
