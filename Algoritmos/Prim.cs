namespace Grafos.Estruturas
{
    public partial class Grafo
    {
        public Grafo ObterAgmPrim(Guid id)
        {
            var verticeInicial = Vertices.Keys.FirstOrDefault(x => x.IdVertice == id);

            if (verticeInicial == null)
                throw new KeyNotFoundException("Vértice não encontrado");

            var verticesSelecionados = new List<Vertice> { verticeInicial };
            var arestasSelecionadas = new List<Aresta>();

            while (verticesSelecionados.Count != Vertices.Count)
            {
                var arestasDisponiveis = new List<Aresta>();

                // Primeiro: coletar todas as arestas possíveis
                foreach (var vertice in verticesSelecionados)
                {
                    if (Vertices.TryGetValue(vertice, out var arestas))
                    {
                        foreach (var aresta in arestas)
                        {
                            // Se destino não está nos selecionados
                            if (!verticesSelecionados.Contains(aresta.VerticeDeDestino))
                                arestasDisponiveis.Add(aresta);
                        }
                    }
                }

                // Se não há arestas disponíveis, pegamos qualquer vértice não visitado
                if (!arestasDisponiveis.Any())
                {
                    var verticeNaoVisitado = Vertices.Keys
                        .FirstOrDefault(v => !verticesSelecionados.Contains(v));

                    if (verticeNaoVisitado != null)
                    {
                        verticesSelecionados.Add(verticeNaoVisitado);
                    }
                    continue;
                }

                // Depois: encontrar a de menor peso entre as disponíveis
                var arestaMenorPeso = arestasDisponiveis.MinBy(aresta => aresta.Peso);

                // Se encontrou uma aresta
                if (arestaMenorPeso != null)
                {
                    verticesSelecionados.Add(arestaMenorPeso.VerticeDeDestino);
                    arestasSelecionadas.Add(arestaMenorPeso);
                }
            }

            // Criar nova árvore
            var arvore = new Grafo(false);

            // Adicionar vértices
            foreach (var vertice in verticesSelecionados)
                arvore.AdicionarVertice(vertice);

            // Adicionar arestas
            foreach (var aresta in arestasSelecionadas)
            {
                arvore.AdicionarAresta(
                    aresta.VerticeDeOrigem,
                    aresta.VerticeDeDestino,
                    aresta.Peso
                );
            }

            return arvore;
        }
    }
}

