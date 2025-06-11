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
                Console.WriteLine("║  1. Cadastrar jogador                      ║");
                Console.WriteLine("║  2. Listar jogadores                       ║");
                Console.WriteLine("║  3. Remover jogador                        ║");
                Console.WriteLine("║  4. Criar jogo                             ║");
                Console.WriteLine("║  5. Registrar interessado no jogo          ║");
                Console.WriteLine("║  6. Verificar se jogo pode ser confirmado  ║");
                Console.WriteLine("║  7. Registrar partida                      ║");
                Console.WriteLine("║  8. Ver histórico de partidas              ║");
                Console.WriteLine("║  9. Mudar modo de continuação              ║");
                Console.WriteLine("║ 10. Criar times por ordem de chegada       ║");
                Console.WriteLine("║ 11. Criar times equilibrados por posição   ║");
                Console.WriteLine("║ 12. Criar times por pontuação alternada    ║");
                Console.WriteLine("║  0. Sair                                   ║");
                Console.WriteLine("╚════════════════════════════════════════════╝");
                Console.Write("Escolha uma opção: ");
                var opcao = Console.ReadLine();

                try
                {
                    Console.WriteLine();

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
                            gerJogadores.AdicionarJogador(new Jogador(cod, nome, idade, posicao, 0));
                            Console.WriteLine("Jogador cadastrado com sucesso.");
                            break;


                        case "2":
                            Console.WriteLine("Lista de jogadores:");
                            foreach (var j in gerJogadores.ListarJogadores())
                                Console.WriteLine($"- {j.Codigo} | {j.Nome} | {j.Idade} anos | {j.Posicao}");
                            break;

                        case "3":
                            Console.Write("Código do jogador a remover: ");
                            int codigoRemover = int.Parse(Console.ReadLine()!);
                            gerJogadores.RemoverJogador(codigoRemover);
                            Console.WriteLine("Jogador removido.");
                            break;

                        case "4":
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
                            break;

                        case "5":
                            var jogos5 = gerJogos.ListarJogos();
                            if (jogos5.Count == 0)
                            {
                                Console.WriteLine("Nenhum jogo cadastrado.");
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
                            break;

                        case "6":
                            foreach (var j in gerJogos.ListarJogos())
                            {
                                string status = j.PodeConfirmarPartida() ? "Pode acontecer" : "Faltam jogadores";
                                Console.WriteLine($"{j.Data.ToShortDateString()} em {j.Local} - {status}");
                            }
                            break;

                        case "7":
                            var jogos7 = gerJogos.ListarJogos();
                            if (jogos7.Count == 0)
                            {
                                Console.WriteLine("Nenhum jogo cadastrado.");
                                break;
                            }
                            Console.WriteLine("Selecione o jogo para registrar a partida:");
                            for (int i = 0; i < jogos7.Count; i++)
                                Console.WriteLine($"{i} - {jogos7[i].Data.ToShortDateString()} em {jogos7[i].Local}");
                            int idxJogo7 = int.Parse(Console.ReadLine()!);
                            var jogoSel7 = jogos7[idxJogo7];
                            var interessados7 = jogoSel7.ListarInteressados();
                            if (interessados7.Count < jogoSel7.JogadoresPorTime * 2)
                            {
                                Console.WriteLine($"É necessário pelo menos {jogoSel7.JogadoresPorTime * 2} interessados para registrar uma partida.");
                                break;
                            }
                            Console.WriteLine("Montar times para a partida:");
                            Console.WriteLine("1 - Ordem de chegada");
                            Console.WriteLine("2 - Equilíbrio de posição");
                            Console.WriteLine("3 - Pontuação alternada");
                            int modoTimes = int.Parse(Console.ReadLine()!);
                            (Times, Times) timesPartida;
                            if (modoTimes == 1)
                                timesPartida = Times.CriarPorOrdemChegada(interessados7, jogoSel7.JogadoresPorTime);
                            else if (modoTimes == 2)
                                timesPartida = Times.CriarPorEquilibrioDePosicao(interessados7, jogoSel7.JogadoresPorTime);
                            else
                                timesPartida = Times.CriarPorPontuacaoAlternada(interessados7, jogoSel7.JogadoresPorTime);
                            var time1P = timesPartida.Item1;
                            var time2P = timesPartida.Item2;
                            Console.WriteLine("Time 1:");
                            foreach (var j in time1P.Jogadores)
                                Console.WriteLine($"- {j.Nome} | {j.Posicao}");
                            Console.WriteLine("Time 2:");
                            foreach (var j in time2P.Jogadores)
                                Console.WriteLine($"- {j.Nome} | {j.Posicao}");
                            Console.Write($"Gols do {time1P.Nome}: ");
                            int golsT1 = int.Parse(Console.ReadLine()!);
                            Console.Write($"Gols do {time2P.Nome}: ");
                            int golsT2 = int.Parse(Console.ReadLine()!);
                            Console.Write("Vencedor: ");
                            string vencedorP = Console.ReadLine()!;
                            var pontuacoesP = new List<(int, string, int)>();
                            Console.WriteLine("Quantos jogadores pontuaram nesta partida?");
                            int qtdPontuaramP = int.Parse(Console.ReadLine()!);
                            for (int i = 0; i < qtdPontuaramP; i++)
                            {
                                Console.Write($"Nome do jogador #{i + 1}: ");
                                string nomeJog = Console.ReadLine()!;
                                var jogador = interessados7.FirstOrDefault(j => j.Nome.Equals(nomeJog, StringComparison.OrdinalIgnoreCase));
                                if (jogador == null)
                                {
                                    Console.WriteLine("Jogador não está entre os interessados. Tente novamente.");
                                    i--; continue;
                                }
                                Console.Write($"Pontos feitos por {jogador.Nome}: ");
                                int pontos = int.Parse(Console.ReadLine()!);
                                Console.Write($"Gols marcados por {jogador.Nome}: ");
                                int gols = int.Parse(Console.ReadLine()!);
                                jogador.Gols += gols;
                                pontuacoesP.Add((jogador.Codigo, jogador.Nome, pontos));
                            }
                            var partida = new Partida(DateTime.Now, time1P.Nome, time2P.Nome, vencedorP, null, pontuacoesP, golsT1, golsT2);
                            gerJogos.AdicionarPartida(jogoSel7, partida);
                            Console.WriteLine("Partida registrada no jogo do dia.");
                            break;

                        case "8":
                            Console.WriteLine("Histórico de partidas:");
                            foreach (var p in gerPartidas.ObterHistorico())
                            {
                                Console.WriteLine($"{p.Data.ToShortDateString()}: {p.Time1} x {p.Time2} => Vencedor: {p.Vencedor}");
                                if (p.Pontuacoes != null && p.Pontuacoes.Count > 0)
                                {
                                    Console.WriteLine("  Jogadores que pontuaram:");
                                    foreach (var pontuacao in p.Pontuacoes)
                                        Console.WriteLine($"    - {pontuacao.NomeJogador} (cód: {pontuacao.CodigoJogador}): {pontuacao.Pontos} ponto(s)");
                                }
                            }
                            break;

                        case "9":
                            Console.WriteLine("Modo de continuação:");
                            Console.WriteLine("0 - Quem ganha fica");
                            Console.WriteLine("1 - Dois jogos por time");
                            int modo = int.Parse(Console.ReadLine()!);
                            gerPartidas = new GerenciadorDePartidas((ModoPartida)modo);
                            Console.WriteLine("Modo de continuação atualizado.");
                            break;

                        case "10":
                        case "11":
                        case "12":
                            var jogosT = gerJogos.ListarJogos();
                            if (jogosT.Count == 0)
                            {
                                Console.WriteLine("Nenhum jogo cadastrado.");
                                break;
                            }
                            Console.WriteLine("Selecione o jogo para montar os times:");
                            for (int i = 0; i < jogosT.Count; i++)
                                Console.WriteLine($"{i} - {jogosT[i].Data.ToShortDateString()} em {jogosT[i].Local}");
                            int idxJogoT = int.Parse(Console.ReadLine()!);
                            var jogoSelT = jogosT[idxJogoT];
                            var interessadosT = jogoSelT.ListarInteressados();
                            if (interessadosT.Count < jogoSelT.JogadoresPorTime * 2)
                            {
                                Console.WriteLine($"É necessário pelo menos {jogoSelT.JogadoresPorTime * 2} interessados para montar os times.");
                                break;
                            }
                            (Times, Times) timesMontados;
                            if (opcao == "10")
                                timesMontados = Times.CriarPorOrdemChegada(interessadosT, jogoSelT.JogadoresPorTime);
                            else if (opcao == "11")
                                timesMontados = Times.CriarPorEquilibrioDePosicao(interessadosT, jogoSelT.JogadoresPorTime);
                            else
                                timesMontados = Times.CriarPorPontuacaoAlternada(interessadosT, jogoSelT.JogadoresPorTime);
                            Console.WriteLine("\nTime 1:");
                            foreach (var j in timesMontados.Item1.Jogadores)
                                Console.WriteLine($"- {j.Nome} | {j.Posicao}");
                            Console.WriteLine("\nTime 2:");
                            foreach (var j in timesMontados.Item2.Jogadores)
                                Console.WriteLine($"- {j.Nome} | {j.Posicao}");
                            break;

                        case "0":
                            Console.WriteLine("Encerrando o programa. Até logo!");
                            return;

                        default:
                            Console.WriteLine("Opção inválida. Tente novamente.");
                            break;
                    }

                    Console.WriteLine();
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        public void SalvarDados()
        {
            JsonStorage.SalvarJogadores(gerJogadores.ListarJogadores());
            JsonStorage.SalvarPartidas(gerPartidas.ObterHistorico());
            JsonStorage.SalvarTimes(timesPreCriados);
            JsonStorage.SalvarJogos(gerJogos.ListarJogos());
        }

        public void CarregarDados()
        {
            var jogadores = JsonStorage.CarregarJogadores();
            foreach (var j in jogadores)
            {
                if (gerJogadores.BuscarPorCodigo(j.Codigo) == null)
                    gerJogadores.AdicionarJogador(j);
            }
            var partidas = JsonStorage.CarregarPartidas();
            foreach (var p in partidas)
                gerPartidas.RegistrarPartida(p);
            timesPreCriados = JsonStorage.CarregarTimes();
            var jogos = JsonStorage.CarregarJogos();
            foreach (var jogo in jogos)
            {
                // Evita duplicidade ao recarregar
                if (!gerJogos.ListarJogos().Any(j => j.Data == jogo.Data && j.Local == jogo.Local))
                    gerJogos.AdicionarJogo(jogo);
            }
        }
    }
}
