using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour {

    public Vector3 topVertice, bottomVertice, eulerAngle = Vector3.zero;

	void Start () {
		
	}
	
	void Update () {
        transform.position = (topVertice + bottomVertice) / 2f;
        transform.eulerAngles = eulerAngle;
	}
}
