using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator2D : MonoBehaviour
{
    [Header("Bruit")]
    public NoiseSettings noise;

    [Header("Tilemap cible")]
    public Tilemap tilemap;

    private TerrainType[] terrains = new TerrainType[]
    {
        new TerrainType { name = "DeepOcean",    color = new Color(0.10f, 0.30f, 0.70f), threshold = 0.15f },
        new TerrainType { name = "Water",color = new Color(0.20f, 0.50f, 0.85f), threshold = 0.25f },
        new TerrainType { name = "Sand",           color = new Color(0.90f, 0.85f, 0.60f), threshold = 0.30f },
        new TerrainType { name = "Grass",           color = new Color(0.30f, 0.70f, 0.30f), threshold = 0.40f },
        new TerrainType { name = "MidGrass",           color = new Color(0.90f, 0.85f, 0.60f), threshold = 0.50f },
        new TerrainType { name = "HighGrass",           color = new Color(0.10f, 0.45f, 0.15f), threshold = 0.62f },
        new TerrainType { name = "Montain",           color = new Color(0.55f, 0.50f, 0.45f), threshold = 0.70f },
        new TerrainType { name = "MidMontain",           color = new Color(0.55f, 0.50f, 0.45f), threshold = 0.80f },
        new TerrainType { name = "HighMontain",           color = new Color(0.55f, 0.50f, 0.45f), threshold = 0.90f },
        new TerrainType { name = "Snow",           color = new Color(0.95f, 0.95f, 1.00f), threshold = 1.00f },
    };

    [Header("Event")]
    public RSE_MapGenerated mapGenerated;
    public RSO_TilemapInfo tilemapInfo;
    public RSO_BlockPrefabStorage blockPrefabStorage;

    private void Start()
    {
        terrains = new TerrainType[] {new TerrainType { name = "DeepOcean",    color = new Color(0.10f, 0.30f, 0.70f), threshold = 0.15f },
        new TerrainType { name = "Water",color = new Color(0.20f, 0.50f, 0.85f), threshold = 0.25f },
        new TerrainType { name = "Sand",           color = new Color(0.90f, 0.85f, 0.60f), threshold = 0.30f },
        new TerrainType { name = "Grass",           color = new Color(0.30f, 0.70f, 0.30f), threshold = 0.40f },
        new TerrainType { name = "MidGrass",           color = new Color(0.90f, 0.85f, 0.60f), threshold = 0.50f },
        new TerrainType { name = "HighGrass",           color = new Color(0.10f, 0.45f, 0.15f), threshold = 0.62f },
        new TerrainType { name = "Montain",           color = new Color(0.55f, 0.50f, 0.45f), threshold = 0.70f },
        new TerrainType { name = "MidMontain",           color = new Color(0.55f, 0.50f, 0.45f), threshold = 0.80f },
        new TerrainType { name = "HighMontain",           color = new Color(0.55f, 0.50f, 0.45f), threshold = 0.90f },
        new TerrainType { name = "Snow",           color = new Color(0.95f, 0.95f, 1.00f), threshold = 1.00f } };

        Generate();
    }
    

    public void Generate()
    {
        for (int i = 0; i < terrains.Length; i++)
            terrains[i].tile = CreateColorTile(terrains[i].color);


        float[,] map = NoiseMap.Generate(noise.width, noise.height, noise);


        tilemap.ClearAllTiles();

        for (int y = 0; y < noise.height; y++)
        {
            for (int x = 0; x < noise.width; x++)
            {
                string tileName = GetTile(map[x, y]);
                Tile tile = null;

                for (int z = 0; z < blockPrefabStorage.tiles.Count; z++)
                {
                    if (blockPrefabStorage.tiles[z].name == tileName)
                    {
                        tile = blockPrefabStorage.tiles[z];
                    }
                }
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
        print(noise.width);
        tilemapInfo.tilemap = tilemap;
        tilemapInfo.Width = noise.width;
        tilemapInfo.Height = noise.height;

        mapGenerated.InvokeTilemapGenerated();
    }


    private static Tile CreateColorTile(Color color)
    {
        Texture2D tex = new Texture2D(16, 16)
        {
            filterMode = FilterMode.Point,
            wrapMode = TextureWrapMode.Clamp
        };

        Color[] pixels = new Color[16 * 16];
        for (int i = 0; i < pixels.Length; i++)
            pixels[i] = color;

        tex.SetPixels(pixels);
        tex.Apply();

        Sprite sprite = Sprite.Create(
            tex,
            new Rect(0, 0, 16, 16),
            new Vector2(0.5f, 0.5f),
            pixelsPerUnit: 16f  // 1 tile = 1 unité Unity
        );

        Tile tile = ScriptableObject.CreateInstance<Tile>();
        tile.sprite = sprite;
        tile.color = Color.white; // couleur déjà dans le sprite
        return tile;
    }

    private string GetTile(float height)
    {
        foreach (var terrain in terrains)
            if (height <= terrain.threshold)
                return terrain.name;

        return terrains[terrains.Length - 1].name;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            print("generate");
            Generate();
        }
    }


}


// creer une nouvelle classe 

