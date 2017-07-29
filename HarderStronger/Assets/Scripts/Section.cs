using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour {

    public Vector3 topVertice, bottomVertice, eulerAngle = Vector3.zero;
    private Vector3 initialScale;

	void Start () {
        initialScale = transform.localScale;
	}
	
	void Update () {
        transform.position = (topVertice + bottomVertice) / 2f;
        transform.localScale = initialScale + topVertice - bottomVertice;
        transform.eulerAngles = eulerAngle;
	}
}
