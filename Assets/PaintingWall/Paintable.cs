using UnityEngine;

public class Paintable : MonoBehaviour
{
    const int TEXTURE_SIZE = 1024;

    public float extendsIslandOffset = 1;
    RenderTexture extendIslandsRenderTexture;
    RenderTexture uvIslandsRenderTexture;
    RenderTexture maskRenderTexture;
    RenderTexture supportTexture;

    Renderer rend;

    int maskTextureID = Shader.PropertyToID("_MaskTexture");

    public RenderTexture getMask() => maskRenderTexture;
    public RenderTexture getUVIslands() => uvIslandsRenderTexture;
    public RenderTexture getExtend() => extendIslandsRenderTexture;
    public RenderTexture getSupport() => supportTexture;
    public Renderer getRenderer() => rend;

    private ComputeShader computeShader;

    void Awake()
    {
        string computeShaderPath = "Assets/TestComputeShader.compute";
        computeShader = UnityEditor.AssetDatabase.LoadAssetAtPath<ComputeShader>(computeShaderPath);
        Debug.Log("Compute Shader: " + computeShader);
    }

    void Start()
    {
        maskRenderTexture = new RenderTexture(TEXTURE_SIZE, TEXTURE_SIZE, 0);
        maskRenderTexture.filterMode = FilterMode.Bilinear;

        // maskRenderTexture = new RenderTexture(TEXTURE_SIZE, TEXTURE_SIZE, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
        // maskRenderTexture.filterMode = FilterMode.Bilinear;
        // maskRenderTexture.enableRandomWrite = true; // UAV kullanım bayrağını etkinleştirme

        extendIslandsRenderTexture = new RenderTexture(TEXTURE_SIZE, TEXTURE_SIZE, 0);
        extendIslandsRenderTexture.filterMode = FilterMode.Bilinear;

        uvIslandsRenderTexture = new RenderTexture(TEXTURE_SIZE, TEXTURE_SIZE, 0);
        uvIslandsRenderTexture.filterMode = FilterMode.Bilinear;

        supportTexture = new RenderTexture(TEXTURE_SIZE, TEXTURE_SIZE, 0);
        supportTexture.filterMode = FilterMode.Bilinear;

        rend = GetComponent<Renderer>();
        rend.material.SetTexture(maskTextureID, extendIslandsRenderTexture);

        PaintManager.Instance.initTextures(this);
    }

    void OnDisable()
    {
        maskRenderTexture.Release();
        uvIslandsRenderTexture.Release();
        extendIslandsRenderTexture.Release();
        supportTexture.Release();
    }


    public float GetPaintedPercentage()
    {
        RenderTexture maskTexture = getMask();

        int paintedPixelCount = 0;
        int totalPixelCount = maskTexture.width * maskTexture.height;

        Texture2D tempTexture = new Texture2D(maskTexture.width, maskTexture.height, TextureFormat.RGBA32, false);
        RenderTexture.active = maskTexture;
        tempTexture.ReadPixels(new Rect(0, 0, maskTexture.width, maskTexture.height), 0, 0);
        Color32[] pixels = tempTexture.GetPixels32();

        for (int i = 0; i < pixels.Length; i++)
        {
            if (pixels[i].r > 0 || pixels[i].g > 0 || pixels[i].b > 0 || pixels[i].a > 0)
            {
                paintedPixelCount++;
            }
        }

        float paintedPercentage = (float)paintedPixelCount / (float)totalPixelCount * 100f;

        Destroy(tempTexture);

        return paintedPercentage;
    }
}