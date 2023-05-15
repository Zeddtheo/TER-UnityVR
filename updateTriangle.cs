using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updateTriangle : MonoBehaviour
{
    private Transform[] controlPoints;
    private LineRenderer lineRenderer;

    void Start()
    {
        GameObject[] controlPointObjects = GameObject.FindGameObjectsWithTag("Controls Points");

        controlPoints = new Transform[controlPointObjects.Length];
        for (int i = 0; i < controlPointObjects.Length; i++)
        {
            controlPoints[i] = controlPointObjects[i].transform;
        }

        lineRenderer = GetComponent<LineRenderer>();
    }



    void Update()
    {
        for (int i = 0; i < controlPoints.Length; i++)
        {
            lineRenderer.SetPosition(i, controlPoints[i].position);
        }

    }
}


