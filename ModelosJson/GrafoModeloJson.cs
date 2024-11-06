using Grafos.Estruturas;
using System.Text.Json.Serialization;
using static Grafos.Enums;

namespace Grafos.ModelosJson
{
    public class GrafoModeloJson
    {
        public class DadosAresta
        {
            [JsonPropertyName("id")]
            public Guid Id { get; set; }

            [JsonPropertyName("origem")]
            public Guid Origem { get; set; }

            [JsonPropertyName("destino")]
            public Guid Destino { get; set; }

            [JsonPropertyName("peso")]
            public int Peso { get; set; }
        }

        public class DadosVertice
        {
            [JsonPropertyName("id")]
            public Guid Id { get; set; }

            [JsonPropertyName("nome")]
            public string Nome { get; set; }

            public DadosVertice Clone()
            {
                return new DadosVertice
                {
                    Id = Id,
                    Nome = Nome
                };
            }
        }

        public class DadosGrafo
        {
            [JsonPropertyName("ehDirecionado")]
            public bool EhDirecionado { get; set; }

            [JsonPropertyName("arestas")]
            public List<DadosAresta>? Arestas { get; set; }

            [JsonPropertyName("vertices")]
            public List<DadosVertice>? Vertices { get; set; }

            public Grafo ParaGrafo()
            {
                var mapaVertices = new Dictionary<Vertice, List<Aresta>>();
                var mapaVerticesAuxiliar = new Dictionary<Guid, Vertice>();

                // Criar vértices
                foreach (var item in Vertices)
                {
                    var vertice = new Vertice(item.Id, item.Nome, 0);
                    mapaVertices.Add(vertice, new List<Aresta>());
                    mapaVerticesAuxiliar.Add(item.Id, vertice);
                }

                // Criar arestas
                foreach (var item in Arestas)
                {
                    if (mapaVerticesAuxiliar.TryGetValue(item.Origem, out var origem) && mapaVerticesAuxiliar.TryGetValue(item.Destino, out var destino))
                    {
                        var aresta = new Aresta(item.Id, item.Peso, origem, destino, ClassificacaoAresta.Nenhuma);

                        // Adiciona aresta no vértice de origem
                        if (mapaVertices.TryGetValue(origem, out var arestasOrigem))
                        {
                            arestasOrigem.Add(aresta);
                            origem.Grau++;
                        }

                        // Se não for direcionado, adiciona também no destino
                        if (!EhDirecionado)
                        {
                            if (mapaVertices.TryGetValue(destino, out var arestasDestino))
                            {
                                var arestaReversa = new Aresta(item.Id, item.Peso, destino, origem, ClassificacaoAresta.Nenhuma); //Inverte origem e destino
                                arestasDestino.Add(arestaReversa);
                                destino.Grau++;
                            }
                        }
                    }
                }

                return new Grafo(
                    EhDirecionado,
                    mapaVertices,
                    mapaVerticesAuxiliar,
                    Vertices.Count,
                    EhDirecionado ? Arestas.Count : Arestas.Count * 2
                );
            }
        }
    }
}
