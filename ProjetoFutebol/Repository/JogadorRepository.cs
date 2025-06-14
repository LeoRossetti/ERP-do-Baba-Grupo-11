using ProjetoFutebol.Models;
using MySql.Data.MySqlClient;

namespace ProjetoFutebol.Data
{
    public class JogadorRepository
    {
        private readonly string _connString = "Server=localhost;Database=futebol;Uid=root;Pwd=senha;";

        // Método para inserir jogador no banco de dados
        public void Inserir(Jogador j)
        {
            try
            {
                using var conn = new MySqlConnection(_connString);
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO jogadores (codigo, nome, idade, posicao) VALUES (@c, @n, @i, @p)";
                cmd.Parameters.AddWithValue("@c", j.Codigo);
                cmd.Parameters.AddWithValue("@n", j.Nome);
                cmd.Parameters.AddWithValue("@i", j.Idade);
                cmd.Parameters.AddWithValue("@p", j.Posicao.ToString());
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                // Tratar exceção de banco de dados
                Console.WriteLine($"Erro ao inserir jogador: {ex.Message}");
            }
        }

        // Método para listar todos os jogadores
        public List<Jogador> Listar()
        {
            var jogadores = new List<Jogador>();
            try
            {
                using var conn = new MySqlConnection(_connString);
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT codigo, nome, idade, posicao FROM jogadores";
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    jogadores.Add(new Jogador(
                        reader.GetInt32("codigo"),
                        reader.GetString("nome"),
                        reader.GetInt32("idade"),
                        Enum.Parse<Posicao>(reader.GetString("posicao"))
                    ));
                }
            }
            catch (MySqlException ex)
            {
                // Tratar exceção de banco de dados
                Console.WriteLine($"Erro ao listar jogadores: {ex.Message}");
            }
            return jogadores;
        }

        // Método para atualizar dados do jogador
        public void Atualizar(Jogador j)
        {
            try
            {
                using var conn = new MySqlConnection(_connString);
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE jogadores SET nome = @n, idade = @i, posicao = @p WHERE codigo = @c";
                cmd.Parameters.AddWithValue("@n", j.Nome);
                cmd.Parameters.AddWithValue("@i", j.Idade);
                cmd.Parameters.AddWithValue("@p", j.Posicao.ToString());
                cmd.Parameters.AddWithValue("@c", j.Codigo);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro ao atualizar jogador: {ex.Message}");
            }
        }

        // Método para remover jogador
        public void Remover(int codigo)
        {
            try
            {
                using var conn = new MySqlConnection(_connString);
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM jogadores WHERE codigo = @c";
                cmd.Parameters.AddWithValue("@c", codigo);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro ao remover jogador: {ex.Message}");
            }
        }

        // Método para buscar jogador por código
        public Jogador? BuscarPorCodigo(int codigo)
        {
            try
            {
                using var conn = new MySqlConnection(_connString);
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT codigo, nome, idade, posicao FROM jogadores WHERE codigo = @c";
                cmd.Parameters.AddWithValue("@c", codigo);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Jogador(
                        reader.GetInt32("codigo"),
                        reader.GetString("nome"),
                        reader.GetInt32("idade"),
                        Enum.Parse<Posicao>(reader.GetString("posicao"))
                    );
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro ao buscar jogador: {ex.Message}");
            }
            return null;
        }
    }
}