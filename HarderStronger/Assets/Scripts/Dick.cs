using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dick : MonoBehaviour {

    // TWO BUGS: plafond infranchissable et impossible de rabaisser

    public GameObject head;

    public Vector3 maxAngle = new Vector3(1f, 0.1f, 0);
    public const int amountOfSections = 10;
    public float gapBetweenTwoSections = 0.1f; // Doesn't work for some reasons

    public const float sizeUp = 0.1f;
    public const float angleUp = 0.05f;
    public const float initialHeight = 0.7f;
    
    private int selectedSectionID = 0;

    private List<Vector3> verticesList = new List<Vector3>();
    private List<int> trianglesList = new List<int>();
    private List<Vector3> normalsList = new List<Vector3>();
    private List<Vector2> uvsList = new List<Vector2>();
    private List<Vector3> anglesList = new List<Vector3>();
    private List<float> sizesList = new List<float>();
    private List<Vector3> shiftUp = new List<Vector3>();
    private Mesh mesh = null;

    void Start () {
        mesh = new Mesh();

        GetComponent<MeshFilter>().mesh = mesh;

        for (int i = 0; i < amountOfSections; i++) {
            // Pivots
            anglesList.Add(Vector3.left * gapBetweenTwoSections);
            NormalizeAngles(i);
            sizesList.Add(initialHeight);
            shiftUp.Add(Vector3.zero);

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
        DeformSelectedSection(1);
    }

    public void ShrinkSelectedSection() {
        DeformSelectedSection(-1);
    }

    public void DeformSelectedSection(int _factor) {
        Vector3 deltaAngle = Vector3.up * angleUp * _factor;
        sizesList[selectedSectionID] += sizeUp * _factor;

        for (int i = selectedSectionID; i < anglesList.Count; i++) {
            anglesList[i] += deltaAngle * Mathf.PI / 180f;
            if (i > selectedSectionID) {
                float angle = Vector3.Angle(anglesList[i - 1], Vector3.left);
                shiftUp[i] = shiftUp[i - 1] + Vector3.up * Mathf.Sin(angle);
            }
            NormalizeAngles(i);
            UpdateVerticesPair(i);

            if (i > selectedSectionID) {
                /*anglesList[i] += Vector3.up * Mathf.Abs(anglesList[i - 1].y) * _factor;
                NormalizeAngles(i);
                verticesList[i * 2] += Vector3.up * Mathf.Abs(anglesList[i - 1].y) * sizesList[i] * _factor;
                verticesList[i * 2 + 1] += Vector3.up * Mathf.Abs(anglesList[i - 1].y) * sizesList[i] * _factor;*/
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

        head.transform.position = (verticesList[verticesList.Count - 1] + verticesList[verticesList.Count - 2]) / 2;
    }

    public void UpdateVerticesPair(int _i) {
        // I guess this is where it fucks up
        //verticesList[_i * 2] = (Vector3.Cross(Vector3.back, anglesList[_i]).normalized * sizesList[_i] / 2f + Vector3.left * _i) * gapBetweenTwoSections;
        //verticesList[_i * 2 + 1] = (Vector3.Cross(Vector3.forward, anglesList[_i]).normalized * sizesList[_i] / 2f + Vector3.left * _i) * gapBetweenTwoSections;
        Vector3 previousAngle = Vector3.left;
        if(_i > 0) {
            previousAngle = anglesList[_i - 1];
        }

        verticesList[_i * 2] = gapBetweenTwoSections * previousAngle.normalized + Vector3.Cross(Vector3.back, anglesList[_i]).normalized * sizesList[_i] / 2f + Vector3.left * _i + shiftUp[_i];
        verticesList[_i * 2 + 1] = gapBetweenTwoSections * previousAngle.normalized - Vector3.Cross(Vector3.back, anglesList[_i]).normalized * sizesList[_i] / 2f + Vector3.left * _i + shiftUp[_i];
    }

    public void NormalizeAngles(int _i) {
        //anglesList[_i] = anglesList[_i].normalized;
    }
}
