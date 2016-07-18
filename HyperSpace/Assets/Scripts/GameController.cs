using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject wall;
    public GameObject player;
    public Vector3 spawnValues;
    public float startWait;
    public float speed;
    public float range;
    public float maxSpeed;

    void Start()
    {
        Profiler.maxNumberOfSamplesPerFrame = 3;
        Vector3 spawnPosition = new Vector3(0, 1, -17);
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(player, spawnPosition, spawnRotation);
        BelowWallController.speed = speed;
        BelowWallController.maxSpeed = maxSpeed;
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        for (int i = 0; i < 3; ++i )
        {
            yield return new WaitForSeconds(13F / speed);
            Vector3 spawnPosition = new Vector3(spawnValues.x, spawnValues.y, spawnValues.z);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(wall, spawnPosition, spawnRotation);
        }
    }


}
