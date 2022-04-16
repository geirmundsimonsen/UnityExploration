using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField]
    public float health;
    [SerializeField]
    public float damage;
    [SerializeField]
    public bool isBoss;

    public virtual void Attack() {

    }

    public virtual void ChangeHealth(float value, ValueChangeMode valueChangeMode) {
        if (valueChangeMode == ValueChangeMode.Absolute) {
            health = value;
        } else if (valueChangeMode == ValueChangeMode.Relative) {
            health += value;
        }

        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
