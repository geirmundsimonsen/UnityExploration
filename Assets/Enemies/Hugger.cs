using UnityEngine;

public class Hugger : Enemy {
    Transform target;
    Rigidbody2D rb;

    public float armor = 0.2f;
    public float moveSpeed = 1.5f;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        if (target == null) {
            FindTarget();
        } else {
            MoveTowardsTarget();
        }
    }

    void MoveTowardsTarget() {
        rb.velocity = (target.position - transform.position).normalized * moveSpeed;
        rb.rotation = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
    }

    void FindTarget() {
        GameObject obj = GameObject.FindWithTag("Player");
        if (obj != null) {
            target = obj.GetComponent<Transform>();
        }
    }
}
