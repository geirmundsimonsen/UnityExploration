using UnityEngine;

public class HUD : MonoBehaviour {
    RectTransform healthRectTransform;
    RectTransform chargeRectTransform;

    private void Awake() {
        healthRectTransform = GameObject.Find("health").GetComponent<RectTransform>();
        chargeRectTransform = GameObject.Find("charge").GetComponent<RectTransform>();
    }

    void Start() {
    }

    void Update() {
        
    }

    public void ChangeHealthBar(float normalizedAmount) {
        healthRectTransform.localPosition = new Vector3(-640 + (normalizedAmount * 640), 0);
    }

    public void ChangeChargeBar(float normalizedAmount) {
        chargeRectTransform.localPosition = new Vector3(-640 + (normalizedAmount * 640), 0);
    }
}
