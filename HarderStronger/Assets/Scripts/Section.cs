using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour {

    public Vector3 topVertice, bottomVertice, eulerAngle, pivot = Vector3.zero;
    private Vector3 initialScale;
    public float height = 1f;

	void Start () {
        initialScale = transform.localScale;
	}
	
	void Update () {
        transform.position = pivot;
        transform.localScale = initialScale + topVertice + bottomVertice;
        transform.eulerAngles = eulerAngle;
	}
}
