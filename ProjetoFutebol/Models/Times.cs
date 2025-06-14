using System.Collections.Generic;
using System.Linq;

namespace ProjetoFutebol.Models
{
    public class Times
    {
        public string Nome { get; set; }
        public List<Jogador> Jogadores { get; set; }
        public bool EmJogo { get; set; } // Indica se o time está em partida

        public Times(string nome)
        {
            Nome = nome;
            Jogadores = new List<Jogador>();
            EmJogo = false;
        }

        // 1. Criação de times por ordem de chegada (com regra dos goleiros)
        public static (Times, Times) CriarPorOrdemChegada(List<Jogador> jogadores, int tamanhoTime)
        {
            var time1 = new Times("Time 1");
            var time2 = new Times("Time 2");

            // Seleciona goleiros
            var goleiros = jogadores.Where(j => j.Posicao == Posicao.Goleiro).ToList();
            Jogador goleiro1 = goleiros.Count > 0 ? goleiros[0] : jogadores[0];
            Jogador goleiro2;
            if (goleiros.Count > 1)
            {
                goleiro2 = goleiros[1];
            }
            else
            {
                // Pega o próximo jogador que não seja o goleiro1 e não seja goleiro
                goleiro2 = jogadores.First(j => j != goleiro1 && j.Posicao != Posicao.Goleiro);
            }

            time1.Jogadores.Add(goleiro1);
            time2.Jogadores.Add(goleiro2);

            // Remove goleiros da lista para distribuir os demais
            var restantes = jogadores.Where(j => j != goleiro1 && j != goleiro2 && j.Posicao != Posicao.Goleiro).ToList();
            var outros = jogadores.Where(j => j != goleiro1 && j != goleiro2 && j.Posicao == Posicao.Goleiro).ToList();
            restantes.AddRange(outros); // Adiciona eventuais goleiros "sobrando" só depois

            // Adiciona os próximos jogadores por ordem de chegada
            for (int i = 0; i < restantes.Count && (time1.Jogadores.Count < tamanhoTime || time2.Jogadores.Count < tamanhoTime); i++)
            {
                if (time1.Jogadores.Count < tamanhoTime)
                    time1.Jogadores.Add(restantes[i]);
                else if (time2.Jogadores.Count < tamanhoTime)
                    time2.Jogadores.Add(restantes[i]);
            }

            return (time1, time2);
        }

        // 2. Criação de times equilibrados por posição
        public static (Times, Times) CriarPorEquilibrioDePosicao(List<Jogador> jogadores, int tamanhoTime)
        {
            var time1 = new Times("Time 1");
            var time2 = new Times("Time 2");

            // Seleciona goleiros
            var goleiros = jogadores.Where(j => j.Posicao == Posicao.Goleiro).ToList();
            if (goleiros.Count > 0) time1.Jogadores.Add(goleiros[0]);
            if (goleiros.Count > 1) time2.Jogadores.Add(goleiros[1]);
            else if (goleiros.Count == 1 && jogadores.Count > 1) time2.Jogadores.Add(jogadores.First(j => j.Posicao != Posicao.Goleiro));
            else if (goleiros.Count == 0 && jogadores.Count > 1)
            {
                time1.Jogadores.Add(jogadores[0]);
                time2.Jogadores.Add(jogadores[1]);
            }

            // Defensores e atacantes
            var defesas = jogadores.Where(j => j.Posicao == Posicao.Defesa && !time1.Jogadores.Contains(j) && !time2.Jogadores.Contains(j)).ToList();
            var ataques = jogadores.Where(j => j.Posicao == Posicao.Ataque && !time1.Jogadores.Contains(j) && !time2.Jogadores.Contains(j)).ToList();

            // Alterna defensores
            for (int i = 0; i < defesas.Count; i++)
            {
                if (time1.Jogadores.Count < tamanhoTime && (i % 2 == 0))
                    time1.Jogadores.Add(defesas[i]);
                else if (time2.Jogadores.Count < tamanhoTime)
                    time2.Jogadores.Add(defesas[i]);
            }
            // Alterna atacantes
            for (int i = 0; i < ataques.Count; i++)
            {
                if (time1.Jogadores.Count < tamanhoTime && (i % 2 == 0))
                    time1.Jogadores.Add(ataques[i]);
                else if (time2.Jogadores.Count < tamanhoTime)
                    time2.Jogadores.Add(ataques[i]);
            }
            // Preenche se faltar
            var restantes = jogadores.Except(time1.Jogadores).Except(time2.Jogadores).ToList();
            foreach (var j in restantes)
            {
                if (time1.Jogadores.Count < tamanhoTime)
                    time1.Jogadores.Add(j);
                else if (time2.Jogadores.Count < tamanhoTime)
                    time2.Jogadores.Add(j);
            }
            return (time1, time2);
        }

        // 3. Critério criativo: alterna mais velhos e mais novos
        public static (Times, Times) CriarPorIdadeAlternada(List<Jogador> jogadores, int tamanhoTime)
        {
            var time1 = new Times("Time 1");
            var time2 = new Times("Time 2");
            var ordenados = jogadores.OrderByDescending(j => j.Idade).ToList();
            int i = 0;
            while ((time1.Jogadores.Count < tamanhoTime || time2.Jogadores.Count < tamanhoTime) && i < ordenados.Count)
            {
                if (time1.Jogadores.Count < tamanhoTime)
                    time1.Jogadores.Add(ordenados[i++]);
                if (i < ordenados.Count && time2.Jogadores.Count < tamanhoTime)
                    time2.Jogadores.Add(ordenados[i++]);
            }
            return (time1, time2);
        }

        // 4. Gerar novo time após término de partida, reaproveitando jogadores do time derrotado
        public static Times GerarNovoTime(List<Jogador> jogadoresDisponiveis, Times timeDerrotado, int tamanhoTime, int numeroTime)
        {
            var novoTime = new Times($"Time {numeroTime}");
            // Adiciona jogadores disponíveis
            foreach (var j in jogadoresDisponiveis)
            {
                if (novoTime.Jogadores.Count < tamanhoTime)
                    novoTime.Jogadores.Add(j);
            }
            // Se faltar, completa com jogadores do time derrotado
            foreach (var j in timeDerrotado.Jogadores)
            {
                if (novoTime.Jogadores.Count < tamanhoTime && !novoTime.Jogadores.Contains(j))
                    novoTime.Jogadores.Add(j);
            }
            return novoTime;
        }
    }
}
