using System;
using System.Collections.Generic;
using UnityEngine;


public class Engine {
    public static void Calculate(Bullet bullet, Enemy enemy) {
        if (bullet.firedBy is Player) {
            Player player = bullet.firedBy as Player;
            float damage = player.damage;
            damage *= bullet.damage;
            foreach (DamageModifier dm in player.damageModifiers) {
                damage *= dm.CalculateModifiedDamage(player, enemy);
            }

            enemy.ChangeHealth(-damage, ValueChangeMode.Relative);
        }
    }

    public static void Calculate(Bullet bullet, Player player) {
        if (bullet.firedBy is Enemy) {
            Enemy enemy = bullet.firedBy as Enemy;
            float damage = enemy.damage;
            damage *= bullet.damage;
            if (player.immortal) {
                damage = 0;
            }
            player.ChangeHealth(-damage, ValueChangeMode.Relative);
        }
    }
}

// Dette er DAMAGE ITEMS!

public interface DamageModifier {
    float CalculateModifiedDamage(Player player, Enemy enemy);
}

public interface Stackable {
    int GetStackNum();
    void SetStackNum(int num);
}

public class CriticalStrike : DamageModifier, Stackable {
    int stackNum = 1;

    public float CalculateModifiedDamage(Player player, Enemy enemy) {
        if (UnityEngine.Random.value < (0.1 * stackNum)) {
            return 2;
        } else {
            return 1;
        }
    }

    public int GetStackNum() {
        return stackNum;
    }

    public void SetStackNum(int num) {
        stackNum = num;
    }
}

public class DelicateWatch : DamageModifier, Stackable {
    int stackNum = 1;

    public float CalculateModifiedDamage(Player player, Enemy enemy) {
        if (player.health / player.initialHealth > 0.25) {
            return (float)Math.Pow(1.2, stackNum);
        } else {
            return 1f;
        }
    }

    public int GetStackNum() {
        return stackNum;
    }

    public void SetStackNum(int num) {
        stackNum = num;
    }
}

public class ArmorPenetration : DamageModifier, Stackable {
    int stackNum = 1;

    public float CalculateModifiedDamage(Player player, Enemy enemy) {
        if (enemy.isBoss) {
            return (float)Math.Pow(1.2, stackNum);
        } else {
            return 1f;
        }
    }

    public int GetStackNum() {
        return stackNum;
    }

    public void SetStackNum(int num) {
        stackNum = num;
    }
}

// Dette er MOVE ITEMS!

public interface UtilitySkill {
    void Trigger(Player player);
    void Update(Player player);
}

public class Dash : UtilitySkill {
    Vector2 dashVector = Vector2.zero;
    Player p;

    public void Trigger(Player player) {
        p = player;
        dashVector = player.moveVector * 7;
        //player.immortal = true; // Invoke, Coroutines outside MonoBehaviour context...
    }

    public void Update(Player player) {
        dashVector *= 0.9f;
        player.rb.velocity += dashVector;
    }
}

public class Blink : UtilitySkill {
    public void Trigger(Player player) {
        player.rb.position += player.moveVector * 1;
    }

    public void Update(Player player) {

    }
}