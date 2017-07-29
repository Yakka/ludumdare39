﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dick : MonoBehaviour {
    
    public int amountOfSections = 5;
    public float gapBetweenTwoSections = 1f;

    public const float sizeUp = 0.1f;
    public const float initialHeight = 2f;
    
    private int selectedSectionID = 0;

    private List<Vector3> verticesList = new List<Vector3>();
    private List<int> trianglesList = new List<int>();
    private List<Vector3> normalsList = new List<Vector3>();
    private List<Vector2> uvsList = new List<Vector2>();
    private Mesh mesh = null;

    void Start () {
            /*for (int i = 0; i < amountOfSections; i++) {
                // Setting the new game object
                Section section = Instantiate(sectionModel);
                section.transform.parent = this.transform;
                section.name = "Section (" + i + ")";
                // Setting its coordinates
                if (i > 0) {
                    section.eulerAngle = Vector3.back * 30;
                    section.pivot = sectionsList[i - 1].pivot 
                        + gapBetweenTwoSections * Mathf.Cos(sectionsList[i - 1].eulerAngle.magnitude) * Vector3.left
                        - gapBetweenTwoSections * Mathf.Sin(sectionsList[i - 1].eulerAngle.magnitude) * Vector3.up;
                    section.topVertice = section.pivot + Vector3.Cross(Vector3.forward, section.pivot).normalized * section.height / 2f;
                    section.bottomVertice = section.pivot - Vector3.Cross(Vector3.forward, section.pivot).normalized * section.height / 2f;
                }
                // Adding to the list
                // sectionsList.Add(section);
            }*/
            // Generating the mesh
            mesh = new Mesh();

            GetComponent<MeshFilter>().mesh = mesh;

            for (int i = 0; i < amountOfSections; i++) {
                // Upper vertice
                verticesList.Add(Vector3.up * initialHeight  / 2f + Vector3.left * i);
                uvsList.Add(new Vector2(amountOfSections - i, initialHeight / 2f));
                normalsList.Add(Vector3.back);

                // Lower vertice
                verticesList.Add(Vector3.down * initialHeight / 2f + Vector3.left * i);
                uvsList.Add(new Vector2(amountOfSections - i, -initialHeight / 2f));
                normalsList.Add(Vector3.back);

                if(i > 0) {
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
        verticesList[selectedSectionID * 2] += Vector3.up * sizeUp;
        uvsList[selectedSectionID * 2] += Vector2.up * sizeUp;

        verticesList[selectedSectionID * 2 + 1] += Vector3.down * sizeUp;
        uvsList[selectedSectionID * 2 + 1] += Vector2.down * sizeUp;

        UpdateDick();
    }

    public void ShrinkSelectedSection() {
        verticesList[selectedSectionID * 2] -= Vector3.up * sizeUp;
        verticesList[selectedSectionID * 2 + 1] -= Vector3.down * sizeUp;

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
}
