using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject wall;
    public Vector3 spawnValues;
    public float startWait;
    public float speed;
    public float range;
    public float maxSpeed;

    void Start()
    {
        BelowWallController.speed = speed;
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            Vector3 spawnPosition = new Vector3(spawnValues.x, spawnValues.y, spawnValues.z);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(wall, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(range / speed);
            if(speed < maxSpeed)
            {
                speed += 0.5F;
                BelowWallController.speed = speed;
            }
        }
    }
}
