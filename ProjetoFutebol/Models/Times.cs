using System.Collections.Generic;

namespace ProjetoFutebol.Models // namespace esquisito pra pegar tudo 
{
    public class Time : Jogador 
    {
        public List<Jogador> Jogadores { get; set; }

        public Time(int codigo, string nome, int idade, Posicao posicao, int pontos)
            : base(codigo, nome, idade, posicao, pontos) // contrutor gigantesco 
        {
            Jogadores = new List<Jogador>();
        }

        public void AdicionarJogadores(Jogador jogador)
        {
            Jogadores.Add(jogador);
        }

        public void RemoverJogadores(Jogador jogador)
        {
            Jogadores.Remove(jogador);
        }

        public static List<Time> OrdemChegada(List<Jogador> jogadores, int jogadoresPorTime)
        {
            var times = new List<Time>();
            var fila = new Queue<Jogador>(jogadores);
            int codigoTime = 1;

            while (fila.Count > 0)
            {
                // Seleciona o goleiro
                Jogador goleiro = null;
                int busca = 0;
                int filaCount = fila.Count;
                while (busca < filaCount)
                {
                    var jogador = fila.Dequeue();// dequeue é desenfileirar o jigador que está no início
                    if (goleiro == null && jogador.Posicao == Posicao.Goleiro)
                    {
                        goleiro = jogador;
                        break;
                    }
                    else
                    {
                        fila.Enqueue(jogador); // enfileirando o goleiro no início 
                    }
                    busca++;
                }
                if (goleiro == null)
                {
                    //não tem goleiro pra dois times 
                    break;
                }
                // add o goleiro 
                var time = new Time(codigoTime++, $"Time {codigoTime}", 0, Posicao.Goleiro, 0);
                time.AdicionarJogadores(goleiro);

                // Preenche o restante do time
                int count = 1;
                while (count < jogadoresPorTime && fila.Count > 0)
                {
                    var jogador = fila.Dequeue();
                    if (jogador.Posicao != Posicao.Goleiro)
                    {
                        time.AdicionarJogadores(jogador);
                        count++;
                    }
                    else
                    {
                        // Mantém goleiros para outros times
                        fila.Enqueue(jogador);
                    }
                }
                times.Add(time);
            }
            return times;
        }

        public static List<Time> FormarTimesBalanceados(List<Jogador> jogadores, int jogadoresPorTime)
        {
            var times = new List<Time>();
            int codigoTime = 1;
            // Separa jogadores por posição
            var goleiros = new Queue<Jogador>(jogadores.FindAll(j => j.Posicao == Posicao.Goleiro));
            var defesas = new Queue<Jogador>(jogadores.FindAll(j => j.Posicao == Posicao.Defesa));
            var ataques = new Queue<Jogador>(jogadores.FindAll(j => j.Posicao == Posicao.Ataque));

            int totalTimes = Math.Min(goleiros.Count, (goleiros.Count + defesas.Count + ataques.Count) / jogadoresPorTime);
            for (int i = 0; i < totalTimes; i++)
            {
                var time = new Time(codigoTime++, $"Time {codigoTime}", 0, Posicao.Goleiro, 0);
                times.Add(time);
            }

            // Distribui goleiros
            foreach (var time in times)
            {
                if (goleiros.Count > 0)
                    time.AdicionarJogadores(goleiros.Dequeue());
            }

            // Distribui defesas e ataques balanceando
            int JogadorComum = 0;
            while (defesas.Count > 0 || ataques.Count > 0)
            {
                if (defesas.Count > 0)
                {
                    times[JogadorComum % times.Count].AdicionarJogadores(defesas.Dequeue());
                    JogadorComum++;
                }
                if (ataques.Count > 0)
                {
                    times[JogadorComum % times.Count].AdicionarJogadores(ataques.Dequeue());
                    JogadorComum++;
                }
            }

            // Se algum time ficou com menos jogadores, preenche com o que sobrou
            foreach (var time in times)
            {
                while (time.Jogadores.Count < jogadoresPorTime)
                {
                    if (goleiros.Count > 0) time.AdicionarJogadores(goleiros.Dequeue());
                    else if (defesas.Count > 0) time.AdicionarJogadores(defesas.Dequeue());
                    else if (ataques.Count > 0) time.AdicionarJogadores(ataques.Dequeue());
                    else break;
                }
            }

            return times;
        }

        public static List<Time> FormarTimesPorIdade(List<Jogador> jogadores, int jogadoresPorTime)
        {
            var times = new List<Time>();
            int codigoTime = 1;
            // Ordena jogadores por idade (do mais novo para o mais velho)
            var jogadoresOrdenados = new List<Jogador>(jogadores);
            jogadoresOrdenados.Sort((a, b) => a.Idade.CompareTo(b.Idade));

            int totalTimes = jogadores.Count / jogadoresPorTime;
            if (jogadores.Count % jogadoresPorTime != 0) totalTimes++;
            for (int i = 0; i < totalTimes; i++)
            {
                var time = new Time(codigoTime++, $"Time {codigoTime}", 0, Posicao.Goleiro, 0);
                times.Add(time);
            }

            // Distribui alternando entre mais novos e mais velhos
            int inicio = 0;
            int fim = jogadoresOrdenados.Count - 1;
            int indiceTime = 0;
            while (inicio <= fim)
            {
                if (inicio <= fim)
                {
                    times[indiceTime % times.Count].AdicionarJogadores(jogadoresOrdenados[inicio]);
                    indiceTime++;
                    inicio++;
                }
                if (inicio <= fim)
                {
                    times[indiceTime % times.Count].AdicionarJogadores(jogadoresOrdenados[fim]);
                    indiceTime++;
                    fim--;
                }
            }

            return times;
        }
    }
}
