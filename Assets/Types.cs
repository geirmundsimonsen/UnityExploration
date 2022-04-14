using System.Collections.Generic;

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