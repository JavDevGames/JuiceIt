using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HumanPaddle : MonoBehaviour
{
    public Bounds bounds;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
    } 

    // Update is called once per frame
    void Update()
    {
        var displacement = speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.W) && (transform.position.y + displacement) < bounds.upperBound)
            transform.Translate(0, speed * Time.deltaTime, 0);
        else if(Input.GetKey(KeyCode.S) && (transform.position.y - displacement) > bounds.lowerBound)
            transform.Translate(0, -speed * Time.deltaTime, 0);
    }
} 
