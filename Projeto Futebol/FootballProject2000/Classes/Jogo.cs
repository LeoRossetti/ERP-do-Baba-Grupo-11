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

    private static List<Jogo> jogos = new List<Jogo>();
    private static int proximoCodigo = 1;

    public static void Cadastrar()
    {
        Console.Write("Data do jogo (dd/MM/yyyy): ");
        DateTime data = DateTime.Parse(Console.ReadLine());

        Console.Write("Local do jogo: ");
        string local = Console.ReadLine();

        Console.Write("Tipo de campo: ");
        string tipoCampo = Console.ReadLine();

        Console.Write("Jogadores por time: ");
        int jogadoresPorTime = int.Parse(Console.ReadLine());

        Console.Write("Limite de times (opcional, pressione Enter para ignorar): ");
        string limiteTimesInput = Console.ReadLine();
        int? limiteTimes = string.IsNullOrWhiteSpace(limiteTimesInput) ? null : int.Parse(limiteTimesInput);

        Jogo jogo = new Jogo(data, local, tipoCampo, jogadoresPorTime, limiteTimes)
        {
            Codigo = proximoCodigo++
        };

        jogos.Add(jogo);
        Console.WriteLine("Novo jogo cadastrado!");
    }

    public static void Listar()
    {
        if (jogos.Count == 0)
        {
            Console.WriteLine("Não há jogos cadastrados ainda . . . ");
            return;
        }

        foreach (var jogo in jogos)
        {
            Console.WriteLine($"Código: {jogo.Codigo}, Data: {jogo.Data:dd/MM/yyyy}, Local: {jogo.Local}, Tipo de Campo: {jogo.TipoCampo}, Jogadores por Time: {jogo.JogadoresPorTime}, Limite de Times: {jogo.LimiteTimes}");
        }
    }

    public static void Editar()
    {
        Console.Write("Digite o código do jogo: ");
        int codigo = int.Parse(Console.ReadLine());

        var jogo = jogos.Find(j => j.Codigo == codigo);

        if (jogo == null)
        {
            Console.WriteLine("Jogo não encontrado.");
            return;
        }

        Console.Write("Nova data (dd/MM/yyyy) (ou Enter para manter): ");
        string dataInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(dataInput))
            jogo.Data = DateTime.Parse(dataInput);

        Console.Write("Novo local (ou Enter para manter): ");
        string local = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(local))
            jogo.Local = local;

        Console.Write("Novo tipo de campo (ou Enter para manter): ");
        string tipoCampo = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(tipoCampo))
            jogo.TipoCampo = tipoCampo;

        Console.Write("Novo número de jogadores por time (ou Enter para manter): ");
        string jogadoresPorTimeInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(jogadoresPorTimeInput))
            jogo.JogadoresPorTime = int.Parse(jogadoresPorTimeInput);

        Console.Write("Novo limite de times (ou Enter para manter): ");
        string limiteTimesInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(limiteTimesInput))
            jogo.LimiteTimes = int.Parse(limiteTimesInput);

        Console.WriteLine("Jogo atualizado com sucesso!");
    }

    public static void Remover()
    {
        Console.Write("Digite o código do jogo: ");
        int codigo = int.Parse(Console.ReadLine());

        var jogo = jogos.Find(j => j.Codigo == codigo);

        if (jogo == null)
        {
            Console.WriteLine("Jogo não encontrado.");
            return;
        }

        jogos.Remove(jogo);
        Console.WriteLine("Jogo removido com sucesso!");
    }

    public static void ConfirmarPartida()
    {
        Console.Write("Digite o código do jogo para confirmar a partida: ");
        int codigo = int.Parse(Console.ReadLine());

        var jogo = jogos.Find(j => j.Codigo == codigo); // Busca o jogo na lista de jogos

        if (jogo == null)
        {
            Console.WriteLine("Jogo não encontrado.");
            return;
        }

        try
        {
            if (jogo.PodeConfirmarPartida())
            {
                Console.WriteLine("A partida foi confirmada com sucesso!");
            }
            else
            {
                Console.WriteLine("A partida não pode ser confirmada. Jogadores insuficientes.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao confirmar a partida: {ex.Message}");
        }
    }

    public static void AdicionarInteressado()
    {
        Console.Write("Digite o código do jogo: ");
        if (!int.TryParse(Console.ReadLine(), out int codigo))
        {
            Console.WriteLine("Código inválido. Por favor, insira um número.");
            return;
        }

        var jogo = jogos.Find(j => j.Codigo == codigo);

        if (jogo == null)
        {
            Console.WriteLine("Jogo não encontrado.");
            return;
        }

        Console.Write("Digite o nome do interessado: ");
        string nome = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("O nome do interessado não pode ser vazio.");
            return;
        }

        if (jogo.interessados == null)
        {
            jogo.interessados = new List<string>(); // Inicializa a lista se estiver nula
        }

        jogo.interessados.Add(nome);
        Console.WriteLine($"Interessado '{nome}' adicionado com sucesso ao jogo de código {codigo}!");
    }

    public static void ListarInteressados()
    {
        Console.Write("Digite o código do jogo: ");
        int codigo = int.Parse(Console.ReadLine());

        var jogo = Jogo.jogos.Find(j => j.Codigo == codigo);

        if (jogo == null)
        {
            Console.WriteLine("Jogo não encontrado.");
            return;
        }

        Console.WriteLine("Lista de interessados:");
        foreach (var interessado in jogo.interessados)
        {
            Console.WriteLine($"- {interessado}");
        }
    }
}