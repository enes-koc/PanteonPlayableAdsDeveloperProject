using UnityEngine;

public class MousePainter : MonoBehaviour
{
    Camera cam;
    [Space]
    [SerializeField] bool isPaintable = false;
    public bool mouseSingleClick;
    [Space]
    public Color paintColor;

    public float radius = 1;
    public float strength = 1;
    public float hardness = 1;

    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (isPaintable)
        {
            bool click;
            click = mouseSingleClick ? Input.GetMouseButtonDown(0) : Input.GetMouseButton(0);

            if (click)
            {
                Vector3 position = Input.mousePosition;
                Ray ray = cam.ScreenPointToRay(position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    Debug.DrawRay(ray.origin, hit.point - ray.origin, Color.red);
                    transform.position = hit.point;
                    Paintable p = hit.collider.GetComponent<Paintable>();
                    if (p != null)
                    {
                        PaintManager.Instance.paint(p, hit.point, radius, hardness, strength, paintColor);
                    }
                }
            }

        }
    }

    public void setColorBlue(){
        paintColor=Color.blue;
    }

    public void setColorYellow(){
        paintColor=Color.yellow;
    }

    public void setColorRed(){
        paintColor=Color.red;
    }

    public void setBrushSize(float size){
        radius=size;
    }

}
