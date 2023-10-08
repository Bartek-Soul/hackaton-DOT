using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class rysowanieOrbity : MonoBehaviour
{
    public float ThetaScale = 0.01f;
    public float radius;
    public GameObject sun;
    float width = 0.1f;
    private int Size;
    private LineRenderer LineDrawer;
    private float Theta = 0f;
    public Gradient color;
    public Material material;
    void Start()
    {
        LineDrawer = GetComponent<LineRenderer>();
        LineDrawer.colorGradient=color;
        LineDrawer.endWidth=width;
        LineDrawer.startWidth=width;
        LineDrawer.material = material;
    }

    void Update()
    {
        Theta = 0f;
        Size = (int)((1f / ThetaScale) + 1f);
        LineDrawer.SetVertexCount(Size);
        for (int i = 0; i < Size; i++)
        {
            Theta += (2.0f * Mathf.PI * ThetaScale);
            float x = radius * Mathf.Cos(Theta);
            float y = radius * Mathf.Sin(Theta);
            LineDrawer.SetPosition(i, new Vector3(x, 0, y));
            
        }
    }

}
