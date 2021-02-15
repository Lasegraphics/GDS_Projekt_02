using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerate : MonoBehaviour
{
    public Transform HexPrefab; 
    public int GridWidth = 11;
    public int gridHeight = 11; 
    float hexWidth = 1.732f;
    float hexHight = 2f;
    public float gap = 0.0f; 
    Vector3 startPosition; 

    private void Start() 
    {
            AddGap();
            CalculateStart();
            CreateGrid();
    }

    void AddGap ()
    {
        hexWidth += hexWidth * gap;
        hexHight += hexHight * gap; 
    }

    void CalculateStart()
    {
        float offset = 0;
        if(gridHeight / 2 % 2 != 0)
            offset = hexWidth / 2; 
        
        float x = -hexWidth * (GridWidth / 2) - offset; 
        float y =  hexHight * 0.75f * (gridHeight / 2);

        startPosition = new Vector3(x, y, 0);
    }

    Vector3 CalcWorldPos(Vector2 gridPos)
    {
        float offset = 0; 
        if (gridPos.y % 2 != 0)
            offset = hexWidth / 2; 
        
        float x = startPosition.x + gridPos.x * hexWidth + offset;
        float y = startPosition.y - gridPos.y * hexHight * 0.75f;
        return new Vector3(x, y,0);
    }
    void CreateGrid()
    {
        for(int y =0; y< gridHeight; y++)
        {
            for (int x = 0; x < GridWidth; x++)
            {
                Transform hex = Instantiate(HexPrefab) as Transform; 
                Vector2 gridPos = new Vector2(x,y);
                hex.position = CalcWorldPos(gridPos);
                hex.parent = this.transform; 
                hex.name = "Hexagon" + x + "|" + y;
            }
        }
    }
}
