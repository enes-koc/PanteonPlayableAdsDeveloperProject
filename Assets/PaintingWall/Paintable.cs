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

    public float paintPercentage = 0;

    int maskTextureID = Shader.PropertyToID("_MaskTexture");

    public RenderTexture getMask() => maskRenderTexture;
    public RenderTexture getUVIslands() => uvIslandsRenderTexture;
    public RenderTexture getExtend() => extendIslandsRenderTexture;
    public RenderTexture getSupport() => supportTexture;
    public Renderer getRenderer() => rend;

    void Start()
    {
        maskRenderTexture = new RenderTexture(TEXTURE_SIZE, TEXTURE_SIZE, 0);
        maskRenderTexture.filterMode = FilterMode.Bilinear;

        extendIslandsRenderTexture = new RenderTexture(TEXTURE_SIZE, TEXTURE_SIZE, 0);
        extendIslandsRenderTexture.filterMode = FilterMode.Bilinear;

        uvIslandsRenderTexture = new RenderTexture(TEXTURE_SIZE, TEXTURE_SIZE, 0);
        uvIslandsRenderTexture.filterMode = FilterMode.Bilinear;

        supportTexture = new RenderTexture(TEXTURE_SIZE, TEXTURE_SIZE, 0);
        supportTexture.filterMode = FilterMode.Bilinear;

        rend = GetComponent<Renderer>();
        rend.material.SetTexture(maskTextureID, extendIslandsRenderTexture);

        PaintManager.Instance.initTextures(this);

        StartPaintedPercentageCalculation();
    }

    void OnDisable()
    {
        maskRenderTexture.Release();
        uvIslandsRenderTexture.Release();
        extendIslandsRenderTexture.Release();
        supportTexture.Release();
    }


    public void GetPaintedPercentage()
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

        paintPercentage = paintedPercentage;
    }

    public void StartPaintedPercentageCalculation()
    {
        InvokeRepeating("GetPaintedPercentage", 0, 0.5f);
    }

    public void StopPaintedPercentageCalculation()
    {
        CancelInvoke("GetPaintedPercentage");
    }

}