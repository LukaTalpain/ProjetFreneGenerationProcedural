using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator2D : MonoBehaviour
{
    [Header("Bruit")]
    public NoiseSettings noise;

    [Header("Tilemap cible")]
    public Tilemap tilemap;

    [Header("Biomes — du plus bas au plus haut")]
    public TerrainType[] terrains = new TerrainType[]
    {
        new TerrainType { name = "Eau profonde",    color = new Color(0.10f, 0.30f, 0.70f), threshold = 0.25f },
        new TerrainType { name = "Eau peu profonde",color = new Color(0.20f, 0.50f, 0.85f), threshold = 0.35f },
        new TerrainType { name = "Plage",           color = new Color(0.90f, 0.85f, 0.60f), threshold = 0.42f },
        new TerrainType { name = "Herbe",           color = new Color(0.30f, 0.70f, 0.30f), threshold = 0.60f },
        new TerrainType { name = "Forêt",           color = new Color(0.10f, 0.45f, 0.15f), threshold = 0.72f },
        new TerrainType { name = "Roche",           color = new Color(0.55f, 0.50f, 0.45f), threshold = 0.85f },
        new TerrainType { name = "Neige",           color = new Color(0.95f, 0.95f, 1.00f), threshold = 1.00f },
    };

    [Header("Event")]
    public RSE_MapGenerated mapGenerated;
    public RSO_TilemapInfo tilemapInfo;

    private void Start() => Generate();

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
                TileBase tile = GetTile(map[x, y]);
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
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

    private TileBase GetTile(float height)
    {
        foreach (var terrain in terrains)
            if (height <= terrain.threshold)
                return terrain.tile;

        return terrains[terrains.Length - 1].tile;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            Generate();
        }
    }


}