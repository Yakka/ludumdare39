using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour {

    static private GameplayManager instance = null;
    
    public int power;
    public int powerCost;
    public int powerRefund;
    public int powerMax;
    
    public int powerPerBlood;
    public GameObject bloodModel;
    public GameObject bloodBarModel;
    private List<GameObject> bloodsList = new List<GameObject>();

    // Use this for initialization
    void Start() {
        instance = this;
        power = powerMax;
        bloodsList.Add(bloodModel);
        for(int i = 1; i < powerMax / powerPerBlood; i++) {
            GameObject blood = Instantiate(bloodModel);
            blood.name = "Blood (" + i + ")";
            blood.transform.SetParent(bloodBarModel.transform);
            Image image = blood.GetComponent<Image>();
            image.rectTransform.localPosition = bloodModel.GetComponent<Image>().rectTransform.localPosition;
            image.rectTransform.Translate(Vector3.up * (image.rectTransform.rect.height + 2f) * i);

            bloodsList.Add(blood);
        }
    }

    // Update is called once per frame
    void Update() {
        for(int i = 0; i < bloodsList.Count; i++) {
            if(power > i * powerPerBlood) {
                bloodsList[i].SetActive(true);
            } else {
                bloodsList[i].SetActive(false);
            }
        }
    }

    static public GameplayManager GetInstance() {
        return instance;
    }
}
