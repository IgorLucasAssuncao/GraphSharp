namespace Grafos.Estruturas;
public class DeepSearchData
{
    public int DiscoveryTime { get; set; }
    public int FinishTime { get; set; }
    public Vertice? Father { get; set; }

    public DeepSearchData()
    {
        DiscoveryTime = 0;
        FinishTime = 0;
        Father = null;
    }
}

public class DijkstraData
{
    public int Distance { get; set; }
    public Vertice? Father { get; set; }

    public DijkstraData()
    {
        Distance = int.MaxValue;
        Father = null;
    }
}

public class Vertice : IEquatable<Vertice>
{
    public Guid IdVertice { get; set; }
    public string Name { get; set; }
    public int Grau { get; set; }

    #region Atributos para algoritmos

    public DeepSearchData? DeepSearchData { get; set; }

    public DijkstraData? DijkstraData { get; set; }

    #endregion Atributos para algoritmos


    public Vertice(Guid id, string name, int grau)
    {
        IdVertice = id;
        Name = name;
        DeepSearchData = new DeepSearchData();
        DijkstraData = new DijkstraData();
        Grau = grau;
    }

    public Vertice() : this(Guid.NewGuid(), string.Empty, 0)
    {
    }

    #region DeepSearch

    public void SetDiscoveryTime(int value)
    {
        if (DeepSearchData != null)
        {
            DeepSearchData.DiscoveryTime = value;
        }
    }

    public void SetFinishTime(int value)
    {
        if (DeepSearchData != null)
        {
            DeepSearchData.FinishTime = value;
        }
    }

    public Vertice SetFather(Vertice vertex)
    {
        DeepSearchData.Father = vertex;
        return this;
    }

    public Vertice? GetFather()
    {
        return DeepSearchData.Father;
    }


    public int GetFinishTime()
    {
        return DeepSearchData?.FinishTime ?? 0;
    }

    public int GetDiscoveryTime()
    {
        return DeepSearchData?.DiscoveryTime ?? 0;
    }

    public void ResetSearchData()
    {
        DeepSearchData = new DeepSearchData();
    }

    #endregion DeepSearch

    #region Dk
    public int getDistanceDk()
    {
        return DijkstraData?.Distance ?? 0;
    }

    public Vertice? getFatherDk()
    {
        return DijkstraData?.Father;
    }

    public Vertice setFatherDk(Vertice vertex)
    {
        if (DijkstraData != null)
        {
            DijkstraData.Father = vertex;
        }

        return this;
    }

    public void setDistanceDk(int value)
    {
        if (DijkstraData != null)
        {
            DijkstraData.Distance = value;
        }
    }

    public void ResetDijkstraData()
    {
        DijkstraData = new DijkstraData();
    }

    #endregion Dk

    public bool Equals(Vertice? other)
    {
        if (other is null)
            return false;

        return IdVertice == other.IdVertice;
    }
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj is not Vertice) return false;

        return Equals(obj as Vertice);
    }

    public override int GetHashCode()
    {
        return IdVertice.GetHashCode();
    }
}