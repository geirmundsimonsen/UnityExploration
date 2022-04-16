using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Tried to use TestRunner (Window>General>Test Runner) after following several tutorials
Assembly definitions were created as explained
This caused the package Player Input to not be found, got bored
So this is the test class for now - at least it works.
*/
public class TestBench {
    public static void Test() {
        //FixedDeltaTimeVsFloat();
    }

    // adding fixedDeltaTime: 105ms. adding floats: 37ms. using Cooldown.Tick(): 37ms.
    public static void FixedDeltaTimeVsFloat() {
        float sum = 0.0f;
        Benchmark.Start();
        for (int i = 0; i < 10000000; i++) {
            sum += Time.fixedDeltaTime;
        }
        Debug.Log(Benchmark.Stop());

        sum = 0.0f;
        Benchmark.Start();
        for (int i = 0; i < 10000000; i++) {
            sum += 0.02f;
        }
        Debug.Log(Benchmark.Stop());

        Cooldown cd = new Cooldown(1.0f);
        Benchmark.Start();
        for (int i = 0; i < 10000000; i++) {
            cd.Tick();
        }
        Debug.Log(Benchmark.Stop());
    }
}

public class Benchmark {
    static System.DateTime startTime;

    public static void Start() {
        startTime = System.DateTime.Now;
    }

    public static double Stop() {
        return (System.DateTime.Now - startTime).TotalSeconds;
    }
}