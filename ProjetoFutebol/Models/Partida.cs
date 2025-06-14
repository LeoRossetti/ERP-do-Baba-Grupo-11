namespace ProjetoFutebol.Models
{

    public class Partida
    {
        public DateTime Data { get; set; }
        public string Time1 { get; set; }
        public string Time2 { get; set; }
        public string Vencedor { get; set; } // Nome do time vencedor
        public string? Observacoes { get; set; }
        public int GolsTime1 { get; set; }
        public int GolsTime2 { get; set; }

        public Partida(DateTime data, string time1, string time2, string vencedor, int golsTime1, int golsTime2, string? observacoes = null)
        {
            if (vencedor != time1 && vencedor != time2 && vencedor != "Empate")
                throw new ArgumentException("Vencedor deve ser um dos times participantes ou 'Empate'.");

            Data = data;
            Time1 = time1;
            Time2 = time2;
            Vencedor = vencedor;
            GolsTime1 = golsTime1;
            GolsTime2 = golsTime2;
            Observacoes = observacoes;
        }
    }
}