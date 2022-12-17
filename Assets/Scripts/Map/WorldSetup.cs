using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSetup : MonoBehaviour
{
    public int width;
    public int height;


    public float noiseScale;

    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    private CodeGrid<float> gridValues;
    private CodeGrid<GameObject> gridTiles;
    private Transform tileHolder;

    private Color[] color;
    public new Renderer renderer;
    public bool autoUpdate;

    //public Terrain[] terrain;
    public Terrain[] terrains;
    public Biome[] biomes;
    public float[,] noiseMap;
    Noise noiseMapFunc = new Noise();


    // Start is called before the first frame update
    public void SetupGrid()
    {
        noiseMap = noiseMapFunc.CreateNoiseMap(height, width, seed, noiseScale, octaves, persistance, lacunarity, offset);

        gridValues = new CodeGrid<float>(height, width, 10f, new Vector3(-width/2 * 10, -height/2 * 10, 0), () => 0.00f);

      

    }
    void WorldSetupMap()
    {
        SetupGrid();
        NoiseToTexture();
    }

    void NoiseToTexture()
    {
        float[,] noiseArr = noiseMapFunc.CreateNoiseMap(width, height, seed, noiseScale, octaves, persistance, lacunarity, offset);
        for (int y = 0; y < gridValues.GetHeight() ; y+=10)
        {
            for (int x = 0; x < gridValues.GetWidth(); x+=10)
            {
                
            }
        } 
        color = new Color[width * height];
        Texture2D texture = new Texture2D(width, height);
        Color[] colourMap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float currentHeight = noiseArr[x, y];
                gridValues.SetValue(x, y, currentHeight);
                for (int i = 0; i < terrains.Length; i++)
                {
                    if (currentHeight <= terrains[i].height)
                    {
                        colourMap[y * width + x] = terrains[i].color;
                        break;
                    }
                }
            }
        }

        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colourMap);
        texture.Apply();

        renderer.sharedMaterial.mainTexture = texture;
        renderer.transform.localScale = new Vector3(width, height,height );
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {

            Debug.Log(gridValues.GetValue(UtilsClass.GetMouseWorldPosition()));
        }
    }

    public void SetupScene()
    {
        WorldSetupMap();
    }
}

[System.Serializable]
public struct Terrain
{
    public string terrainType;
    public float height;
    public Color color;
}
[System.Serializable]
public struct Biome
{
    public string biomeName;
    public Terrain[] terrains;
}








