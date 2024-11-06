namespace Grafos.Estruturas
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using static global::Grafos.Enums;

    public partial class Grafo
    {
        public bool EhDirecionado { get; private set; }
        public Dictionary<Vertice, List<Aresta>> Vertices { get; private set; }
        public Dictionary<Guid, Vertice> DadosVertice { get; private set; }
        public int TotalVertices { get; private set; }
        public int TotalArestas { get; private set; }

        public Grafo(
            bool eDirecionado,
            Dictionary<Vertice, List<Aresta>> vertices,
            Dictionary<Guid, Vertice> dadosVertice,
            int totalVertices,
            int totalArestas)
        {
            EhDirecionado = eDirecionado;
            Vertices = vertices;
            DadosVertice = dadosVertice;
            TotalVertices = totalVertices;
            TotalArestas = totalArestas;
        }

        public Grafo(bool eDirecionado) : this(
            eDirecionado,
            new Dictionary<Vertice, List<Aresta>>(),
            new Dictionary<Guid, Vertice>(),
            0,
            0)
        {
        }

        public void AdicionarVertice(Vertice vertice)
        {
            TotalVertices++;
            Vertices[vertice] = new List<Aresta>();
            DadosVertice[vertice.IdVertice] = vertice;
        }

        public void AdicionarAresta(Vertice verticeOrigem, Vertice verticeDestino, int peso)
        {
            if (Vertices.ContainsKey(verticeOrigem))
            {
                Vertices[verticeOrigem].Add(new Aresta(
                    Guid.NewGuid(),
                    peso,
                    verticeOrigem,
                    verticeDestino,
                    ClassificacaoAresta.Nenhuma));
            }

            if (!EhDirecionado)
            {
                if (Vertices.ContainsKey(verticeDestino))
                {
                    Vertices[verticeDestino].Add(new Aresta(
                        Guid.NewGuid(),
                        peso,
                        verticeDestino,
                        verticeOrigem,
                        ClassificacaoAresta.Nenhuma));
                }
                TotalArestas += 2;
            }
            else
            {
                TotalArestas++;
            }
        }

        public void RemoverAresta(Aresta aresta)
        {
            if (Vertices.ContainsKey(aresta.VerticeDeOrigem))
            {
                var arestaParaRemover = Vertices[aresta.VerticeDeOrigem].Find(a => a.VerticeDeDestino.IdVertice == aresta.VerticeDeDestino.IdVertice);

                if (arestaParaRemover != null)
                {
                    Vertices[aresta.VerticeDeOrigem].RemoveAll(aresta => aresta.IdAresta == arestaParaRemover.IdAresta);
                    aresta.VerticeDeOrigem.Grau--;
                }
            }

            if (!EhDirecionado && Vertices.ContainsKey(aresta.VerticeDeDestino))
            {
                var arestaParaRemover = Vertices[aresta.VerticeDeDestino].Find(a => a.VerticeDeDestino.IdVertice == aresta.VerticeDeOrigem.IdVertice);

                if (arestaParaRemover != null)
                {
                    Vertices[aresta.VerticeDeDestino].RemoveAll(aresta => aresta.IdAresta == arestaParaRemover.IdAresta);
                    aresta.VerticeDeDestino.Grau--;
                }
            }

            TotalArestas = EhDirecionado ? TotalArestas - 1 : TotalArestas - 2;

        }

        public Vertice? ObterVertice(Func<Vertice, bool> expressao)
        {
            var vertice = Vertices.Keys.FirstOrDefault(expressao);

            if (vertice != null)
                return vertice;

            return null;
        }

        public string ParaListaAdjacencia()
        {
            var resultado = new StringBuilder();

            foreach (var (vertice, arestas) in Vertices)
            {
                resultado.Append($"{vertice.Name}: ");

                var vizinhos = arestas.Select(aresta =>
                {
                    if (EhDirecionado)
                    {
                        return $"{aresta.VerticeDeDestino.Name}({aresta.Peso})->";
                    }
                    return $"{aresta.VerticeDeDestino.Name}({aresta.Peso})";
                });

                resultado.Append(string.Join(", ", vizinhos));
                resultado.Append(" ");
            }

            return resultado.ToString();
        }
    }
}
