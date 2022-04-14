using UnityEngine;

public class Snark : Enemy {
    Transform playerTransform;
    Transform endOfBarrel;
    Rigidbody2D rb;

    float counter = 0;

    public float armor = 0.2f;
    public float moveSpeed = 1.5f;
    public Bullet bullet;

    void Awake() {
        endOfBarrel = transform.Find("EndOfBarrel");
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;

        Rotate(directionToPlayer);

        if (IsPlayerInRange()) {
            Stop();
            Attack();
        } else {
            Move(directionToPlayer);
        }
    }

    public override void Attack() {
        counter += 1.0f;

        if (counter > 40.0f) {
            Bullet newBullet = Instantiate(bullet, endOfBarrel.position, transform.rotation);
            newBullet.firedBy = this;
            newBullet.init(playerTransform.position - transform.position, 5);
            Destroy(newBullet.gameObject, 10);

            counter = 0;
        }
    }

    void Move(Vector2 directionToPlayer) {
        rb.velocity = directionToPlayer * moveSpeed;
    }

    void Stop() {
        rb.velocity = Vector2.zero;
    }

    void Rotate(Vector2 directionToPlayer) {
        rb.rotation = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg - 90;
    }

    bool IsPlayerInRange() {
        return Vector2.Distance(transform.position, playerTransform.position) < 5;
    }
}
