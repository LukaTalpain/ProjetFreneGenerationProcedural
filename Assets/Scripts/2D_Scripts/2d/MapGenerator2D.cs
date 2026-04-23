using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MapGenerator2D : MonoBehaviour
{
    [Header("Bruit")]
    public NoiseSettings noise;

    [Header("Biomes — ordonner du plus bas au plus haut")]
    public TerrainType[] terrains = new TerrainType[]
    {
        new TerrainType { name = "Eau profonde", color = new Color(0.10f, 0.30f, 0.70f), threshold = 0.25f },
        new TerrainType { name = "Eau peu profonde", color = new Color(0.20f, 0.50f, 0.85f), threshold = 0.35f },
        new TerrainType { name = "Plage",         color = new Color(0.90f, 0.85f, 0.60f), threshold = 0.42f },
        new TerrainType { name = "Herbe",          color = new Color(0.30f, 0.70f, 0.30f), threshold = 0.60f },
        new TerrainType { name = "Forêt",          color = new Color(0.10f, 0.45f, 0.15f), threshold = 0.72f },
        new TerrainType { name = "Roche",          color = new Color(0.55f, 0.50f, 0.45f), threshold = 0.85f },
        new TerrainType { name = "Neige",          color = new Color(0.95f, 0.95f, 1.00f), threshold = 1.00f },
    };

    private SpriteRenderer _sr;

    private void Awake() => _sr = GetComponent<SpriteRenderer>();
    private void Start() => Generate();

    public void Generate()
    {
        float[,] map = NoiseMap.Generate(noise.width, noise.height, noise);
        _sr.sprite = BuildSprite(map);
    }

    private Sprite BuildSprite(float[,] map)
    {
        int w = map.GetLength(0);
        int h = map.GetLength(1);

        Texture2D tex = new Texture2D(w, h)
        {
            filterMode = FilterMode.Point,
            wrapMode = TextureWrapMode.Clamp
        };

        Color[] pixels = new Color[w * h];

        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                float height = map[x, y];
                pixels[y * w + x] = GetBiomeColor(height);
            }
        }

        tex.SetPixels(pixels);
        tex.Apply();

        return Sprite.Create(
            tex,
            new Rect(0, 0, w, h),
            new Vector2(0.5f, 0.5f),
            pixelsPerUnit: 1f
        );
    }

    private Color GetBiomeColor(float height)
    {
        foreach (var terrain in terrains)
            if (height <= terrain.threshold)
                return terrain.color;

        return terrains[terrains.Length - 1].color;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_sr == null) _sr = GetComponent<SpriteRenderer>();
        if (Application.isPlaying) Generate();
    }
#endif
}
