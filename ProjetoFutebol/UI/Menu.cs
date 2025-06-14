using ProjetoFutebol.Models;
using ProjetoFutebol.Services;

namespace ProjetoFutebol.UI
{
    public class Menu
    {
        GerenciadorDeJogadores gerJogadores = new GerenciadorDeJogadores();
        GerenciadorDeJogos gerJogos = new GerenciadorDeJogos();
        GerenciadorDePartidas gerPartidas = new GerenciadorDePartidas(ModoPartida.QuemGanhaFica);

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

<<<<<<< Updated upstream
                        case "2":
                            Console.WriteLine("Lista de jogadores:");
                            foreach (var j in gerJogadores.ListarJogadores())
                                Console.WriteLine($"- {j.Codigo} | {j.Nome} | {j.Idade} anos | {j.Posicao}");
                            break;
=======
        private void ExibirMenuJogos()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════════╗");
                Console.WriteLine("║           GESTÃO DE JOGOS                  ║");
                Console.WriteLine("╠════════════════════════════════════════════╣");
                Console.WriteLine("║  1. Criar jogo                             ║");
                Console.WriteLine("║  2. Listar jogos                           ║");
                Console.WriteLine("║  3. Atualizar jogo                         ║");
                Console.WriteLine("║  4. Remover jogo                           ║");
                Console.WriteLine("║  5. Registrar interessado no jogo          ║");
                Console.WriteLine("║  6. Verificar se jogo pode ser confirmado  ║");
                Console.WriteLine("║  7. Registrar partida                      ║");
                Console.WriteLine("║  8. Ver histórico de partidas              ║");
                Console.WriteLine("║  9. Ver pódio de times com mais gols       ║");
                Console.WriteLine("║  0. Voltar                                 ║");
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
>>>>>>> Stashed changes

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
                            var jogos = gerJogos.ListarJogos();
                            if (jogos.Count == 0)
                            {
                                Console.WriteLine("Nenhum jogo cadastrado.");
                                break;
                            }
                            Console.WriteLine("Selecione o jogo:");
                            for (int i = 0; i < jogos.Count; i++)
                                Console.WriteLine($"{i} - {jogos[i].Data.ToShortDateString()} em {jogos[i].Local}");
                            int index = int.Parse(Console.ReadLine()!);
                            Console.Write("Nome do interessado: ");
                            string interessado = Console.ReadLine()!;
                            gerJogos.RegistrarInteressado(jogos[index], interessado);
                            Console.WriteLine("Interessado registrado.");
                            break;

                        case "6":
                            foreach (var j in gerJogos.ListarJogos())
                            {
                                string status = j.PodeConfirmarPartida() ? "Pode acontecer" : "Faltam jogadores";
                                Console.WriteLine($"{j.Data.ToShortDateString()} em {j.Local} - {status}");
                            }
                            break;

                        case "7":
                            Console.Write("Time 1: ");
                            string time1 = Console.ReadLine()!;
                            Console.Write("Time 2: ");
                            string time2 = Console.ReadLine()!;
                            Console.Write("Vencedor: ");
                            string vencedor = Console.ReadLine()!;
                            gerPartidas.RegistrarPartida(new Partida(DateTime.Now, time1, time2, vencedor));
                            Console.WriteLine("Partida registrada.");
                            break;

                        case "8":
                            Console.WriteLine("Histórico de partidas:");
                            foreach (var p in gerPartidas.ObterHistorico())
                                Console.WriteLine($"{p.Data.ToShortDateString()}: {p.Time1} x {p.Time2} => Vencedor: {p.Vencedor}");
                            break;

                        case "9":
                            Console.WriteLine("Modo de continuação:");
                            Console.WriteLine("0 - Quem ganha fica");
                            Console.WriteLine("1 - Dois jogos por time");
                            int modo = int.Parse(Console.ReadLine()!);
                            gerPartidas = new GerenciadorDePartidas((ModoPartida)modo);
                            Console.WriteLine("Modo de continuação atualizado.");
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
    }
}
