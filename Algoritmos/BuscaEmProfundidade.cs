using static Grafos.Enums;

namespace Grafos.Estruturas
{
    public partial class Grafo
    {
        private int Tempo = 0;

        public bool BuscaEmProfundidade(Guid IdVertice)
        {
            Tempo = 0;

            DadosVertice.Values.ToList().ForEach(vertice => vertice.ResetSearchData()); //Resetar dados de busca

            while (DadosVertice.Values.Any(vertice => vertice.GetDiscoveryTime() == 0))
            {
                if (Tempo != 0)// Para Grafos desconexos
                    IdVertice = DadosVertice.Values.First(vertice => vertice.GetDiscoveryTime() == 0).IdVertice;

                var vertice = DadosVertice[IdVertice];
                BuscaEmProfundidade(vertice);
            }
            if (Vertices.Values.SelectMany(arestas => arestas).Any(aresta => aresta.Classificacao == ClassificacaoAresta.ArestaDeRetorno))
                return true;

            return false;
        }

        private void BuscaEmProfundidade(Vertice vertice)
        {
            Tempo = Tempo + 1;
            vertice.SetDiscoveryTime(Tempo);

            foreach (var (vizinho, aresta) in Vertices[vertice].Select(arestas => (arestas.VerticeDeDestino, arestas)))
            {
                if (vizinho.GetDiscoveryTime() == 0)
                {
                    aresta.Classificacao = ClassificacaoAresta.ArestaArvore;
                    vizinho.SetFather(vertice);
                    BuscaEmProfundidade(vizinho);
                }
                else if (EhDirecionado)
                {
                    //Se for direcionado vai por esse caminho
                    if (vizinho.GetFinishTime() == 0)
                    {
                        aresta.Classificacao = ClassificacaoAresta.ArestaDeRetorno;
                    }
                    else if (vizinho.GetDiscoveryTime() > vertice.GetDiscoveryTime())
                    {
                        aresta.Classificacao = ClassificacaoAresta.ArestaDeAvanco;
                    }
                    else
                    {
                        aresta.Classificacao = ClassificacaoAresta.ArestaDeCruzamento;
                    }
                }
                else if (vizinho.GetFinishTime() == 0 && vizinho.IdVertice != vertice.GetFather()?.IdVertice)
                {
                    aresta.Classificacao = ClassificacaoAresta.ArestaDeRetorno;
                }
            }
            Tempo = Tempo + 1;
            vertice.SetFinishTime(Tempo);
        }
    }
}
