using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ProjetoFutebol.Models;

namespace ProjetoFutebol.Utils
{
    public static class JsonStorage
    {
        private static string jogadoresPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProjetoFutebol", "Data", "jogadores.json");
        private static string timesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProjetoFutebol", "Data", "times.json");
        private static string jogosPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProjetoFutebol", "Data", "jogos.json");

        private static void GarantirDiretorio(string path)
        {
            var dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        public static void SalvarJogadores(List<Jogador> jogadores)
        {
            GarantirDiretorio(jogadoresPath);
            var json = JsonSerializer.Serialize(jogadores, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jogadoresPath, json);
        }

        public static List<Jogador> CarregarJogadores()
        {
            if (!File.Exists(jogadoresPath)) return new List<Jogador>();
            var json = File.ReadAllText(jogadoresPath);
            return JsonSerializer.Deserialize<List<Jogador>>(json) ?? new List<Jogador>();
        }

        public static void SalvarTimes(List<Times> times)
        {
            GarantirDiretorio(timesPath);
            var json = JsonSerializer.Serialize(times, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(timesPath, json);
        }

        public static List<Times> CarregarTimes()
        {
            if (!File.Exists(timesPath)) return new List<Times>();
            var json = File.ReadAllText(timesPath);
            return JsonSerializer.Deserialize<List<Times>>(json) ?? new List<Times>();
        }

        public static void SalvarJogos(List<Jogo> jogos)
        {
            GarantirDiretorio(jogosPath);
            var json = JsonSerializer.Serialize(jogos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jogosPath, json);
        }

        public static List<Jogo> CarregarJogos()
        {
            if (!File.Exists(jogosPath)) return new List<Jogo>();
            var json = File.ReadAllText(jogosPath);
            return JsonSerializer.Deserialize<List<Jogo>>(json) ?? new List<Jogo>();
        }
    }
}
