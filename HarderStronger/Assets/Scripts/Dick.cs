using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dick : MonoBehaviour {

    public Vector3 maxAngleFactor = new Vector3(1f, 1f, 0);
    public const int amountOfSections = 5;
    public float gapBetweenTwoSections = 0.01f;

    public const float sizeUp = 0.1f;
    public const float initialHeight = 2f;
    
    private int selectedSectionID = 0;

    private List<Vector3> verticesList = new List<Vector3>();
    private List<int> trianglesList = new List<int>();
    private List<Vector3> normalsList = new List<Vector3>();
    private List<Vector2> uvsList = new List<Vector2>();
    private List<Vector3> anglesList = new List<Vector3>();
    private List<float> sizesList = new List<float>();
    private Mesh mesh = null;

    void Start () {
        mesh = new Mesh();

        GetComponent<MeshFilter>().mesh = mesh;

        for (int i = 0; i < amountOfSections; i++) {
            // Pivots
            anglesList.Add(Vector3.left);
            NormalizeAngles(i);
            sizesList.Add(initialHeight);

            // Upper vertice
            verticesList.Add(Vector3.zero); 
            normalsList.Add(Vector3.back);

            // Lower vertice
            verticesList.Add(Vector3.zero);
            normalsList.Add(Vector3.back);

            UpdateVerticesPair(i);

            // UVs
            uvsList.Add(new Vector2(verticesList[i * 2].x, verticesList[i * 2].y));
            uvsList.Add(new Vector2(verticesList[i * 2 + 1].x, verticesList[i * 2 + 1].y));

            if (i > 0) {
                // Upper triangle
                trianglesList.Add((i * 2) - 2);
                trianglesList.Add((i * 2) - 1);
                trianglesList.Add(i * 2);

                // Lower triangle
                trianglesList.Add((i * 2) - 1);
                trianglesList.Add((i * 2) + 1);
                trianglesList.Add(i * 2);
            }
        }

        // Building the mesh
        UpdateDick();
	}

    public void Update() {
    }

    public void SelectOnLeft() {
        if(selectedSectionID + 1 < amountOfSections) {
            selectedSectionID++;
        }
    }

    public void SelectOnRight() {
        if (selectedSectionID > 0) {
            selectedSectionID--;
        }
    }

    public void EnlargeSelectedSection() {
        anglesList[selectedSectionID] = anglesList[selectedSectionID] + Vector3.up * 0.1f;
        NormalizeAngles(selectedSectionID);
        sizesList[selectedSectionID] += sizeUp;

        for (int i = selectedSectionID; i < anglesList.Count; i++) {
            
            anglesList[selectedSectionID] = Vector3.Min(anglesList[selectedSectionID], maxAngleFactor * i / anglesList.Count); // It's easier and easier to have a large angle
            NormalizeAngles(i);
            UpdateVerticesPair(i);

            if (i > selectedSectionID) {
                anglesList[i] += Vector3.up * Mathf.Abs(anglesList[i - 1].y);
                NormalizeAngles(i);
                verticesList[i * 2] += Vector3.up * Mathf.Abs(anglesList[i - 1].y) * sizesList[i];
                verticesList[i * 2 + 1] += Vector3.up * Mathf.Abs(anglesList[i - 1].y) * sizesList[i];
            }

            uvsList[i * 2] = new Vector2(verticesList[i * 2].x, verticesList[i * 2].y);

            uvsList[i * 2 + 1] = new Vector2(verticesList[i * 2 + 1].x, verticesList[i * 2 + 1].y);
        }
        UpdateDick();
    }

    public void ShrinkSelectedSection() {
        anglesList[selectedSectionID] -= Vector3.up * 0.1f;
        NormalizeAngles(selectedSectionID);
        sizesList[selectedSectionID] -= sizeUp;

        for (int i = selectedSectionID; i < anglesList.Count; i++) {

            UpdateVerticesPair(i);

            if (i > selectedSectionID) {
                anglesList[i] -= Vector3.up * Mathf.Abs(anglesList[i - 1].y);
                NormalizeAngles(i);
                //anglesList[i] = anglesList[i].normalized;
                verticesList[i * 2] -= Vector3.up * Mathf.Abs(anglesList[i - 1].y);
                verticesList[i * 2 + 1] -= Vector3.up * Mathf.Abs(anglesList[i - 1].y);
            }

            uvsList[i * 2] = new Vector2(verticesList[i * 2].x, verticesList[i * 2].y);

            uvsList[i * 2 + 1] = new Vector2(verticesList[i * 2 + 1].x, verticesList[i * 2 + 1].y);
        }
        UpdateDick();
    }

    public void UpdateSectionSelectionFeedback(int _id) {
    }

    public void UpdateDick() {
        mesh.Clear();

        mesh.vertices = verticesList.ToArray();
        mesh.triangles = trianglesList.ToArray();

        mesh.normals = normalsList.ToArray();
        mesh.uv = uvsList.ToArray();
    }

    public void UpdateVerticesPair(int _i) {
        verticesList[_i * 2] = (Vector3.Cross(Vector3.back, anglesList[_i]).normalized * sizesList[_i] / 2f + Vector3.left * _i) * gapBetweenTwoSections;
        verticesList[_i * 2 + 1] = (Vector3.Cross(Vector3.forward, anglesList[_i]).normalized * sizesList[_i] / 2f + Vector3.left * _i) * gapBetweenTwoSections;
    }

    public void NormalizeAngles(int _i) {
        anglesList[_i] = anglesList[_i].normalized;
    }
}
