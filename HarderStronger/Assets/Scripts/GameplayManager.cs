using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour {

    public Dick dick = null;

	// Use this for initialization
	void Start () {
		if(dick == null) {
            Debug.Log("Error: no dick found.");
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.LeftArrow)) {
            dick.SelectOnLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            dick.SelectOnRight();
        }
        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            dick.EnlargeSelectedSection();
        }
        if (Input.GetKeyUp(KeyCode.DownArrow)) {
            dick.ShrinkSelectedSection();
        }
    }
}
