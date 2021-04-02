namespace GridPack.Pathfinding.DataStructs
{
    public interface IGraphNode
    {
        //Metoda zwraca dystans do IGraphNode który jest podany jako parametr
        int GetDistance(IGraphNode other);
    }
}

