using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGeometry : MonoBehaviour
{
    private List<Vector3> verticiesList = new List<Vector3>();
    private List<int> trianglesList = new List<int>();
    private int[] _triangles;
    private Vector2 _mousePos;

    // Use this for initialization
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RecordClick(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    private void BuildMesh()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        mesh.Clear();
        Vector3[] _verticies = verticiesList.ToArray();
        mesh.vertices = _verticies;


        _triangles = new int[] { 0,1,2};
        Debug.Log("tri list length: " + _triangles.Length);

        /*for (int triCount = 0; triCount < _triangles.Length; triCount += 3)
        {
            trianglesList.Add(triCount);
            trianglesList.Add(triCount + 1);
            trianglesList.Add(triCount + 2);
        }
        _triangles = trianglesList.ToArray();
        Debug.Log("tri list post loop length: " + _triangles.Length);*/
        mesh.triangles = _triangles;
    }

    private void RecordClick(Vector3 mousePos)
    {
        mousePos.z = 0;
        verticiesList.Add (mousePos);
        Debug.Log("clicked at" + mousePos);

        if (verticiesList.Count >= 3)
        {
            BuildMesh();
        }
    }
}
