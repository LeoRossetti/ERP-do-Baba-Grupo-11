using ProjetoFutebol.Models;

namespace ProjetoFutebol.Abstractions
{
    public abstract class JogoBase
    {
        protected List<Jogador> interessados = new();
        public List<Jogador> Interessados => new List<Jogador>(interessados);
        public List<Partida> Partidas { get; set; } = new();
        public DateTime Data { get; protected set; }
        public string Local { get; protected set; } = "";
        public string TipoCampo { get; protected set; } = "";
        public int JogadoresPorTime { get; protected set; }
        public int? LimiteTimes { get; protected set; }

        public abstract bool PodeConfirmarPartida();

        public void RegistrarInteressado(Jogador jogador)
        {
            if (jogador == null)
                throw new ArgumentException("Jogador não pode ser nulo.");
            if (interessados.Any(j => j.Codigo == jogador.Codigo))
                throw new InvalidOperationException("Esse jogador já foi registrado como interessado.");
            interessados.Add(jogador);
        }


        public List<Jogador> ListarInteressados()
        {
            return new List<Jogador>(interessados);
        }
    }
}
