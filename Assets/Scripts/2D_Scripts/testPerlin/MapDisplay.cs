using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class MapDisplay : MonoBehaviour
{
    [SerializeField] private NoiseSettings settings;
    [SerializeField] private bool autoRefresh = true;

    private Renderer _renderer;

    private void Awake() => _renderer = GetComponent<Renderer>();

    private void Start() => Refresh();

    public void Refresh()
    {
        float[,] map = NoiseMap.Generate(settings.width, settings.height, settings);
        _renderer.sharedMaterial.mainTexture = BuildTexture(map);
    }

    private static Texture2D BuildTexture(float[,] map)
    {
        int w = map.GetLength(0);
        int h = map.GetLength(1);

        Color[] colors = new Color[w * h];
        for (int y = 0; y < h; y++)
            for (int x = 0; x < w; x++)
            {
                float v = map[x, y];
                colors[y * w + x] = new Color(v, v, v); // niveaux de gris
            }

        Texture2D tex = new Texture2D(w, h);
        tex.filterMode = FilterMode.Point;
        tex.SetPixels(colors);
        tex.Apply();
        return tex;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (autoRefresh && _renderer != null)
            Refresh();
    }
#endif
}