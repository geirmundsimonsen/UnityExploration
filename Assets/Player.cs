using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    Game game;
    HUD hud;
    Camera mainCamera;
    Transform endOfBarrel;

    bool isActive = false;
    Vector2 inputLook;
    Vector2 inputPos;
    bool inputPrimary;
    bool inputSecondary;

    public Rigidbody2D rb;
    public Vector2 inputMove;
    public float initialHealth = 100f;
    public float charge = 0f;
    public float health = 100f;
    public float damage = 3f;
    public List<DamageModifier> damageModifiers = new List<DamageModifier>();
    public UtilitySkill utilitySkill;
    public float moveSpeed;
    public Bullet bullet;
    public float bulletSpeed;
    public bool immortal = false;
    public int primaryCountdown = 0;

    private void Awake() {
        endOfBarrel = transform.Find("EndOfBarrel");
        rb = GetComponent<Rigidbody2D>();
        game = GameObject.FindWithTag("Game").GetComponent<Game>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        hud = GameObject.Find("HUD").GetComponent<HUD>();

        utilitySkill = new Blink();
    }

    void FixedUpdate() {
        if (isActive) {
            Primary();
            Secondary();
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

    void Primary() {
        primaryCountdown--;

        if (inputPrimary) {
            inputSecondary = false;
            if (primaryCountdown <= 0) {
                primaryCountdown = (int)(50 * Mathf.Pow(2.0f, -charge / 35f));
                charge -= 10;
                Bullet newBullet = Instantiate(bullet, endOfBarrel.position, transform.rotation);
                newBullet.firedBy = this;
                newBullet.init(Player2CursorVector(), bulletSpeed);
                Destroy(newBullet.gameObject, 10);
            }
        }
    }

    void Secondary() {
        hud.ChangeChargeBar(charge / 200f + 0.5f);
        if (inputSecondary && !inputPrimary && charge < 100) {
            charge += 1.0f;
        }
        if (!inputSecondary) {
            if (charge < 0) {
                charge += 0.07f;
            } else {
                charge -= 1f;
            }
        }
        if (inputSecondary) {
            //moveSpeed = 1.5f; buggy
        } else {
            //moveSpeed = 3.0f;
        }
    }

    void OnPrimary(InputValue value) {
        inputPrimary = value.isPressed;
    }

    void OnSecondary(InputValue value) {
        inputSecondary = value.isPressed;
    }

    void OnMove(InputValue value) {
        inputMove = value.Get<Vector2>() * moveSpeed;
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

    public void Reset() {
        ChangeHealth(initialHealth, ValueChangeMode.Absolute);
        isActive = true;
    }

    void Rotate() {
        Vector3 lookDirection = Player2CursorVector();
        float angleInDegrees = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90;
        rb.MoveRotation(angleInDegrees);
    }

    void Move() {
        rb.velocity = inputMove;
    }

    void MoveCamera() {
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    Vector2 Player2CursorVector() {
        return mainCamera.ScreenToWorldPoint(inputPos) - transform.position;
    }
}