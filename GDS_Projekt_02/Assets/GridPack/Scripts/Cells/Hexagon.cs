using System.Collections.Generic;
using UnityEngine;

namespace GridPack.Cells
{

    //Implementacja Komórki heksagonalnej 
 
    public abstract class Hexagon : Cell
    {
        List<Cell> neighbours = null;

        //Gridy heksagonalne występują w 4 typach 
        //To rozróznienie jest wymagane do własciwego przekonwertowania koordynatów cuba na przesunięcie i odwrotnie
        [HideInInspector]
        public HexGridType HexGridType;


        //Konwertuje przesunięte wspołrzędne na współrzędne cuba 
        // System ten uławtia obliczenia współrzędnych na siatkach heksadecymalnych 
        protected Vector3 CubeCoord
        {
            get
            {
                Vector3 ret = new Vector3();
                switch (HexGridType)
                {
                    case HexGridType.odd_q:
                        {
                            ret.x = OffsetCoord.x;
                            ret.z = OffsetCoord.y - (OffsetCoord.x + (Mathf.Abs(OffsetCoord.x) % 2)) / 2;
                            ret.y = -ret.x - ret.z;
                            break;
                        }
                    case HexGridType.even_q:
                        {
                            ret.x = OffsetCoord.x;
                            ret.z = OffsetCoord.y - (OffsetCoord.x - (Mathf.Abs(OffsetCoord.x) % 2)) / 2;
                            ret.y = -ret.x - ret.z;
                            break;
                        }
                }
                return ret;
            }
        }

        //Konwertuje spowrotem do 2D 
        protected Vector2 CubeToOffsetCoords(Vector3 cubeCoords)
        {
            Vector2 ret = new Vector2();

            switch (HexGridType)
            {
                case HexGridType.odd_q:
                    {
                        ret.x = cubeCoords.x;
                        ret.y = cubeCoords.z + (cubeCoords.x + (Mathf.Abs(cubeCoords.x) % 2)) / 2;
                        break;
                    }
                case HexGridType.even_q:
                    {
                        ret.x = cubeCoords.x;
                        ret.y = cubeCoords.z + (cubeCoords.x - (Mathf.Abs(cubeCoords.x) % 2)) / 2;
                        break;
                    }
            }
            return ret;
        }

        protected static readonly Vector3[] _directions =  {
        new Vector3(+1, -1, 0), new Vector3(+1, 0, -1), new Vector3(0, +1, -1),
        new Vector3(-1, +1, 0), new Vector3(-1, 0, +1), new Vector3(0, -1, +1)};

        public override int GetDistance(Cell other)
        {
            var _other = other as Hexagon;
            int distance = (int)(Mathf.Abs(CubeCoord.x - _other.CubeCoord.x) + Mathf.Abs(CubeCoord.y - _other.CubeCoord.y) + Mathf.Abs(CubeCoord.z - _other.CubeCoord.z)) / 2;
            return distance;
            //Dystans obliczony przy uzyciu normy manhattańskiej czyli jako suma wartości bezwględnych róznic współrzędnych punktów w cube. 
        }

        public override List<Cell> GetNeighbours(List<Cell> cells)
        {
            if (neighbours == null)
            {
                neighbours = new List<Cell>(6);
                foreach (var direction in _directions)
                {
                    var neighbour = cells.Find(c => c.OffsetCoord == CubeToOffsetCoords(CubeCoord + direction));
                    if (neighbour == null) continue;
                    neighbours.Add(neighbour);
                }
            }
            return neighbours;

        }// Kazda komórka heksagonalna ma szesciu sąsiadów, których pozycje na siatce względem komórki są przechowywane w stałej _directions. 

        public override void CopyFields(Cell newCell)
        {
            newCell.OffsetCoord = OffsetCoord;
            (newCell as Hexagon).HexGridType = HexGridType;
        }
    }

    public enum HexGridType
    {
        even_q,
        odd_q,
        even_r,
        odd_r
    };
}