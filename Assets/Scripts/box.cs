using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour
{
    public Vector3 positionBox = new Vector3(2, 0.5f, -2.4f);
    public Vector3 scaleBox = new Vector3(1, 1, 1);

    public Vector3 changePositionBox = new Vector3(0, 0, 0.01f);
    public Vector3 changeScaleBox = new Vector3(0.0001f, 0.0001f, 0.0001f);

    // Start is called before the first frame update
    void Start()
    {
        transform.position = positionBox;
        transform.localScale = scaleBox;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += changePositionBox;
        transform.localScale -= changeScaleBox;

    }
}
