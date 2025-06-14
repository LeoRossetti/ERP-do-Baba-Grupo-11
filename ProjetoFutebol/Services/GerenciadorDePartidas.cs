using ProjetoFutebol.Models;
using ProjetoFutebol.Utils;
namespace ProjetoFutebol.Services
{
    public class GerenciadorDePartidas
    {
        private List<Partida> historico = new List<Partida>();
        private Dictionary<string, int> jogosPorTime = new Dictionary<string, int>();
        private ModoPartida modoAtual;

        public GerenciadorDePartidas(ModoPartida modo)
        {
            modoAtual = modo;
            historico = new List<Partida>(); // Não carrega mais do JSON
        }

        public void RegistrarPartida(Partida partida)
        {
            historico.Add(partida);
            if (!jogosPorTime.ContainsKey(partida.Time1)) jogosPorTime[partida.Time1] = 0;
            if (!jogosPorTime.ContainsKey(partida.Time2)) jogosPorTime[partida.Time2] = 0;
            jogosPorTime[partida.Time1]++;
            jogosPorTime[partida.Time2]++;
        }

        public List<Partida> ObterHistorico()
        {
            return new List<Partida>(historico);
        }

        public string? ProximoTime(string timeAnterior, string vencedor)
        {
            switch (modoAtual)
            {
                case ModoPartida.QuemGanhaFica:
                    return vencedor;
                case ModoPartida.DoisJogosPorTime:
                    if (jogosPorTime.ContainsKey(timeAnterior) && jogosPorTime[timeAnterior] >= 2)
                        return null;
                    return vencedor;
                default:
                    return null;
            }
        }

        public void Redefinir()
        {
            historico.Clear();
            jogosPorTime.Clear();
        }
    }
}
