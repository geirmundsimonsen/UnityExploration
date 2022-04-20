using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour {
    public bool toBlack;
    float alpha = 0;
    Image image;

    void Awake() {
        image = GetComponentInChildren(typeof(Image)) as Image;
    }

    void Start() {
        
    }

    void Update() {
        if (toBlack) {

            alpha += 0.01f;
            if (alpha < 1) {
                Debug.Log(alpha);
                image.color = new Color(0, 0, 0, alpha);
            }
        }
    }
}
