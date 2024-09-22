using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsumirApiGitHub
{
    // Classe que representa os dados do usuário do GitHub
    public class UsuarioGitHub
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            // URL do endpoint
            string url = "https://api.github.com/user/1";

            using (HttpClient client = new HttpClient())
            {
                // Adicionando User-Agent no cabeçalho da requisição
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("MyApp", "1.0"));

                try
                {
                    // Fazendo a requisição GET para o endpoint
                    HttpResponseMessage response = await client.GetAsync(url);

                    // Verificando se a requisição foi bem-sucedida
                    response.EnsureSuccessStatusCode();

                    // Lendo o conteúdo da resposta
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Configurando opções de desserialização para ignorar maiúsculas e minúsculas
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    // Desserializando o JSON para o objeto UsuarioGitHub
                    UsuarioGitHub usuario = JsonSerializer.Deserialize<UsuarioGitHub>(responseBody, options);

                    // Imprimindo os dados solicitados no console
                    Console.WriteLine("Nome: " + usuario.Name);
                    Console.WriteLine("Empresa: " + usuario.Company);
                    Console.WriteLine("Localização: " + usuario.Location);
                    Console.WriteLine("Login: " + usuario.Login);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Erro na requisição: {e.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocorreu um erro: {ex.Message}");
                }
            }
        }
    }
}