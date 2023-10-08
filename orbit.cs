using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbit : MonoBehaviour
{
    public GameObject sun;
    GameObject planet;
    public float orbitTime;
    public float daysToRotate;
    float offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = Random.RandomRange(0, 360);
        planet = gameObject;
        transform.RotateAround(sun.transform.position, Vector3.up, offset);
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(sun.transform.position, Vector3.up, 360f/orbitTime * Time.deltaTime);
        transform.Rotate(new Vector3(0f, -360f/daysToRotate*Time.deltaTime, 0f));
    }
}
