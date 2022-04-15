using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    Game game;
    HUD hud;
    Camera mainCamera;
    Transform endOfBarrel;

    bool isActive = true;
    Vector2 inputLook;
    Vector2 inputPos;
    Vector2 inputMove;
    bool inputPrimary;
    bool inputSecondary;

    [System.NonSerialized] public Rigidbody2D rb;
    [System.NonSerialized] public List<DamageModifier> damageModifiers = new List<DamageModifier>();
    [System.NonSerialized] public UtilitySkill utilitySkill;
    [System.NonSerialized] public Vector2 moveVector;
    [System.NonSerialized] public Cooldown fireCooldown;

    [System.NonSerialized] public float initialHealth = 100f;
    [System.NonSerialized] public float health = 100f;
    [System.NonSerialized] public float charge = 0f;
    [System.NonSerialized] public float damage = 3f;
    [System.NonSerialized] public float moveSpeed = 3f;
    [System.NonSerialized] public float bulletSpeed = 6f;
    [System.NonSerialized] public bool immortal = false;

    private void Awake() {
        endOfBarrel = transform.Find("EndOfBarrel");
        rb = GetComponent<Rigidbody2D>();
        game = GameObject.FindWithTag("Game").GetComponent<Game>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        hud = GameObject.Find("HUD").GetComponent<HUD>();
        fireCooldown = new Cooldown(0.4f);
        utilitySkill = new Blink();
    }

    void FixedUpdate() {
        if (isActive) {
            ShootAndCharge();
            Rotate();
            Move();
            if (utilitySkill != null) {
                utilitySkill.Update(this);
            }
        }
    }

    private void Update() {
        MoveCamera();
    }

    void ShootAndCharge() {
        fireCooldown.Tick();
        fireCooldown.factor = Mathf.Pow(2.0f, charge / 35f);

        if (inputPrimary) {
            inputSecondary = false;
            if (fireCooldown.isReady()) {
                fireCooldown.Reset();
                charge -= 10;
                Bullet newBullet = Instantiate(Prefabs.bullet, endOfBarrel.position, transform.rotation);
                newBullet.firedBy = this;
                newBullet.init(Player2CursorVector(), bulletSpeed);
                Destroy(newBullet.gameObject, 10);
            }
        }

        if (inputSecondary && !inputPrimary && charge < 100) {
            if (charge < 0) {
                charge += 1.0f * (1 + (Mathf.Abs(charge) / 100));
            } else if (charge > 0) {
                charge += 1.0f * (1 - (Mathf.Abs(charge) / 100));
            }
        }

        if (!inputSecondary) {
            if (charge < 0) {
                charge += 0.07f;
            } else {
                charge -= 1f;
            }
        }

        if (inputSecondary) {
            moveSpeed = 1f;
        } else {
            moveSpeed = 3.0f;
        }

        hud.ChangeChargeBar(charge / 200f + 0.5f);
    }

    void OnPrimary(InputValue value) {
        inputPrimary = value.isPressed;
    }

    void OnSecondary(InputValue value) {
        inputSecondary = value.isPressed;
    }

    void OnMove(InputValue value) {
        inputMove = value.Get<Vector2>();
    }

    void OnLook(InputValue value) {
        inputLook = value.Get<Vector2>();
    }

    void OnPos(InputValue value) {
        inputPos = value.Get<Vector2>();
    }

    void OnUtility(InputValue value) {
        if (value.isPressed) {
            if (utilitySkill != null) {
                utilitySkill.Trigger(this);
            }
        }
    }

    void OnUpdate() {}

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("ExitZone")) {
            isActive = false;
            game.EndGame(GameResult.Win);
        }
    }

    public void ChangeHealth(float value, ValueChangeMode valueChangeMode) {
        if (valueChangeMode == ValueChangeMode.Absolute) {
            health = value;
        } else if (valueChangeMode == ValueChangeMode.Relative) {
            health += value;
        }

        hud.ChangeHealthBar(health / initialHealth);

        if (health <= 0) {
            isActive = false;
            game.EndGame(GameResult.Lose);
        }
    }

    void Rotate() {
        Vector3 lookDirection = Player2CursorVector();
        float angleInDegrees = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90;
        rb.MoveRotation(angleInDegrees);
    }

    void Move() {
        moveVector = inputMove * moveSpeed;
        rb.velocity = moveVector;
    }

    void MoveCamera() {
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    Vector2 Player2CursorVector() {
        return mainCamera.ScreenToWorldPoint(inputPos) - transform.position;
    }
}