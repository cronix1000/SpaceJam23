using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupMap : MonoBehaviour
{
    public int height;
    public int width;


    public float noiseScale;

    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;
    
    public TerrainType[] regions;
    private Grid<float> gridValues;
    private Grid<GameObject> gridTiles;
    private Transform tileHolder;
    public void SetupGrid()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(height, width, seed, noiseScale, octaves, persistance, lacunarity, offset);

        gridValues = new Grid<float>(height, width, 10f, Vector3.zero);
        for (int x = 0; x < gridValues.width; x++)
        {
            for (int y = 0; y < gridValues.height; y++)
            {
                gridValues.SetValue(x, y, noiseMap[x, y]);
            }
        }

        //make 2 for loops to go through the gridTiles and set the values
        gridTiles = new Grid<GameObject>(height, width, 10f, Vector3.zero);
        for (int x = 0; x < gridTiles.width; x++)
        {
            for (int y = 0; y < gridTiles.height; y++)
            {
                switch (gridValues.GetValue(x, y))
                {
                    case float n when (n <= 0.05f):
                        gridTiles.SetValue(x, y, Instantiate(Resources.Load("Prefabs/DeepWater"), new Vector3(x, y, 0), Quaternion.identity) as GameObject);
                        break;
                    case float n when (n > 0.15f && n <= 0.2f):
                        gridTiles.SetValue(x, y, Instantiate(Resources.Load("Prefabs/Water"), new Vector3(x, y, 0), Quaternion.identity) as GameObject);
                        break;
                    case float n when (n > 0.2f && n <= 0.25f):
                        gridTiles.SetValue(x, y, Instantiate(Resources.Load("Prefabs/ShallowWater"), new Vector3(x, y, 0), Quaternion.identity) as GameObject);
                        break;
                    case float n when (n > 0.25f && n <= 0.4f):
                        gridTiles.SetValue(x, y, Instantiate(Resources.Load("Prefabs/ShallowWaterSand"), new Vector3(x, y, 0), Quaternion.identity) as GameObject); 
                        break;
                    case float n when (n > 0.4f && n <= 0.6f):
                        gridTiles.SetValue(x, y, Instantiate(Resources.Load("Prefabs/Sand"), new Vector3(x, y, 0), Quaternion.identity) as GameObject);
                        break;
                    case float n when (n > 0.6f && n <= 0.8f):
                        gridTiles.SetValue(x, y, Instantiate(Resources.Load("Prefabs/Grass"), new Vector3(x, y, 0), Quaternion.identity) as GameObject);
                        break;
                    case float n when (n > 0.8f && n <= 1f):
                        gridTiles.SetValue(x, y, Instantiate(Resources.Load("Prefabs/GrassDark"), new Vector3(x, y, 0), Quaternion.identity) as GameObject);
                        break;
                }
            }
        }

    }

    void WorldSetup() {
        tileHolder = new GameObject("Board").transform;
        SetupGrid();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject toInstantiate;

                toInstantiate = gridTiles.GetValue(x, y);

                //Instantiates the object 
                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                //Sets the object being used to the holder e.i boardHolder to avoid having messy hierarchy
                instance.transform.SetParent(tileHolder);

            }
        }
    }

    public void SetupScene()
    {
        WorldSetup();
    }


}
public struct TerrainType
{
    public string name;
    public float height;
    public Color colour;
}