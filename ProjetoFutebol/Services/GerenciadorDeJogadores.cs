using ProjetoFutebol.Models;
using ProjetoFutebol.Utils;
namespace ProjetoFutebol.Services
{
    public class GerenciadorDeJogadores
    {
        private List<Jogador> jogadores;

        public GerenciadorDeJogadores()
        {
            jogadores = JsonStorage.CarregarJogadores();
        }

        public void AdicionarJogador(Jogador jogador)
        {
            if (jogadores.Any(j => j.Codigo == jogador.Codigo))
                throw new ArgumentException("Já existe um jogador com este código.");
            jogadores.Add(jogador);
            JsonStorage.SalvarJogadores(jogadores);
        }

        public List<Jogador> ListarJogadores()
        {
            return new List<Jogador>(jogadores);
        }

        public Jogador? BuscarPorCodigo(int codigo)
        {
            return jogadores.FirstOrDefault(j => j.Codigo == codigo);
        }

        public void AtualizarJogador(Jogador atualizado)
        {
            var jogador = BuscarPorCodigo(atualizado.Codigo);
            if (jogador == null)
                throw new KeyNotFoundException("Jogador não encontrado.");
            jogador.Nome = atualizado.Nome;
            jogador.Idade = atualizado.Idade;
            jogador.Posicao = atualizado.Posicao;
            JsonStorage.SalvarJogadores(jogadores);
        }

        public void RemoverJogador(int codigo)
        {
            var jogador = BuscarPorCodigo(codigo);
            if (jogador != null)
            {
                jogadores.Remove(jogador);
                JsonStorage.SalvarJogadores(jogadores);
            }
        }
    }
}
