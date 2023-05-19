using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

public class ProceduralGrid : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    public float cellSize = 1;
    public Vector3 gridOffset;
    public int gridSize;
    public Transform controlPoint;

    Transform[] controlsPoints;


    private void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        MakeContinuousProceduralGrid();
        UpdateMesh();
    }

    private void UpdateMesh()
    {
        mesh.Clear();
        //be careful to send vertices before triangles
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }


    void MakeContinuousProceduralGrid()
    {
        //set array sizes
        vertices = new Vector3[(gridSize+1)*(gridSize+1)];
        controlsPoints = new Transform[(gridSize + 1) * (gridSize + 1)];
        triangles = new int[gridSize * gridSize * 6];

        //set tracker int
        int v = 0;
        int t = 0;

        //set vertex offset
        float vertexOffset = cellSize * 0.5f;

        //create vertex grid
        for (int x = 0; x <= gridSize; x++)
        {
            for (int y = 0; y <= gridSize; y++)
            {
                Vector3 vertexPos = new Vector3((x * cellSize) - vertexOffset, 0 , (y * cellSize) - vertexOffset);
                vertices[v] = vertexPos;
                Transform newControlPoint = Instantiate(controlPoint, vertexPos, Quaternion.identity);
                newControlPoint.transform.parent = transform; // Le transform de la grille est le parent
                controlsPoints[v] = newControlPoint;
                v++;
                Debug.Log("vertex : " + vertexPos);
            }
        }

        //reset vertex tracker
        v = 0;

        //set each cell's triangles
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                triangles[t + 0] = v;
                triangles[t + 1] = triangles[t + 4] = v + 1;
                triangles[t + 2] = triangles[t + 3] = v + (gridSize + 1);
                triangles[t + 5] = v + (gridSize +1) + 1;
                v++;
                t += 6;
            }
            v++;
        }
    }

    private void Update()
    {
        int v = 0;
        for (int x = 0; x <= gridSize; x++)
        {
            for (int y = 0; y <= gridSize; y++)
            {

                vertices[v] = controlsPoints[v].position;
                v++;
            }
        }
        UpdateMesh();

    }





}
