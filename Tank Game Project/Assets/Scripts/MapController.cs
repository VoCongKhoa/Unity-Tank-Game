using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour
{
    public GameObject wallUnit;
    public GameObject spawnPoint;
    public int wallLength;
    public GameObject enemy;

    // Enemy spawn points
    private GameObject spawnPoint1;
    private GameObject spawnPoint2;
    private GameObject spawnPoint3;

    // Obstacle spawn points
    public GameObject obstacleSpawn1;
    public GameObject obstacleSpawn2;
    public GameObject obstacleSpawn3;
    public GameObject obstacleSpawn4;
    public GameObject obstacleSpawn5;
    public GameObject obstacleSpawn6;
    public GameObject obstacleSpawn7;
    public GameObject obstacleSpawn8;
    public GameObject obstacleSpawn9;
    public GameObject obstacleSpawn10;
    public GameObject obstacleSpawn11;
    public GameObject[] obstacleList;
    private int countEnemy = 0;

    public Button nextLevelBtn;

    // Timer
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public Text timer;

    // Score text
    public Text scoreText;



    private void Awake()
    {
        nextLevelBtn.gameObject.SetActive(false);


        // Generate Wall
        for (int i = 0; i < wallLength; i++)
        {
            for (int j = 0; j < wallLength; j++)
            {
                if (i == 0 || j == 0 || j == wallLength - 1 || i == wallLength - 1)
                {
                    Instantiate(wallUnit, new Vector3(i * 10, wallUnit.transform.position.y, j * 10), Quaternion.identity);
                }
            }
        }

        // Generate Enemy Spawn Point
        spawnPoint1 = Instantiate(spawnPoint, new Vector3(45, wallUnit.transform.position.y, 445), Quaternion.identity);
        spawnPoint2 = Instantiate(spawnPoint, new Vector3(245, wallUnit.transform.position.y, 445), Quaternion.identity);
        spawnPoint3 = Instantiate(spawnPoint, new Vector3(445, wallUnit.transform.position.y, 445), Quaternion.identity);

        // Generate Obstacles Random
        Instantiate(obstacleList[Random.Range(0, 5)], new Vector3(obstacleSpawn1.transform.position.x, obstacleSpawn1.transform.position.y, obstacleSpawn1.transform.position.z), Quaternion.identity);
        Instantiate(obstacleList[Random.Range(0, 5)], new Vector3(obstacleSpawn2.transform.position.x, obstacleSpawn2.transform.position.y, obstacleSpawn2.transform.position.z), Quaternion.identity);
        Instantiate(obstacleList[Random.Range(0, 5)], new Vector3(obstacleSpawn3.transform.position.x, obstacleSpawn3.transform.position.y, obstacleSpawn3.transform.position.z), Quaternion.identity);
        Instantiate(obstacleList[Random.Range(0, 5)], new Vector3(obstacleSpawn4.transform.position.x, obstacleSpawn4.transform.position.y, obstacleSpawn4.transform.position.z), Quaternion.identity);
        Instantiate(obstacleList[Random.Range(0, 5)], new Vector3(obstacleSpawn5.transform.position.x, obstacleSpawn5.transform.position.y, obstacleSpawn5.transform.position.z), Quaternion.identity);
        Instantiate(obstacleList[Random.Range(0, 5)], new Vector3(obstacleSpawn6.transform.position.x, obstacleSpawn6.transform.position.y, obstacleSpawn6.transform.position.z), Quaternion.identity);
        Instantiate(obstacleList[Random.Range(0, 5)], new Vector3(obstacleSpawn7.transform.position.x, obstacleSpawn7.transform.position.y, obstacleSpawn7.transform.position.z), Quaternion.identity);
        Instantiate(obstacleList[Random.Range(0, 5)], new Vector3(obstacleSpawn8.transform.position.x, obstacleSpawn8.transform.position.y, obstacleSpawn8.transform.position.z), Quaternion.identity);
        Instantiate(obstacleList[Random.Range(0, 5)], new Vector3(obstacleSpawn9.transform.position.x, obstacleSpawn9.transform.position.y, obstacleSpawn9.transform.position.z), Quaternion.identity);
        Instantiate(obstacleList[Random.Range(0, 5)], new Vector3(obstacleSpawn10.transform.position.x, obstacleSpawn10.transform.position.y, obstacleSpawn10.transform.position.z), Quaternion.identity);
        Instantiate(obstacleList[Random.Range(0, 5)], new Vector3(obstacleSpawn11.transform.position.x, obstacleSpawn11.transform.position.y, obstacleSpawn11.transform.position.z), Quaternion.identity);

    }
    // Start is called before the first frame update
    void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;

        StartCoroutine(spawnRandomEnemy(5f));
    }

    // Spawn 3 Enemy every 5 seconds
    private IEnumerator spawnRandomEnemy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Start");
        Instantiate(enemy, spawnPoint1.transform.position, Quaternion.identity);
        Instantiate(enemy, spawnPoint2.transform.position, Quaternion.identity);
        Instantiate(enemy, spawnPoint3.transform.position, Quaternion.identity);
        
        countEnemy += 3;
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
        // Timer running
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                nextLevelBtn.gameObject.SetActive(true);
            }
        }

        // Score display
        scoreText.text = $"Score: {PlayerController.Instance.getScore()}";
    }

    //Timer display
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timer.text = "Time remaining: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
