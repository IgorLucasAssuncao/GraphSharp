namespace Grafos.Estruturas
{
    public partial class Grafo
    {
        public Grafo ObterAgmKruskal()
        {
            var agm = new Grafo(false);
            var arestasOrdenadas = new List<Aresta>(Vertices.SelectMany(v => v.Value).DistinctBy(aresta => aresta.IdAresta).OrderBy(a => a.Peso)); //Seleciona todas as arestas (sem repetição)

            Vertices.Keys.ToList().ForEach(agm.AdicionarVertice); //Adiciona todos os vértices na AGM

            var arestasInseridasNaAgm = new List<Aresta>();

            do
            {
                var arestaParaInserir = arestasOrdenadas.First();
                agm.AdicionarAresta(arestaParaInserir.VerticeDeOrigem, arestaParaInserir.VerticeDeDestino, arestaParaInserir.Peso);

                if (!agm.BuscaEmProfundidade(Vertices.Keys.First().IdVertice))
                {
                    arestasOrdenadas.Remove(arestaParaInserir);
                    arestasInseridasNaAgm.Add(arestaParaInserir);
                }
                else
                {
                    agm.RemoverAresta(arestaParaInserir);
                    arestasOrdenadas.Remove(arestaParaInserir);
                }
            }
            while (arestasInseridasNaAgm.Count < Vertices.Count - 1);

            return agm;
        }
    }
}
