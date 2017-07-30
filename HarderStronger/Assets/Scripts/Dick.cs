using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dick : MonoBehaviour {

    public Section sectionModel = null;
    public const int amountOfSections = 10;
    public const float gapBetweenTwoSections = 0.7f;

    public void Start() {
        Transform parent = transform;

        for(int i = 0; i < amountOfSections; i++) {
            Section section = Instantiate(sectionModel);
            section.transform.parent = parent;
            section.name = "Section (" + i + ")";
            section.transform.Translate(Vector3.left * gapBetweenTwoSections * i);
            if(i == amountOfSections - 1) {
                section.isHead = true;
            } else {
                parent = section.transform;
            }
        }
    }
}

