namespace ProjetoFutebol.Abstractions
{
    public abstract class JogoBase
    {
        protected List<string> interessados = new();
        public DateTime Data { get; protected set; }
        public string Local { get; protected set; } = "";
        public string TipoCampo { get; protected set; } = "";
        public int JogadoresPorTime { get; protected set; }
        public int? LimiteTimes { get; protected set; }

        public abstract bool PodeConfirmarPartida();

        public void RegistrarInteressado(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome do interessado não pode ser vazio.");

            if (interessados.Contains(nome))
                throw new InvalidOperationException("Esse interessado já foi registrado.");

            interessados.Add(nome);
        }


        public List<string> ListarInteressados()
        {
            return new List<string>(interessados);
        }
    }
}
