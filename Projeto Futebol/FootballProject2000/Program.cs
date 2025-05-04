using Models;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\nMenu Principal:");
            Console.WriteLine("1. Jogador - Cadastrar");
            Console.WriteLine("2. Jogador - Listar");
            Console.WriteLine("3. Jogador - Editar");
            Console.WriteLine("4. Jogador - Remover");
            Console.WriteLine("5. Jogo - Cadastrar");
            Console.WriteLine("6. Jogo - Listar");
            Console.WriteLine("7. Jogo - Editar");
            Console.WriteLine("8. Jogo - Remover");
            Console.WriteLine("9. Jogo - Confirmar Partida");
            Console.WriteLine("10. Jogo - Adicionar Interessado");
            Console.WriteLine("11. Jogo - Listar Interessados");
            Console.WriteLine("12. Sair");
            Console.Write("Escolha uma opção: ");

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Jogador.Cadastrar();
                    break;
                case "2":
                    Jogador.Listar();
                    break;
                case "3":
                    Jogador.Editar();
                    break;
                case "4":
                    Jogador.Remover();
                    break;
                case "5":
                    Jogo.Cadastrar();
                    break;
                case "6":
                    Jogo.Listar();
                    break;
                case "7":
                    Jogo.Editar();
                    break;
                case "8":
                    Jogo.Remover();
                    break;
                case "9":
                    Jogo.ConfirmarPartida();
                    break;
                case "10":
                    Jogo.AdicionarInteressado();
                    break;
                case "11":
                    Jogo.ListarInteressados();
                    break;
                case "12":
                    Console.WriteLine("Saindo...");
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }
}