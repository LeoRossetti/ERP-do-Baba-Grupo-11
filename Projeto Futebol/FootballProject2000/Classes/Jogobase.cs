namespace Models;
public abstract class JogoBase : IRegistravel
{
    public int Codigo { get; set; }
    public DateTime Data { get; set; }
    public string Local { get; set; }
    public string TipoCampo { get; set; }
    public int JogadoresPorTime { get; set; }
    public int? LimiteTimes { get; set; }

    protected List<string> interessados = new List<string>();

    public void RegistrarInteressado(string nome)
    {
        interessados.Add(nome);
    }

    public List<string> ListarInteressados()
    {
        return new List<string>(interessados);
    }

    public abstract bool PodeConfirmarPartida();
}