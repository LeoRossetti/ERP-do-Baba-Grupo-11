namespace ProjetoFutebol.Models
{
    public enum Posicao { Goleiro, Defesa, Ataque }

    public class Jogador
    {
        public int Codigo { get; set; }
        public string Nome { get; set; } = "";
        public int Idade { get; set; }
        public Posicao Posicao { get; set; }

        public int Pontos { get; private set; }

        public Jogador(int codigo, string nome, int idade, Posicao posicao, int pontos)
        {
            Codigo = codigo;
            Nome = nome;
            Idade = idade;
            Posicao = posicao;
            Pontos = pontos;
        }

        public void RegistrarResultado(bool venceu, bool empate)
        {
            if (venceu) Pontos += 3;
            else if (empate) Pontos += 1;
        }
    }
}
