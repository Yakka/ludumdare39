using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dick : MonoBehaviour {
    
    public Section sectionModel = null;

	void Start () {
		if(sectionModel != null) {
            Instantiate(sectionModel);
        } else {
            Debug.Log("Error: no section model, cannot instantiate the dick.");
        }
	}
	
	void Update () {
		
	}
}
