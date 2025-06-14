using ProjetoFutebol.Abstractions;
namespace ProjetoFutebol.Models

{
    public class Jogo : JogoBase
    {
        public Jogo(DateTime data, string local, string tipoCampo, int jogadoresPorTime, int? limiteTimes = null)
        {
            if (jogadoresPorTime < 1)
                throw new ArgumentException("Jogadores por time deve ser pelo menos 1");

            if (string.IsNullOrWhiteSpace(local))
                throw new ArgumentException("O local do jogo não pode ser vazio.");

            Data = data;
            Local = local;
            TipoCampo = tipoCampo;
            JogadoresPorTime = jogadoresPorTime;
            LimiteTimes = limiteTimes;
            Partidas = new List<Partida>();
        }

        public new DateTime Data { get => base.Data; set => base.Data = value; }
        public new string Local { get => base.Local; set => base.Local = value; }
        public new string TipoCampo { get => base.TipoCampo; set => base.TipoCampo = value; }
        public new int JogadoresPorTime { get => base.JogadoresPorTime; set => base.JogadoresPorTime = value; }
        public new int? LimiteTimes { get => base.LimiteTimes; set => base.LimiteTimes = value; }

        // Propriedade pública para serialização correta dos interessados
        public List<Jogador> Interessados
        {
            get => interessados;
            set => interessados = value ?? new List<Jogador>();
        }

        public override bool PodeConfirmarPartida()
        {
            int totalJogadores = interessados.Count;
            int jogadoresPorTimeCompleto = JogadoresPorTime;

            if (LimiteTimes.HasValue)
            {
                int maxJogadores = LimiteTimes.Value * jogadoresPorTimeCompleto;
                if (totalJogadores > maxJogadores)
                    throw new InvalidOperationException("Número de interessados excede o limite de jogadores.");
            }

            return totalJogadores >= JogadoresPorTime * 2;
        }
    }
}
