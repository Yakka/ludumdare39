using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour {

    static private GameplayManager instance = null;

    public Dick dick = null;
    public int power;
    public const int powerCost = 10;
    public const int powerRefund = 5;
    public const int powerMax = 100;

    // Use this for initialization
    void Start() {
        instance = this;
        power = powerMax;
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
            if(power >= powerCost) {
                dick.EnlargeSelectedSection();
                power -= 10;
            }
        }
        if (Input.GetKeyUp(KeyCode.DownArrow)) {
            dick.ShrinkSelectedSection();
            power = Mathf.Min(power + powerRefund, powerMax);
        }
    }

    static public GameplayManager GetInstance() {
        return instance;
    }
}
