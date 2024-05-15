using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T34 : MonoBehaviour
{
    public float rotationInitSpeed = 0;
    public float moveSpeed = 0;
    bool stillRot = true;
    int rotCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stillRot)
        {
            transform.Rotate(new Vector3(0.0f, -1.0f, 0.0f) * Time.deltaTime * rotationInitSpeed);
        }
        else {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
        }
        if (stillRot && rotCount > 1000) stillRot = false;
        else ++rotCount;
    }
}
