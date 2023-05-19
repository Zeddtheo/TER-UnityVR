using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurveController : MonoBehaviour
{
    public int discretization = 10;

    public List<Transform> controlPoints;

    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        controlPoints = new List<Transform>();
        foreach (Transform child in allChildren)
        {
            if (child.CompareTag("Controls Points"))
            {
                controlPoints.Add(child);
            }
        }

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = discretization + 1; 

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i <= discretization; i++)
        {
            float t = i / (float)discretization;
            lineRenderer.SetPosition(i, CalculateBezierPoint(t));
        }
    }

    private Vector3 CalculateBezierPoint(float t)
    {
        int n = controlPoints.Count - 1;
        Vector3[] points = new Vector3[controlPoints.Count];
        for (int i = 0; i < controlPoints.Count; i++)
        {
            points[i] = controlPoints[i].position;
        }

        Vector3 point = Vector3.zero;
        for (int i = 0; i <= n; i++)
        {
            float Beta = BinomialCoefficient(n, i) * Mathf.Pow(1 - t, n - i) * Mathf.Pow(t, i);
            point += Beta * points[i];
        }

        return point;
    }

    private int BinomialCoefficient(int n, int k)
    {
        int res = 1;
        for (int i = 1; i <= k; i++)
        {
            res *= (n - i + 1);
            res /= i;
        }
        return res;
    }

}
