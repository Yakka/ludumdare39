using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour {

    static private GameplayManager instance = null;

    public Dick dick = null;
    public int power;
    public const int powerCost = 10;
    public const int powerRefund = 5;
    public const int powerMax = 1000;

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
        
    }

    static public GameplayManager GetInstance() {
        return instance;
    }
}
