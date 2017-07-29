using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour {

    static private GameplayManager instance = null;

    public Dick dick = null;
    public int power = 100;

    // Use this for initialization
    void Start() {
        instance = this;
        if (dick == null) {
            Debug.Log("Error: no dick found.");
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
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

    static public GameplayManager GetInstance() {
        return instance;
    }
}
