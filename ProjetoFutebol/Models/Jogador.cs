namespace ProjetoFutebol.Models
{
    public enum Posicao { Goleiro, Defesa, Ataque }

    public class Jogador
    {
        public int Codigo { get; set; }
        public string Nome { get; set; } = "";
        public int Idade { get; set; }
        public Posicao Posicao { get; set; }

        public Jogador(int codigo, string nome, int idade, Posicao posicao)
        {
            Codigo = codigo;
            Nome = nome;
            Idade = idade;
            Posicao = posicao;
        }
    }
}
