using System;

namespace GridPack.Pathfinding.DataStructs
{
    //Reprezentuje kolejkę piorytetową. 
    public interface IPriorityQueue<T>
    {   //Pobiera liczbę przedmiotów w kolejce.  
         int Count {get;}
        //Metoda dodaje przedmioty do kolejki 
        void Enqueue(T item, float priority);

        //Metoda zwraca przedmiot z najnizszym piorytetem w kolejce 
        T Dequeue();
    }

    //Klasa reprezentuje wierzchołek w kolejce piorytetowej 
    class PriorityQueueNode<T> : IComparable
    {
        public T Item {get; private set;}
        public float Priority {get; private set;}

        public PriorityQueueNode(T item, float priority)
        {
            Item = item; 
            Priority = priority;
        }

        public int CompareTo(object obj)
        {
            return Priority.CompareTo((obj as PriorityQueueNode<T>).Priority);
        }
    }
}
