using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dick : MonoBehaviour {

    // TWO BUGS: plafond infranchissable et impossible de rabaisser

    public GameObject head;

    public Vector3 maxAngle = new Vector3(-1f, 1f, 0f);
    public const int amountOfSections = 10;
    public float gapBetweenTwoSections = 0.1f; // Doesn't work for some reasons

    public const float sizeUp = 0.01f;
    public const float angleUp = 5f;
    public const float initialHeight = 0.7f;

    private int selectedSectionID = 0;

    void Start() {
        
    }

    public void Update() {
    }

    public void SelectOnLeft() {
        if (selectedSectionID + 1 < amountOfSections) {
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
        
    }
}

