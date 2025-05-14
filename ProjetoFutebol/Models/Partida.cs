namespace ProjetoFutebol.Models
{

    public class Partida
    {
        public DateTime Data { get; set; }
        public string Time1 { get; set; }
        public string Time2 { get; set; }
        public string Vencedor { get; set; } // Nome do time vencedor
        public string? Observacoes { get; set; }

        public Partida(DateTime data, string time1, string time2, string vencedor, string? observacoes = null)
        {
            if (vencedor != time1 && vencedor != time2)
                throw new ArgumentException("Vencedor deve ser um dos times participantes.");

            Data = data;
            Time1 = time1;
            Time2 = time2;
            Vencedor = vencedor;
            Observacoes = observacoes;
        }
    }
}