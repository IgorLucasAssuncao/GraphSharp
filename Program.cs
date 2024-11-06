using System.Text.Json;
using static Grafos.ModelosJson.GrafoModeloJson;

namespace ProjetoGrafos
{
    public class Program
    {
        private const string caminhoArquivo = "grafo.json";

        public static void Main(string[] args)
        {

            string conteudoJson = File.ReadAllText(caminhoArquivo);
            var dadosGrafo = JsonSerializer.Deserialize<DadosGrafo>(conteudoJson);
            var grafoFinal = dadosGrafo?.ParaGrafo();

            var arvoreMinima = grafoFinal?.ObterAgmPrim(Guid.Parse("67e55044-10b1-426f-9247-bb680e5fe0c8"));

            var listaAdjacencia = arvoreMinima?.ParaListaAdjacencia();

            foreach (var elemento in listaAdjacencia?.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            {
                Console.WriteLine(elemento.Trim());
            }

            if (grafoFinal.BuscaEmProfundidade(Guid.Parse("67e55044-10b1-426f-9247-bb680e5fe0c8")))
                Console.WriteLine("Tem ciclo");

            var AgmKruskal = grafoFinal.ObterAgmKruskal();

            foreach (var item in AgmKruskal.ParaListaAdjacencia().Split(' ', StringSplitOptions.RemoveEmptyEntries))
            {
                Console.WriteLine(item.Trim());
            }
            Console.WriteLine($"Tem ciclo: {AgmKruskal.BuscaEmProfundidade(Guid.Parse("67e55044-10b1-426f-9247-bb680e5fe0c8"))}");

        }
    }
}