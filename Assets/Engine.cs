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
        dashVector = player.inputMove * 7;
        //player.immortal = true; // Invoke, Coroutines outside MonoBehaviour context...
    }

    public void Update(Player player) {
        dashVector *= 0.9f;
        player.rb.velocity += dashVector;
    }
}

public class Blink : UtilitySkill {
    public void Trigger(Player player) {
        player.rb.position += player.inputMove * 1;
    }

    public void Update(Player player) {

    }
}

public class TestEngine {
    public static void Test() {
        /*
        // Bullet meets Enemy
        {
            Player p = new Player();
            Enemy e = new Snark();
            Bullet b = new Bullet();
            b.firedBy = p;
            Engine.Calculate(b, e);
            if (e.health != 91f) { throw new Exception(); }
        }
        
        // Bullet with CriticalStrike meets Enemy
        {
            P p = new P();
            E e = new E();
            B b = new B();
            b.firedBy = p;
            p.damageModifiers.Add(new CriticalStrike());
            Engine.Calculate(b, e);
            if (!(e.health == 82f || e.health == 91f)) { throw new Exception(); }
        }

        // Bullet with CriticalStrike x10 meets Player
        {
            P p = new P();
            E e = new E();
            B b = new B();
            b.firedBy = p;
            p.damageModifiers.Add(new CriticalStrike());
            (p.damageModifiers[0] as CriticalStrike).SetStackNum(10);
            Engine.Calculate(b, e);
            Debug.Log(e.health);
            if (e.health != 82f) { throw new Exception(); }
        }

        // Benchmark
        {
            P p = new P();
            E e = new E();
            B b = new B();
            b.firedBy = p;
            p.damageModifiers.Add(new CriticalStrike());

            // Version with CalculateDamageV1: ~3.000.000 times/s. (no mods)
            // Version with CalculateDamageV2: ~2.450.000 times/s. (mods, using function variables)
            // Version with CalculateDamageV3: ~2.450.000 times/s. (mods, using interfaces)
            // Version with CalculateDamageV3: ~3.450.000 times/s. (mods, and now using classes, and more assumptions)
            int counter = 0;
            System.DateTime t1 = System.DateTime.Now;
            while ((System.DateTime.Now - t1).TotalSeconds < 1) {
                Engine.Calculate(b, e);
                counter++;
            }
            Debug.Log(counter);
        }
        */
    }
}