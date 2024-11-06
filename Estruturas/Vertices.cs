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

public class Vertice : IEquatable<Vertice>
{
    public Guid IdVertice { get; set; }
    public string Name { get; set; }
    public int Grau { get; set; }

    public DeepSearchData? DeepSearchData { get; private set; }

    public Vertice(Guid id, string name, int grau)
    {
        IdVertice = id;
        Name = name;
        DeepSearchData = new DeepSearchData();
        Grau = grau;
    }

    public Vertice() : this(Guid.NewGuid(), string.Empty, 0)
    {
    }

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

    public void SetFather(Vertice vertex)
    {
        if (DeepSearchData != null)
        {
            DeepSearchData.Father = vertex;
        }
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