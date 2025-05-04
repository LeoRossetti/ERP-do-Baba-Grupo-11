namespace Models;
<<<<<<< HEAD:Projeto Futebol/FootballProject2000/Classes/Jogador.cs
public class Jogador{
    public string Nome {get;set;}
    public int Código{get; set;}
    public int Idade{get;set;}
    public string posicao{get;set;}
}
=======
using System;
using System.Collections.Generic;

public class Jogador
{
    public int Codigo { get; set; }
    public string Nome { get; set; }
    public int Idade { get; set; }
    public string Posicao { get; set; }

    
    private static List<Jogador> jogadores = new List<Jogador>();
    private static int proximoCodigo = 1;

    public static void Cadastrar()
    {
        Console.Write("NOME: ");
        string nome = Console.ReadLine();

        Console.Write("IDADE: ");
        int idade = int.Parse(Console.ReadLine());

        Console.Write("Posição: goleiro/ defeza/ ataque: ");
        string posicao = Console.ReadLine().ToLower();

        if (posicao != "goleiro" && posicao != "defesa" && posicao != "ataque")
        {
            Console.WriteLine("Posição não encontrada!");
            return;
        }

        Jogador j  = new Jogador
        {
            Codigo = proximoCodigo++,
            Nome = nome,
            Idade = idade,
            Posicao = posicao
        };

        jogadores.Add(j);
        Console.WriteLine("Novo jogador listado! ");
    }

    public static void Listar()
    {
        if (jogadores.Count == 0)
        {
            Console.WriteLine("Não há jogadores cadastrados ainda . . . ");
            return;
        }

        foreach (var j in jogadores)
        {
            Console.WriteLine($"Código: {j.Codigo}, Nome: {j.Nome}, Idade: {j.Idade}, Posição: {j.Posicao}");
        }
    }

    public static void Editar()
    {
        Console.Write("Digite o código do jogador : ");
        int codigo = int.Parse(Console.ReadLine());

        var jogador = jogadores.Find(j => j.Codigo == codigo);

        if (jogador == null)
        {
            Console.WriteLine("Jogador não encontrado.");
            return;
        }

        Console.Write("Novo nome (ou Enter para manter): ");
        string nome = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(nome))
            jogador.Nome = nome;

        Console.Write("Nova idade (ou Enter para manter): ");
        string idadeInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(idadeInput))
            jogador.Idade = int.Parse(idadeInput);

        Console.Write("Nova posição (ou Enter para manter): ");
        string posicao = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(posicao))
            jogador.Posicao = posicao;

        Console.WriteLine("Jogador atualizado com sucesso!");
    }

    public static void Remover()
    {
        Console.Write("Digite o código do jogador : ");
        int codigo = int.Parse(Console.ReadLine());

        var jogador = jogadores.Find(j => j.Codigo == codigo);

        if (jogador == null)
        {
            Console.WriteLine(" não encontrado.");
            return;
        }

        jogadores.Remove(jogador);
        Console.WriteLine("Jogador removido com sucesso!");
    }
}

>>>>>>> ecc0de2702cc5a9c4ccc54ad994e64aa8c818703:Projeto Futebol/Models/Jogador.cs
    
