namespace Grafos.Estruturas
{

    public class HelperDijkstra
    {
        public Aresta Aresta { get; set; }
        public int Distancia { get; set; }
    }
    public partial class Grafo
    {
        public void Dijkstra(Guid IdInicio)
        {
            DadosVertice.Values.ToList().ForEach(x => x.ResetDijkstraData());

            var verticeDeInicio = DadosVertice.Values.FirstOrDefault(x => x.IdVertice == IdInicio);
            var verticesSelecionados = new List<Vertice>
            {
                verticeDeInicio
            };

            verticeDeInicio.setDistanceDk(0);

            while (verticesSelecionados.Count < DadosVertice.Count())
            {
                var arestasPossiveis = new List<HelperDijkstra>();

                // Primeiro: coletar todas as arestas possíveis
                foreach (var vertice in verticesSelecionados)
                {
                    if (Vertices.TryGetValue(vertice, out var arestas))
                    {
                        foreach (var aresta in arestas)
                        {
                            // Se destino está fora do conjunto de S
                            if (!verticesSelecionados.Contains(aresta.VerticeDeDestino))
                            {
                                arestasPossiveis.Add(new HelperDijkstra
                                {
                                    Aresta = aresta,
                                    Distancia = aresta.VerticeDeOrigem.getDistanceDk() + aresta.Peso
                                });

                            }
                        }
                    }
                }

                var menorCaminho = arestasPossiveis.MinBy(x => x.Distancia);

                if (menorCaminho == null)
                    break;

                menorCaminho.Aresta.VerticeDeDestino
                    .setFatherDk(menorCaminho.Aresta.VerticeDeOrigem)
                    .setDistanceDk(menorCaminho.Distancia);
                verticesSelecionados.Add(menorCaminho.Aresta.VerticeDeDestino);

            }
        }
        public void ImprimirTabelaDijkstra()
        {
            Console.WriteLine("Tabela de Distâncias e Predecessores (Dijkstra):");
            Console.WriteLine("Vértice\t\tDistância\tPredecessor");

            foreach (var vertice in Vertices.Keys)
            {
                var dijkstraData = vertice.DijkstraData;
                var distancia = dijkstraData.Distance == int.MaxValue ? "∞" : dijkstraData.Distance.ToString();
                var predecessor = dijkstraData.Father?.Name ?? "-";

                Console.WriteLine($"{vertice.Name}\t\t{distancia}\t\t{predecessor}");
            }
        }
    }
}
