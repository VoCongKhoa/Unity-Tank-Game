using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject enemy;
    private GameObject[] spawnPoints;
    public GameObject spawnPoint;
    public GameObject wallUnit;
    private int countEnemy = 0;
    // Start is called before the first frame update

    private void Awake()
    {
        spawnPoints[0] = Instantiate(spawnPoint, new Vector3(45, wallUnit.transform.position.y, 445), Quaternion.identity);
        spawnPoints[1] = Instantiate(spawnPoint, new Vector3(245, wallUnit.transform.position.y, 445), Quaternion.identity);
        spawnPoints[2] = Instantiate(spawnPoint, new Vector3(445, wallUnit.transform.position.y, 445), Quaternion.identity);
    }
    void Start()
    {
        Debug.Log("Starting " + Time.time);
        StartCoroutine(spawnRandomEnemy(5f));
    }

    private IEnumerator spawnRandomEnemy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Start");
        int randomNumber = UnityEngine.Random.Range(0, spawnPoints.Length);
        Instantiate(enemy, spawnPoints[randomNumber].transform.position, Quaternion.identity);
        countEnemy++;
        Debug.Log("Spawn " + Time.time);
        Debug.Log("Count: " + countEnemy);
        if (countEnemy < 11)
        {
            StartCoroutine(spawnRandomEnemy(waitTime));
        }
        else
        {
            yield break;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

}
