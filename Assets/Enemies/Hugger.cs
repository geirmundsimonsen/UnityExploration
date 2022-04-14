using UnityEngine;

public class Hugger : Enemy {
    Transform playerTransform;
    Rigidbody2D rb;

    public float armor = 0.2f;
    public float moveSpeed = 1.5f;

    void Awake() {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        Move();
    }

    void Move() {
        rb.velocity = (playerTransform.position - transform.position).normalized * moveSpeed;
        rb.rotation = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
    }
}
