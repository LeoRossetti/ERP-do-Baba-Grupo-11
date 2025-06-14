using ProjetoFutebol.Models;
using ProjetoFutebol.Abstractions;
using ProjetoFutebol.Utils;
namespace ProjetoFutebol.Services
{
    public class GerenciadorDeJogos
    {
        private List<Jogo> jogos;

        public GerenciadorDeJogos()
        {
            jogos = JsonStorage.CarregarJogos();
        }

        public void AdicionarJogo(Jogo jogo)
        {
            if (!jogos.Any(j => j.Data == jogo.Data && j.Local == jogo.Local))
            {
                jogos.Add(jogo);
                SalvarTodosJogos();
            }
        }

        public List<Jogo> ListarJogos()
        {
            // Retorna a lista real para garantir referência e persistência corretas
            return jogos;
        }

        public void SalvarTodosJogos()
        {
            JsonStorage.SalvarJogos(jogos);
        }

        public void RegistrarInteressado(Jogo jogo, Jogador jogador)
        {
            jogo.RegistrarInteressado(jogador);
            SalvarTodosJogos();
        }

        public void RemoverJogo(Jogo jogo)
        {
            jogos.Remove(jogo);
            SalvarTodosJogos();
        }

        public void AdicionarPartida(Jogo jogo, Partida partida)
        {
            jogo.Partidas.Add(partida);
            SalvarTodosJogos();
        }
    }
}
