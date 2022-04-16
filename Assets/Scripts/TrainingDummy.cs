using UnityEngine;

public class TrainingDummy : MonoBehaviour {
    public float health = 100f;
    public float armor = 0.2f;

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Bullet")) {
            Bullet bullet = col.gameObject.GetComponent<Bullet>();
            health -= bullet.damage - (bullet.damage * armor);

            Destroy(bullet.gameObject);
            if (health < 0) {
                Destroy(gameObject);
            }
        }
    }
}
