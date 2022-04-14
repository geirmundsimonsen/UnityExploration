using System.Collections.Generic;
using UnityEngine;

public enum ValueChangeMode {
    Absolute,
    Relative
}

public enum GameResult {
    Win,
    Lose
}

public class EnemyStats {
    public bool damageOnContact;
}

public class Debug {
    static System.DateTime timeAtBeginning = System.DateTime.Now;
    public static void Log(object obj) {
        System.TimeSpan ts = (System.DateTime.Now - timeAtBeginning);
        string time = string.Format("{0:N3}", ts.TotalSeconds);
        UnityEngine.Debug.Log(time + ": " + obj);
    }
}

public class Value {
    public float value;

    public Value(float v) {
        value = v;
    }
}

public class Cooldown {
    float cooldown;
    float fixedDeltaTime;
    float runningCooldown;
    
    public float factor = 1.0f;

    public Cooldown(float cooldown) {
        this.cooldown = cooldown;
        fixedDeltaTime = Time.fixedDeltaTime;
        Reset();
    }

    public void Tick() {
        Debug.Log(factor);
        runningCooldown -= fixedDeltaTime * factor;
    }

    public bool isReady() {
        return runningCooldown <= 0;
    }

    public void Reset() {
        runningCooldown = cooldown;
    }
}