using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour {
    
    public const float maxAngle = 45f;
    public const float angleBoost = 5f;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if(Input.GetMouseButtonDown(0) && hit.collider != null) {
            if (hit.collider.name == name) {
                BoostAngle(angleBoost);
            }
        }
    }

    public void BoostAngle(float _angleBoost) {        
        transform.Rotate(Vector3.back * _angleBoost);
    }
}
