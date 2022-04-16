using UnityEngine;

public class PlayerStartingPos : MonoBehaviour {
    Transform playerTransform;

    private void Awake() {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    void Start() {
        playerTransform.position = new Vector3(transform.position.x, transform.position.y, playerTransform.position.z);
        playerTransform.rotation = transform.rotation;
    }
}
