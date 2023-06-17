using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPainter : MonoBehaviour
{
    public Color paintColor = Color.white;
    public float brushSize = 1f;

    private Camera mainCamera;
    private Renderer wallRenderer;
    private Texture2D texture;
    private float pixelAmount;
    private List<Vector2> paintedCoords = new List<Vector2>();

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0)) // Sol mouse düğmesi tıklandığında
        {
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit)) // Fare işaretçisi duvara temas ettiğinde
            {
                if (hit.collider.CompareTag("Wall")) // Duvar etiketine sahip nesneyle temas edildiğinde
                {
                    wallRenderer = hit.collider.GetComponent<Renderer>();

                    if (wallRenderer != null)
                    {
                        texture = GetTexture();
                        //pixelAmount = texture.width * texture.height;
                        Vector2 pixelUV = hit.textureCoord;
                        PaintTexture(pixelUV, brushSize, paintColor);
                        ApplyTexture();
                    }
                }
            }
        }
    }

    private Texture2D GetTexture()
    {
        if (wallRenderer.material.mainTexture is Texture2D)
        {
            return (Texture2D)wallRenderer.material.mainTexture;
        }
        else
        {
            Debug.LogError("The object's material does not have a valid Texture2D assigned.");
            return null;
        }
    }

    private void PaintTexture(Vector2 uv, float brushSize, Color color)
    {
        int textureWidth = texture.width;
        int textureHeight = texture.height;

        int brushSizeInt = Mathf.RoundToInt(brushSize);

        int startX = Mathf.RoundToInt(uv.x * textureWidth - brushSize / 2);
        int startY = Mathf.RoundToInt(uv.y * textureHeight - brushSize / 2);

        for (int x = startX; x < startX + brushSizeInt; x++)
        {
            for (int y = startY; y < startY + brushSizeInt; y++)
            {
                if (x >= 0 && x < textureWidth && y >= 0 && y < textureHeight)
                {
                    texture.SetPixel(x, y, color);
                    // if (!paintedCoords.Contains(new Vector2(x, y)))
                    // {
                    //     paintedCoords.Add(new Vector2(x, y));
                    //     //Debug.Log(calculatePercantage());
                    // }
                }
            }
        }
    }

    private float calculatePercantage()
    {
        return (pixelAmount * paintedCoords.Count) / 100;
    }

    private void ApplyTexture()
    {
        texture.Apply();
        wallRenderer.material.mainTexture = texture;
    }
}
