using ProjetoFutebol.UI;

class Program
{
    static void Main(string[] args)
    {
        // Garante que a pasta Data existe
        if (!System.IO.Directory.Exists("Data"))
            System.IO.Directory.CreateDirectory("Data");

        var menu = new Menu();
        menu.CarregarDados(); // Carrega dados automaticamente ao iniciar
        menu.Exibir();
        menu.SalvarDados(); // Salva dados automaticamente ao sair
    }
}