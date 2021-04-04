using System.Collections.Generic;
using System.Linq;
using GridPack.Pathfinding.DataStructs;

namespace GridPack.Pathfinding.Algorithms
{
    public abstract class IPathfinding
    {
        //Metoda znajduje ściezkę pomiędzy dwoma wierzchołkami w grafie 
        //Krawędzie grafu są przedstawione jako zagniezdzone słowniki. Słownik zewnętrzny zawiera wszystkie węzły w grafie. Słownik wewnętrzny zawiera wewnętrzne sasiadujące węzły z wagą krawędzi. 
        //Jeśli sciezka istnieje metoda zwraca listę węzłów, z których składa się ściezka. W przeciwnym razie zwracana jest pusta lista. 
        public abstract List<T> FindPath<T>(Dictionary<T, Dictionary<T, float>> edges, T originNode, T destinationNode) where T : IGraphNode;
        protected List<T> GetNeigbours<T>(Dictionary<T, Dictionary<T, float>> edges, T node) where T : IGraphNode
        {
            if (!edges.ContainsKey(node))
            {
                return new List<T>();
            }
            return edges[node].Keys.ToList();
        }
    }
}