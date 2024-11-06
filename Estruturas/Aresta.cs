using static Grafos.Enums;

namespace Grafos.Estruturas
{

    public class Aresta : IEquatable<Aresta>
    {
        public Guid IdAresta { get; set; }
        public int Peso { get; set; }
        public Vertice VerticeDeOrigem { get; set; }
        public Vertice VerticeDeDestino { get; set; }
        public ClassificacaoAresta Classificacao { get; set; }

        public Aresta(Guid id, int peso, Vertice verticeOrigem, Vertice verticeDestino, ClassificacaoAresta classificacao)
        {
            IdAresta = id;
            Peso = peso;
            VerticeDeOrigem = verticeOrigem;
            VerticeDeDestino = verticeDestino;
            Classificacao = classificacao;
        }

        public Aresta() : this(Guid.NewGuid(), 0, new Vertice(), new Vertice(), ClassificacaoAresta.Nenhuma)
        {
        }

        public bool Equals(Aresta? outra)
        {
            if (outra is null)
                return false;

            return IdAresta == outra.IdAresta;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Aresta);
        }

        public override int GetHashCode()
        {
            return IdAresta.GetHashCode();
        }
    }
}




