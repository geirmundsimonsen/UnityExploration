using UnityEngine;

public class Bullet : MonoBehaviour {
    Rigidbody2D rb;

    Vector2 shootDirection;
    float bulletSpeed;

    public float damage = 3f;
    public object firedBy;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        shootDirection = new Vector2(0, 0);
        bulletSpeed = 0;
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + (shootDirection * bulletSpeed * Time.fixedDeltaTime));
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (firedBy is Player && col.gameObject.GetComponent<Enemy>() != null) {
            Engine.Calculate(this, col.gameObject.GetComponent<Enemy>());
            Destroy(gameObject);
        }

        if (firedBy is Enemy && col.gameObject.GetComponent<Player>() != null) {
            Engine.Calculate(this, col.gameObject.GetComponent<Player>());
            Destroy(gameObject);
        }
    }

    public void init(Vector2 lookDirection, float bulletSpeedParam) {
        bulletSpeed = bulletSpeedParam;
        shootDirection = lookDirection.normalized;
    }
}
