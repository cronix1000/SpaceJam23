using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int width, height;
    public float noiseScale;

    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    private Color[] color;
    public new Renderer renderer;
    public bool autoUpdate;

    //public Terrain[] terrain;
    public Terrain[] terrains;
    public Biome[] biomes;
    Noise noiseMap = new Noise();
    // Start is called before the first frame update
    void Start()
    {
        NoiseToTexture();
    }

    public void GenerateMap()
    {
        NoiseToTexture();
    }
                                                                                      
    void NoiseToTexture()
    {
        int plotWidth = (int)Mathf.Floor(width / 3);
        int plotHeight = (int)Mathf.Floor(width / 3);
        float[,] noiseArr = noiseMap.CreateNoiseMap(width, height, seed, noiseScale, octaves, persistance, lacunarity, offset);
        color = new Color[width * height];
        Texture2D texture = new Texture2D(width, height);
        Color[] colourMap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float currentHeight = noiseArr[x, y];
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
        renderer.transform.localScale = new Vector3(width, 1, height);
    }
}
