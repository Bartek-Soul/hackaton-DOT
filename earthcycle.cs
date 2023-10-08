using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earthcycle : MonoBehaviour
{
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend=GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float offset=360f/365.25f*Time.deltaTime;
        rend.material.SetTextureOffset("_DetailAlbedoMap", new Vector2(0, offset));
    }
}
