using ProjetoFutebol.Models;
using ProjetoFutebol.Services;
using ProjetoFutebol.Utils;

namespace ProjetoFutebol.UI
{
    public class Menu
    {
        GerenciadorDeJogadores gerJogadores = new GerenciadorDeJogadores();
        GerenciadorDeJogos gerJogos = new GerenciadorDeJogos();
        GerenciadorDePartidas gerPartidas = new GerenciadorDePartidas(ModoPartida.QuemGanhaFica);

        // Armazena times pré-criados
        private List<Times> timesPreCriados = new List<Times>();

        public void Exibir()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════════╗");
                Console.WriteLine("║             MENU PRINCIPAL                 ║");
                Console.WriteLine("╠════════════════════════════════════════════╣");
                Console.WriteLine("║  1. Gestão de Jogadores                    ║");
                Console.WriteLine("║  2. Gestão de Jogos                        ║");
                Console.WriteLine("║  0. Sair                                   ║");
                Console.WriteLine("╚════════════════════════════════════════════╝");
                Console.Write("Escolha uma opção: ");
                var opcaoPrincipal = Console.ReadLine();

                if (opcaoPrincipal == "0") break;

                switch (opcaoPrincipal)
                {
                    case "1":
                        ExibirMenuJogadores();
                        break;
                    case "2":
                        ExibirMenuJogos();
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Pressione Enter para continuar...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void ExibirMenuJogadores()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════════╗");
                Console.WriteLine("║         GESTÃO DE JOGADORES               ║");
                Console.WriteLine("╠════════════════════════════════════════════╣");
                Console.WriteLine("║  1. Cadastrar jogador                     ║");
                Console.WriteLine("║  2. Listar jogadores                      ║");
                Console.WriteLine("║  3. Atualizar jogador                     ║");
                Console.WriteLine("║  4. Remover jogador                       ║");
                Console.WriteLine("║  0. Voltar                                ║");
                Console.WriteLine("╚════════════════════════════════════════════╝");
                Console.Write("Escolha uma opção: ");
                var opcao = Console.ReadLine();
                if (opcao == "0") break;
                switch (opcao)
                {
                    case "1":
                        Console.Write("Código: ");
                        int cod = int.Parse(Console.ReadLine()!);
                        Console.Write("Nome: ");
                        string nome = Console.ReadLine()!;
                        Console.Write("Idade: ");
                        int idade = int.Parse(Console.ReadLine()!);
                        Console.WriteLine("Posição (0 = Goleiro, 1 = Defesa, 2 = Ataque): ");
                        Posicao posicao = (Posicao)int.Parse(Console.ReadLine()!);
                        gerJogadores.AdicionarJogador(new Jogador(cod, nome, idade, posicao));
                        Console.WriteLine("Jogador cadastrado com sucesso.");
                        Console.ReadLine();
                        break;
                    case "2":
                        Console.WriteLine("Lista de jogadores:");
                        foreach (var j in gerJogadores.ListarJogadores())
                            Console.WriteLine($"- {j.Codigo} | {j.Nome} | {j.Idade} anos | {j.Posicao}");
                        Console.ReadLine();
                        break;
                    case "3":
                        Console.Write("Código do jogador a atualizar: ");
                        int codAtualizar = int.Parse(Console.ReadLine()!);
                        var jogadorAtual = gerJogadores.BuscarPorCodigo(codAtualizar);
                        if (jogadorAtual == null)
                        {
                            Console.WriteLine("Jogador não encontrado.");
                            Console.ReadLine();
                            break;
                        }
                        Console.Write($"Novo nome ({jogadorAtual.Nome}): ");
                        string novoNome = Console.ReadLine()!;
                        Console.Write($"Nova idade ({jogadorAtual.Idade}): ");
                        int novaIdade = int.Parse(Console.ReadLine()!);
                        Console.WriteLine($"Nova posição (0 = Goleiro, 1 = Defesa, 2 = Ataque) ({jogadorAtual.Posicao}): ");
                        Posicao novaPosicao = (Posicao)int.Parse(Console.ReadLine()!);
                        gerJogadores.AtualizarJogador(new Jogador(codAtualizar, string.IsNullOrWhiteSpace(novoNome) ? jogadorAtual.Nome : novoNome, novaIdade, novaPosicao));
                        Console.WriteLine("Jogador atualizado com sucesso.");
                        Console.ReadLine();
                        break;
                    case "4":
                        Console.Write("Código do jogador a remover: ");
                        int codigoRemover = int.Parse(Console.ReadLine()!);
                        gerJogadores.RemoverJogador(codigoRemover);
                        Console.WriteLine("Jogador removido.");
                        Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Pressione Enter para continuar...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void ExibirMenuJogos()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════════╗");
                Console.WriteLine("║           GESTÃO DE JOGOS                 ║");
                Console.WriteLine("╠════════════════════════════════════════════╣");
                Console.WriteLine("║  1. Criar jogo                            ║");
                Console.WriteLine("║  2. Listar jogos                          ║");
                Console.WriteLine("║  3. Atualizar jogo                        ║");
                Console.WriteLine("║  4. Remover jogo                          ║");
                Console.WriteLine("║  5. Registrar interessado no jogo         ║");
                Console.WriteLine("║  6. Verificar se jogo pode ser confirmado ║");
                Console.WriteLine("║  7. Registrar partida                     ║");
                Console.WriteLine("║  8. Ver histórico de partidas             ║");
                Console.WriteLine("║  9. Ver pódio de times com mais gols      ║");
                Console.WriteLine("║  0. Voltar                                ║");
                Console.WriteLine("╚════════════════════════════════════════════╝");
                Console.Write("Escolha uma opção: ");
                var opcao = Console.ReadLine();
                if (opcao == "0") break;
                switch (opcao)
                {
                    case "1":
                        Console.Write("Data do jogo (yyyy-mm-dd): ");
                        DateTime data = DateTime.Parse(Console.ReadLine()!);
                        Console.Write("Local: ");
                        string local = Console.ReadLine()!;
                        Console.Write("Tipo do campo: ");
                        string campo = Console.ReadLine()!;
                        Console.Write("Jogadores por time: ");
                        int jpt = int.Parse(Console.ReadLine()!);
                        Console.Write("Limite de times (opcional): ");
                        string inputLimite = Console.ReadLine()!;
                        int? limite = string.IsNullOrWhiteSpace(inputLimite) ? null : int.Parse(inputLimite);
                        var jogo = new Jogo(data, local, campo, jpt, limite);
                        gerJogos.AdicionarJogo(jogo);
                        Console.WriteLine("Jogo criado com sucesso.");
                        Console.ReadLine();
                        break;

                    case "2":
                        var jogos = gerJogos.ListarJogos();
                        if (jogos.Count == 0)
                        {
                            Console.WriteLine("Nenhum jogo cadastrado.");
                        }
                        else
                        {
                            Console.WriteLine("Lista de jogos:");
                            for (int i = 0; i < jogos.Count; i++)
                            {
                                var interessados = jogos[i].ListarInteressados();
                                Console.WriteLine($"{i} - {jogos[i].Data.ToShortDateString()} em {jogos[i].Local} | Tipo: {jogos[i].TipoCampo} | Jogadores/time: {jogos[i].JogadoresPorTime}");
                                if (interessados.Count == 0)
                                    Console.WriteLine("    Nenhum interessado cadastrado.");
                                else
                                {
                                    Console.WriteLine("    Interessados:");
                                    foreach (var j in interessados)
                                        Console.WriteLine($"      - {j.Nome} | {j.Posicao}");
                                }
                            }
                        }
                        Console.WriteLine("Pressione Enter para voltar ao menu.");
                        Console.ReadLine();
                        break;
                    case "3":
                        var jogosAt = gerJogos.ListarJogos();
                        if (jogosAt.Count == 0)
                        {
                            Console.WriteLine("Nenhum jogo cadastrado.");
                            Console.ReadLine();
                            break;
                        }
                        Console.WriteLine("Selecione o jogo para atualizar:");
                        for (int i = 0; i < jogosAt.Count; i++)
                            Console.WriteLine($"{i} - {jogosAt[i].Data.ToShortDateString()} em {jogosAt[i].Local}");
                        int idxAt = int.Parse(Console.ReadLine()!);
                        var jogoAt = jogosAt[idxAt];
                        Console.Write($"Nova data ({jogoAt.Data.ToShortDateString()}): ");
                        string novaDataStr = Console.ReadLine()!;
                        DateTime novaData = string.IsNullOrWhiteSpace(novaDataStr) ? jogoAt.Data : DateTime.Parse(novaDataStr);
                        Console.Write($"Novo local ({jogoAt.Local}): ");
                        string novoLocal = Console.ReadLine()!;
                        Console.Write($"Novo tipo de campo ({jogoAt.TipoCampo}): ");
                        string novoTipo = Console.ReadLine()!;
                        Console.Write($"Jogadores por time ({jogoAt.JogadoresPorTime}): ");
                        string novoJptStr = Console.ReadLine()!;
                        int novoJpt = string.IsNullOrWhiteSpace(novoJptStr) ? jogoAt.JogadoresPorTime : int.Parse(novoJptStr);
                        Console.Write($"Limite de times ({jogoAt.LimiteTimes}): ");
                        string novoLimiteStr = Console.ReadLine()!;
                        int? novoLimite = string.IsNullOrWhiteSpace(novoLimiteStr) ? jogoAt.LimiteTimes : int.Parse(novoLimiteStr);
                        // Atualiza os campos diretamente
                        jogoAt.Data = novaData;
                        jogoAt.Local = string.IsNullOrWhiteSpace(novoLocal) ? jogoAt.Local : novoLocal;
                        jogoAt.TipoCampo = string.IsNullOrWhiteSpace(novoTipo) ? jogoAt.TipoCampo : novoTipo;
                        jogoAt.JogadoresPorTime = novoJpt;
                        jogoAt.LimiteTimes = novoLimite;
                        Console.WriteLine("Jogo atualizado com sucesso.");
                        Console.ReadLine();
                        break;
                    case "4":
                        var jogosRem = gerJogos.ListarJogos();
                        if (jogosRem.Count == 0)
                        {
                            Console.WriteLine("Nenhum jogo cadastrado.");
                            Console.ReadLine();
                            break;
                        }
                        Console.WriteLine("Selecione o jogo para remover:");
                        for (int i = 0; i < jogosRem.Count; i++)
                            Console.WriteLine($"{i} - {jogosRem[i].Data.ToShortDateString()} em {jogosRem[i].Local}");
                        int idxRem = int.Parse(Console.ReadLine()!);
                        var jogoRem = jogosRem[idxRem];
                        gerJogos.RemoverJogo(jogoRem);
                        Console.WriteLine("Jogo removido com sucesso.");
                        Console.ReadLine();
                        break;
                    case "5":
                        var jogos5 = gerJogos.ListarJogos();
                        if (jogos5.Count == 0)
                        {
                            Console.WriteLine("Nenhum jogo cadastrado.");
                            Console.ReadLine();
                            break;
                        }
                        Console.WriteLine("Selecione o jogo:");
                        for (int i = 0; i < jogos5.Count; i++)
                            Console.WriteLine($"{i} - {jogos5[i].Data.ToShortDateString()} em {jogos5[i].Local}");
                        int index5 = int.Parse(Console.ReadLine()!);
                        var jogoSel5 = jogos5[index5];
                        Console.WriteLine("Selecione o jogador interessado:");
                        var jogadoresDisp5 = gerJogadores.ListarJogadores();
                        for (int i = 0; i < jogadoresDisp5.Count; i++)
                            Console.WriteLine($"{i} - {jogadoresDisp5[i].Nome} ({jogadoresDisp5[i].Posicao})");
                        int idxJog5 = int.Parse(Console.ReadLine()!);
                        var jogadorInt5 = jogadoresDisp5[idxJog5];
                        try {
                            gerJogos.RegistrarInteressado(jogoSel5, jogadorInt5);
                            Console.WriteLine("Interessado registrado.");
                        } catch (Exception ex) {
                            Console.WriteLine($"Erro: {ex.Message}");
                        }
                        Console.ReadLine();
                        break;

                    case "6":
                        foreach (var j in gerJogos.ListarJogos())
                        {
                            string status;
                            try
                            {
                                status = j.PodeConfirmarPartida() ? "Pode acontecer" : "Faltam jogadores";
                            }
                            catch (Exception ex)
                            {
                                status = $"Erro: {ex.Message}";
                            }
                            Console.WriteLine($"{j.Data.ToShortDateString()} em {j.Local} - {status}");
                        }
                        Console.ReadLine();
                        break;

                    case "7":
                        var jogos7 = gerJogos.ListarJogos();
                        if (jogos7.Count == 0)
                        {
                            Console.WriteLine("Nenhum jogo cadastrado.");
                            Console.ReadLine();
                            break;
                        }
                        Console.WriteLine("Selecione o jogo para registrar as partidas sequenciais:");
                        for (int i = 0; i < jogos7.Count; i++)
                            Console.WriteLine($"{i} - {jogos7[i].Data.ToShortDateString()} em {jogos7[i].Local}");
                        int idxJogo7 = int.Parse(Console.ReadLine()!);
                        var jogoSel7 = jogos7[idxJogo7];
                        var interessados7 = new List<Jogador>(jogoSel7.ListarInteressados());
                        int tamanhoTime = jogoSel7.JogadoresPorTime;
                        if (interessados7.Count < tamanhoTime * 2)
                        {
                            Console.WriteLine($"É necessário pelo menos {tamanhoTime * 2} interessados para registrar uma partida.");
                            Console.ReadLine();
                            break;
                        }
                        // Escolha do critério de geração de times
                        Console.WriteLine("Escolha o critério de geração de times:");
                        Console.WriteLine("1 - Ordem de chegada");
                        Console.WriteLine("2 - Equilíbrio de posição");
                        Console.WriteLine("3 - Alternando mais velhos e mais novos");
                        int criterio = int.Parse(Console.ReadLine()!);
                        (Times, Times) timesPartida;
                        if (criterio == 2)
                            timesPartida = Times.CriarPorEquilibrioDePosicao(interessados7, tamanhoTime);
                        else if (criterio == 3)
                            timesPartida = Times.CriarPorIdadeAlternada(interessados7, tamanhoTime);
                        else
                            timesPartida = Times.CriarPorOrdemChegada(interessados7, tamanhoTime);
                        var filaFora = new Queue<Jogador>(interessados7.Except(timesPartida.Item1.Jogadores).Except(timesPartida.Item2.Jogadores));
                        Times time1 = timesPartida.Item1;
                        Times time2 = timesPartida.Item2;
                        var jogosPorTime = new Dictionary<string, int>();
                        void IncrementaJogo(string nome)
                        {
                            if (!jogosPorTime.ContainsKey(nome)) jogosPorTime[nome] = 0;
                            jogosPorTime[nome]++;
                        }
                        // Pergunta modo de continuação antes do loop
                        Console.WriteLine("Modo de continuação:");
                        Console.WriteLine("0 - Quem ganha fica");
                        Console.WriteLine("1 - Dois jogos por time");
                        int modoPartida = int.Parse(Console.ReadLine()!);

                        // Inicialização
                        var timesNaRodada = new List<Times> { time1, time2 };
                        int numeroTime = 3;
                        Times? timeVencedor = null;
                        Times? timeDerrotado = null;
                        string continuar = "s";
                        while (continuar.ToLower() == "s")
                        {
                            // Seleciona os dois times para a partida
                            Times t1, t2;
                            if (timeVencedor == null) // Primeira partida
                            {
                                t1 = time1;
                                t2 = time2;
                            }
                            else
                            {
                                t1 = timeVencedor;
                                t2 = timesNaRodada.First(t => t != t1 && (modoPartida == 0 || jogosPorTime[t.Nome] < 2));
                            }
                            Console.WriteLine($"\nPartida: {t1.Nome} x {t2.Nome}");
                            Console.WriteLine($"{t1.Nome}:");
                            foreach (var j in t1.Jogadores)
                                Console.WriteLine($"- {j.Nome} | {j.Posicao}");
                            Console.WriteLine($"{t2.Nome}:");
                            foreach (var j in t2.Jogadores)
                                Console.WriteLine($"- {j.Nome} | {j.Posicao}");
                            Console.Write($"Gols do {t1.Nome}: ");
                            int golsT1 = int.Parse(Console.ReadLine()!);
                            Console.Write($"Gols do {t2.Nome}: ");
                            int golsT2 = int.Parse(Console.ReadLine()!);
                            string vencedorP = "Empate";
                            if (golsT1 > golsT2) vencedorP = t1.Nome;
                            else if (golsT2 > golsT1) vencedorP = t2.Nome;
                            Console.WriteLine($"Vencedor: {vencedorP}");
                            Console.Write("Observações (opcional): ");
                            string obs = Console.ReadLine()!;
                            var partida = new Partida(DateTime.Now, t1.Nome, t2.Nome, vencedorP, golsT1, golsT2, string.IsNullOrWhiteSpace(obs) ? null : obs);
                            gerJogos.AdicionarPartida(jogoSel7, partida);
                            IncrementaJogo(t1.Nome);
                            IncrementaJogo(t2.Nome);

                            if (vencedorP == "Empate")
                            {
                                Console.WriteLine("Empate! Fim da sequência de partidas.");
                                break;
                            }
                            // Define vencedor e derrotado
                            timeVencedor = vencedorP == t1.Nome ? t1 : t2;
                            timeDerrotado = vencedorP == t1.Nome ? t2 : t1;

                            // Modo "dois jogos por time": se algum time atingiu 2 jogos, ele sai
                            if (modoPartida == 1 && jogosPorTime[timeDerrotado.Nome] >= 2)
                            {
                                Console.WriteLine($"{timeDerrotado.Nome} atingiu 2 jogos e sai do ciclo.");
                                timesNaRodada.Remove(timeDerrotado);
                            }
                            // Remove derrotado (ou time que atingiu 2 jogos) e cria novo time
                            if (modoPartida == 0 || (modoPartida == 1 && jogosPorTime[timeDerrotado.Nome] >= 2))
                            {
                                // Gera novo time
                                var jogadoresDisponiveis = filaFora.ToList();
                                var novoTime = Times.GerarNovoTime(jogadoresDisponiveis, timeDerrotado, tamanhoTime, numeroTime++);
                                foreach (var j in novoTime.Jogadores)
                                    filaFora = new Queue<Jogador>(filaFora.Where(f => f.Codigo != j.Codigo));
                                timesNaRodada.Remove(timeDerrotado);
                                timesNaRodada.Add(novoTime);
                                if (!jogosPorTime.ContainsKey(novoTime.Nome)) jogosPorTime[novoTime.Nome] = 0;
                            }
                            // Se só restar um time, encerra
                            if (timesNaRodada.Count < 2)
                            {
                                Console.WriteLine("Não há times suficientes para continuar.");
                                break;
                            }
                            Console.WriteLine("Deseja registrar outra partida? (s/n)");
                            continuar = Console.ReadLine()!;
                        }
                        Console.WriteLine("Sequência de partidas encerrada.");
                        Console.ReadLine();
                        break;
                    case "8":
                        var jogosHist = gerJogos.ListarJogos();
                        if (jogosHist.Count == 0)
                        {
                            Console.WriteLine("Nenhum jogo cadastrado.");
                            Console.ReadLine();
                            break;
                        }
                        Console.WriteLine("Selecione o jogo para ver o histórico de partidas:");
                        for (int i = 0; i < jogosHist.Count; i++)
                            Console.WriteLine($"{i} - {jogosHist[i].Data.ToShortDateString()} em {jogosHist[i].Local}");
                        int idxHist = int.Parse(Console.ReadLine()!);
                        var jogoHist = jogosHist[idxHist];
                        if (jogoHist.Partidas == null || jogoHist.Partidas.Count == 0)
                        {
                            Console.WriteLine("Nenhuma partida registrada para este jogo.");
                        }
                        else
                        {
                            Console.WriteLine($"Histórico de partidas do jogo em {jogoHist.Local}:");
                            foreach (var p in jogoHist.Partidas)
                            {
                                Console.WriteLine($"{p.Time1} {p.GolsTime1} x {p.GolsTime2} {p.Time2} => Vencedor: {p.Vencedor}");
                                if (!string.IsNullOrWhiteSpace(p.Observacoes))
                                    Console.WriteLine($"  Observações: {p.Observacoes}");
                            }
                        }
                        Console.ReadLine();
                        break;

                    case "9":
                        var jogosPodio = gerJogos.ListarJogos();
                        if (jogosPodio.Count == 0)
                        {
                            Console.WriteLine("Nenhum jogo cadastrado.");
                            Console.ReadLine();
                            break;
                        }
                        Console.WriteLine("Selecione o jogo para ver o pódio de gols:");
                        for (int i = 0; i < jogosPodio.Count; i++)
                            Console.WriteLine($"{i} - {jogosPodio[i].Data.ToShortDateString()} em {jogosPodio[i].Local}");
                        int idxPodio = int.Parse(Console.ReadLine()!);
                        var jogoPodio = jogosPodio[idxPodio];
                        if (jogoPodio.Partidas == null || jogoPodio.Partidas.Count == 0)
                        {
                            Console.WriteLine("Nenhuma partida registrada para este jogo.");
                            Console.ReadLine();
                            break;
                        }
                        var golsPorTime = new Dictionary<string, int>();
                        foreach (var partida in jogoPodio.Partidas)
                        {
                            if (!golsPorTime.ContainsKey(partida.Time1)) golsPorTime[partida.Time1] = 0;
                            if (!golsPorTime.ContainsKey(partida.Time2)) golsPorTime[partida.Time2] = 0;
                            golsPorTime[partida.Time1] += partida.GolsTime1;
                            golsPorTime[partida.Time2] += partida.GolsTime2;
                        }
                        var podio = golsPorTime.OrderByDescending(kv => kv.Value).Take(3).ToList();
                        string[] posicoes = {"1º lugar", "2º lugar", "3º lugar"};
                        Console.WriteLine($"Pódio de times com mais gols no jogo em {jogoPodio.Local}:");
                        for (int i = 0; i < podio.Count; i++)
                            Console.WriteLine($"{posicoes[i]}: {podio[i].Key} com {podio[i].Value} gol(s)");
                        Console.WriteLine("Pressione Enter para voltar ao menu.");
                        Console.ReadLine();
                        break;

                    default:
                        Console.WriteLine("Opção inválida. Pressione Enter para continuar...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        public void SalvarDados()
        {
            JsonStorage.SalvarJogadores(gerJogadores.ListarJogadores());
            //JsonStorage.SalvarPartidas(gerPartidas.ObterHistorico()); // Removido: partidas agora só dentro de jogos
            JsonStorage.SalvarTimes(timesPreCriados);
            JsonStorage.SalvarJogos(gerJogos.ListarJogos());
        }

        public void CarregarDados()
        {
            // Limpa listas antes de carregar
            gerJogadores = new GerenciadorDeJogadores();
            gerJogos = new GerenciadorDeJogos();
            gerPartidas = new GerenciadorDePartidas(ModoPartida.QuemGanhaFica);
            timesPreCriados = new List<Times>();

            var jogadores = JsonStorage.CarregarJogadores();
            foreach (var j in jogadores)
            {
                if (gerJogadores.BuscarPorCodigo(j.Codigo) == null)
                    gerJogadores.AdicionarJogador(j);
            }
            timesPreCriados = JsonStorage.CarregarTimes();
            var jogos = JsonStorage.CarregarJogos();
            foreach (var jogo in jogos)
            {
                if (!gerJogos.ListarJogos().Any(j => j.Data == jogo.Data && j.Local == jogo.Local))
                    gerJogos.AdicionarJogo(jogo);
            }
        }
    }
}
