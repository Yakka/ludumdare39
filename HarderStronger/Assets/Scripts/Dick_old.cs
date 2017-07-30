using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dick_old : MonoBehaviour {

    // TWO BUGS: plafond infranchissable et impossible de rabaisser

    public GameObject head;

    public Vector3 maxAngle = new Vector3(-1f, 1f, 0f);
    public const int amountOfSections = 10;
    public float gapBetweenTwoSections = 0.1f; // Doesn't work for some reasons

    public const float sizeUp = 0.01f;
    public const float angleUp = 5f;
    public const float initialHeight = 0.7f;
    
    private int selectedSectionID = 0;

    private List<Vector3> verticesList = new List<Vector3>();
    private List<int> trianglesList = new List<int>();
    private List<Vector3> normalsList = new List<Vector3>();
    private List<Vector2> uvsList = new List<Vector2>();
    private List<Vector3> anglesList = new List<Vector3>(); //Angles in radian
    private List<float> sizesList = new List<float>();
    private List<Vector3> shiftUp = new List<Vector3>();
    private Mesh mesh = null;

    void Start () {
        mesh = new Mesh();

        GetComponent<MeshFilter>().mesh = mesh;

        for (int i = 0; i < amountOfSections; i++) {
            // Pivots
            anglesList.Add(Vector3.left * gapBetweenTwoSections);
            sizesList.Add(initialHeight);
            shiftUp.Add(Vector3.zero);

            // Upper vertice
            verticesList.Add(Vector3.zero); 
            normalsList.Add(Vector3.back);

            // Lower vertice
            verticesList.Add(Vector3.zero);
            normalsList.Add(Vector3.back);

            // UVs
            uvsList.Add(Vector3.zero);
            uvsList.Add(Vector3.back);

            UpdateVerticesPair(i);

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
            anglesList[i] += deltaAngle * Mathf.PI / 180f * Mathf.Max(3 + selectedSectionID - i, 0);
            // Security to keep low angles:
            if(Vector3.Angle(Vector3.left, anglesList[i]) > Vector3.Angle(Vector3.left, maxAngle)) {
                if(anglesList[i].y > 0f) {
                    anglesList[i] = maxAngle;
                } else {
                    anglesList[i] = new Vector3(anglesList[i].x, -anglesList[i].y, 0f);
                }
            }
            // Impacting the next sections:
            if (i > selectedSectionID) {
                float angle = Vector3.Angle(Vector3.left, anglesList[i - 1]) * _factor;
                shiftUp[i] = shiftUp[i - 1] + Vector3.up * Mathf.Sin(angle * Mathf.PI / 180f) * gapBetweenTwoSections;
            }
            UpdateVerticesPair(i);

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
        Vector3 previousAngle = Vector3.left;
        if(_i > 0) {
            previousAngle = anglesList[_i - 1];
        }

        verticesList[_i * 2] = gapBetweenTwoSections * previousAngle.normalized + Vector3.Cross(Vector3.back, anglesList[_i]).normalized * sizesList[_i] / 2f + Vector3.left * _i + shiftUp[_i];
        verticesList[_i * 2 + 1] = gapBetweenTwoSections * previousAngle.normalized - Vector3.Cross(Vector3.back, anglesList[_i]).normalized * sizesList[_i] / 2f + Vector3.left * _i + shiftUp[_i];


        uvsList[_i * 2] = new Vector2(verticesList[_i * 2].x, verticesList[_i * 2].y);
        uvsList[_i * 2 + 1] = new Vector2(verticesList[_i * 2 + 1].x, verticesList[_i * 2 + 1].y);
    }
}
