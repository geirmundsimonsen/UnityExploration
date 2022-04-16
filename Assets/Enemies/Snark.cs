using UnityEngine;

public class Snark : Enemy {
    Transform target;
    Transform endOfBarrel;
    Rigidbody2D rb;

    float counter = 0;

    public float armor = 0.2f;
    public float moveSpeed = 1.5f;

    void Awake() {
        endOfBarrel = transform.Find("EndOfBarrel");   
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        if (target == null) {
            FindTarget();
        } else {
            Vector2 targetDirection = (target.position - transform.position).normalized;

            RotateTowardsTarget(targetDirection);

            if (IsPlayerInRange()) {
                Stop();
                Attack();
            } else {
                MoveTowardsTarget(targetDirection);
            }
        }
    }

    public override void Attack() {
        counter += 1.0f;

        if (counter > 40.0f) {
            Bullet newBullet = Instantiate(Prefabs.bullet, endOfBarrel.position, transform.rotation);
            newBullet.firedBy = this;
            newBullet.init(target.position - transform.position, 5);
            Destroy(newBullet.gameObject, 10);

            counter = 0;
        }
    }

    void MoveTowardsTarget(Vector2 targetDirection) {
        rb.velocity = targetDirection * moveSpeed;
    }

    void Stop() {
        rb.velocity = Vector2.zero;
    }

    void RotateTowardsTarget(Vector2 targetDirection) {
        rb.rotation = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90;
    }

    bool IsPlayerInRange() {
        return Vector2.Distance(transform.position, target.position) < 5;
    }

    void FindTarget() {
        GameObject obj = GameObject.FindWithTag("Player");
        if (obj != null) {
            target = obj.GetComponent<Transform>();
        }
    }
}
