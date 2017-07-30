using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour {

    public Vector2 angle = Vector2.left;
    public float size;

    public const float initialSize = 1f;
    public const float maxSize = 5f;
    public const Vector2 maxVector = (Vector2.up + Vector2.left).normalize;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
