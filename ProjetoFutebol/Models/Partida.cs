namespace ProjetoFutebol.Models
{

    public class Partida
    {
        public DateTime Data { get; set; }
        public string Time1 { get; set; }
        public string Time2 { get; set; }
        public string Vencedor { get; set; } // Nome do time vencedor
        public string? Observacoes { get; set; }
        // Novo: lista de jogadores que pontuaram e seus pontos
        public List<(int CodigoJogador, string NomeJogador, int Pontos)> Pontuacoes { get; set; }
        public int GolsTime1 { get; set; } // Novo campo para gols do time 1
        public int GolsTime2 { get; set; } // Novo campo para gols do time 2

        public Partida(DateTime data, string time1, string time2, string vencedor, string? observacoes = null, List<(int, string, int)>? pontuacoes = null, int golsTime1 = 0, int golsTime2 = 0)
        {
            if (vencedor != time1 && vencedor != time2)
                throw new ArgumentException("Vencedor deve ser um dos times participantes.");

            Data = data;
            Time1 = time1;
            Time2 = time2;
            Vencedor = vencedor;
            Observacoes = observacoes;
            Pontuacoes = pontuacoes ?? new List<(int, string, int)>();
            GolsTime1 = golsTime1;
            GolsTime2 = golsTime2;
        }
    }
}