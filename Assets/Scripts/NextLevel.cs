using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour {
    public bool toBlack;
    float alpha = 0;
    Image image;

    void Awake() {
        image = transform.GetChild(0).GetComponent<Image>();
    }

    void Update() {
        if (toBlack) {
            alpha += 0.002f;
            if (alpha < 1) {
                image.color = new Color(0, 0, 0, alpha);
            }
        }
    }
}
