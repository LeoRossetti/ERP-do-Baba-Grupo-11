namespace Models;
public class Jogo : JogoBase
{
    public Jogo(DateTime data, string local, string tipoCampo, int jogadoresPorTime, int? limiteTimes = null)
    {
        if (jogadoresPorTime < 1)
            throw new ArgumentException("Jogadores por time deve ser pelo menos 1");

        Data = data;
        Local = local;
        TipoCampo = tipoCampo;
        JogadoresPorTime = jogadoresPorTime;
        LimiteTimes = limiteTimes;
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