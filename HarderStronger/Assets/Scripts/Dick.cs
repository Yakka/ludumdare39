using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dick : MonoBehaviour {
    
    public Section sectionModel = null;
    public int amountOfSections = 5;
    public float gapBetweenTwoSections = 1f;

    private List<Section> sectionsList = new List<Section>();
    private int selectedSectionID = 0;

    private List<Vector3> verticesList = new List<Vector3>();
    private List<int> trianglesList = new List<int>();
    private List<Vector3> normalsList = new List<Vector3>();

    void Start () {
		if(sectionModel != null) {
            Mesh mesh = new Mesh();

            GetComponent<MeshFilter>().mesh = mesh;

            verticesList.Add(Vector3.up);
            verticesList.Add(Vector3.down);
            verticesList.Add(Vector3.left);
            verticesList.Add(Vector3.right);

            mesh.vertices = verticesList.ToArray();

            trianglesList.Add(0);
            trianglesList.Add(1);
            trianglesList.Add(2);

            trianglesList.Add(1);
            trianglesList.Add(0);
            trianglesList.Add(3);

            mesh.triangles = trianglesList.ToArray();

            for(int i = 0; i < verticesList.Count; i++) {
                normalsList.Add(Vector3.back);
            }

            mesh.normals = normalsList.ToArray();

            mesh.uv = new Vector2[]{ Vector2.zero, Vector2.right, Vector2.up, Vector2.right + Vector2.up };

            for (int i = 0; i < amountOfSections; i++) {
                // Setting the new game object
                /*Section section = Instantiate(sectionModel);
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
                sectionsList.Add(section);*/

                
            }
        } else {
            Debug.Log("Error: no section model, cannot instantiate the dick.");
        }
	}

    public void Update() {
        for(int i = 0; i < sectionsList.Count; i++) {
            UpdateSectionSelectionFeedback(i);
        }
    }

    public List<Section> GetSectionsList() {
        return sectionsList;
    }

    public void SelectOnLeft() {
        if(selectedSectionID + 1 < sectionsList.Count) {
            selectedSectionID++;
        }
    }

    public void SelectOnRight() {
        if (selectedSectionID > 0) {
            selectedSectionID--;
        }
    }

    public void EnlargeSelectedSection() {
        sectionsList[selectedSectionID].topVertice += Vector3.up * 0.1f;
        sectionsList[selectedSectionID].bottomVertice += Vector3.down * 0.1f;
    }

    public void ShrinkSelectedSection() {
        sectionsList[selectedSectionID].topVertice -= Vector3.up * 0.1f;
        sectionsList[selectedSectionID].bottomVertice -= Vector3.down * 0.1f;
    }

    public void UpdateSectionSelectionFeedback(int _id) {
        Color color;
        if(_id == selectedSectionID) {
            color = Color.red;
        } else {
            color = Color.white;
        }
        sectionsList[_id].GetComponent<MeshRenderer>().material.color = color;
    }
}
