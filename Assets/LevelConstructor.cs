using UnityEngine;

public class LevelConstructor : MonoBehaviour {
    public LevelAreaData levelAreaData;

    public void Spawn() {
        Instantiate(Prefabs.levelArea);

        float playerSpawnPointWidth = levelAreaData.width - 1;
        float playerSpawnPointHeight = levelAreaData.height - 1;

        float exitZoneSpawnPointWidth = levelAreaData.width - 5;
        float exitZoneSpawnPointHeight = levelAreaData.height - 5;

        float enemySpawnPointWidth = levelAreaData.width - 2;
        float enemySpawnPointHeight = levelAreaData.height - 2;


        Instantiate(Prefabs.playerStartingPos, new Vector3(
            Random.Range(-(playerSpawnPointWidth / 2), playerSpawnPointWidth / 2),
            Random.Range(-(playerSpawnPointHeight / 2), playerSpawnPointHeight / 2),
            0), Quaternion.Euler(0, 0, 0));

        Instantiate(Prefabs.exitZone, new Vector3(
            Random.Range(-(exitZoneSpawnPointWidth / 2), exitZoneSpawnPointWidth / 2),
            Random.Range(-(exitZoneSpawnPointHeight / 2), exitZoneSpawnPointHeight / 2),
            0), Quaternion.Euler(0, 0, 0));

        for (int i = 0; i < 20; i++) {
            Instantiate(Prefabs.hugger, new Vector3(
                Random.Range(-(enemySpawnPointWidth / 2), enemySpawnPointWidth / 2),
                Random.Range(-(enemySpawnPointHeight / 2), enemySpawnPointHeight / 2),
                0), Quaternion.Euler(0, 0, 0));

            Instantiate(Prefabs.snark, new Vector3(
                Random.Range(-(enemySpawnPointWidth / 2), enemySpawnPointWidth / 2),
                Random.Range(-(enemySpawnPointHeight / 2), enemySpawnPointHeight / 2),
                0), Quaternion.Euler(0, 0, 0));
        }  
    }
}
