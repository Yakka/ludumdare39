using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPowerFeedback : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        GetComponent<Text>().text = "Power: " + GameplayManager.GetInstance().power;
	}
}
