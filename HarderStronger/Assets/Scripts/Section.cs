using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour {

    public Vector3 angle = Vector3.left;
    public Vector3 maxAngle = (Vector3.up + Vector3.left).normalized;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if(Input.GetMouseButtonDown(0)) {
            if (hit.collider.name == name) {
                Debug.Log(hit.collider.GetComponent<Section>().name);
            }
        }

        transform.eulerAngles = angle;
    }

    public void BoostAngle(Vector3 _angleBoost) {
        angle += _angleBoost;
        angle.Normalize();
        if(Vector3.Angle(Vector3.left, angle) > Vector3.Angle(Vector3.left, _angleBoost)) {
            if(angle.y > 0) {
                angle = maxAngle;
            } else {
                angle = -maxAngle;
            }
        }
    }
}
