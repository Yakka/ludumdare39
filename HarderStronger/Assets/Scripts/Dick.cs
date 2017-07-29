using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dick : MonoBehaviour {
    
    public Section sectionModel = null;
    public int amountOfSections = 5;
    public int gapBetweenTwoSections = 10;

    private List<Section> sectionsList = new List<Section>();

	void Start () {
		if(sectionModel != null) {
            for(int i = 0; i < amountOfSections; i++) {
                Section section = Instantiate(sectionModel);
                section.transform.parent = this.transform;
                section.transform.Translate(Vector3.left * i * gapBetweenTwoSections);
                section.name = "Section (" + i + ")";

                sectionsList.Add(section);
            }
        } else {
            Debug.Log("Error: no section model, cannot instantiate the dick.");
        }
	}
	
	void Update () {
        Vector3 previousPosition = Vector3.zero;
		foreach(Section section in sectionsList) {
            Debug.DrawLine(previousPosition, section.transform.position);
            previousPosition = section.transform.position;
        }
	}
}
