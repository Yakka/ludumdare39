﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour {
    
    public const float angleBoost = 25f;
    public const int amountOfImpactedSections = 5;
    public float angleSpeed = 100f;
    public const float maxAngle = 80f;

    public float lerpDuration = 0.5f;
    private float lerpCounter = 0f;
    private bool isRotating = false;

    public bool isHead = false;
    public Sprite headSprite = null;

    private float targetedAngle;
    private Quaternion targetedQuaternion;
    private float initialAngle = 0f;

    private bool hasCost;

    public Sprite spriteWhite;
    public Sprite spritePink;
    public GameObject targetUI;

    // Use this for initialization
    void Start () {
        if (isHead) {
            GetComponent<SpriteRenderer>().sprite = headSprite;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(lerpCounter <= lerpDuration && isRotating) {
            //transform.Rotate(Vector3.back * angleSpeed * Time.deltaTime);
            lerpCounter += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetedQuaternion, lerpCounter / lerpDuration);
        } else {
            isRotating = false;
        }


        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null && hit.collider.name == name) {
            targetUI.SetActive(true);
            
            if (Input.GetMouseButtonDown(0)) {
                if(GameplayManager.GetInstance().power >= GameplayManager.GetInstance().powerCost) {
                    BoostAngle(angleBoost, 0);
                    if(hasCost) {
                        GameplayManager.GetInstance().power -= GameplayManager.GetInstance().powerCost;
                    }
                }
                
            } else if (Input.GetMouseButtonDown(1) && hit.collider) {
                if (hit.collider.name == name) {
                    BoostAngle(-angleBoost, 0);

                    if (GameplayManager.GetInstance().power < GameplayManager.GetInstance().powerMax) {
                        if (hasCost) {
                            GameplayManager.GetInstance().power = Mathf.Min(GameplayManager.GetInstance().powerMax,
                                GameplayManager.GetInstance().power + GameplayManager.GetInstance().powerRefund);
                        }
                    }
                }
            }
        } else {
            targetUI.SetActive(false);
        }
    }

    public void BoostAngle(float _angleBoost, int _index) {
        isRotating = true;
        lerpCounter = 0f;
        if(_angleBoost > 0f) {
            targetedAngle = _angleBoost * Mathf.Cos((0f + ((float)_index + 1f) / (float)amountOfImpactedSections) * Mathf.PI);
        } else {
            targetedAngle = _angleBoost;
        }

        targetedAngle += initialAngle;
        targetedAngle = Mathf.Clamp(targetedAngle % 360, -maxAngle, maxAngle);
        hasCost = targetedAngle != initialAngle;
        initialAngle = targetedAngle;

        targetedQuaternion = Quaternion.AngleAxis(targetedAngle, Vector3.back);

        Section[] sectionChild = GetComponentsInChildren<Section>();
        
        if(sectionChild.Length > 1 && _index + 1 < amountOfImpactedSections) {
            sectionChild[1].BoostAngle(_angleBoost, _index + 1);
        }
    }
}
