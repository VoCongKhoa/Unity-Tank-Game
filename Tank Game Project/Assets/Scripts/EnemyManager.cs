using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, ICanDestroy
{
    [Range(0, 100)]
    [SerializeField]
    private float speed;
    public GameObject firePosition;
    public GameObject projectile;
    private RaycastHit vision;
    [Range(0.1f, 100.0f)]
    [SerializeField]
    private float speedMove;
    private int health;
    private int visionRange;
    /*private List<GameObject> projectileList = new List<GameObject>();*/

    // Start is called before the first frame update
    void Start()
    {
        health = 90;
        visionRange = 30;
        /*Vector3 euler = transform.eulerAngles;
        euler.z = Random.Range(0f, 360f);
        transform.eulerAngles = euler;*/
        StartCoroutine(OnRandomMove());
        StartCoroutine(OnRandomShoot());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(firePosition.transform.position, transform.TransformDirection(Vector3.back) * 30, Color.red);
    }

    private void FixedUpdate()
    {
        
    }

    // Check have obstacle in vision range
    private bool CheckObstacle()
    {
        Ray ray = new(firePosition.transform.position, transform.TransformDirection(Vector3.back));
        if (Physics.Raycast(firePosition.transform.position, transform.TransformDirection(Vector3.back), out vision))
        {
            var hitPoint = vision.point;
            var distance = Vector3.Distance(hitPoint, firePosition.transform.position);
            return ((vision.collider.CompareTag("Wall") || vision.collider.CompareTag("Obstacle")) && distance < visionRange);
        }
        else
        {
            return false;
        }
    }

    // Randomly Move for enemy
    private IEnumerator OnRandomMove()
    {
        while (true)
        {
            // Check if there's obstacle in vision range, then random rotation another direction, then check is there obstacle again
            while (CheckObstacle())
            {
                yield return new WaitForSeconds(1);
                gameObject.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            }

            // If there's no obstacle in vision range, then move on 0.5s forward
            yield return new WaitForSeconds(1);
            var moveTimer = .5f;
            while (moveTimer > 0)
            {
                  gameObject.transform.Translate(speedMove * Time.deltaTime * Vector3.back);
                  moveTimer -= Time.deltaTime;
                  yield return null;
            }

        }
    }

    // Auto shoot on moving
    private IEnumerator OnRandomShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            Transform firePoint = firePosition.transform;
            Debug.DrawRay(firePoint.position, firePoint.transform.right * 100, Color.red);
            // Shoot 2 bulltes once
            for (int i = 0; i < 2; i++)
            {
                Rigidbody rb = Instantiate(projectile, firePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce((firePosition.transform.right) * 100f, ForceMode.Impulse);
                rb.AddTorque((firePosition.transform.up) * 2f, ForceMode.Impulse);
                yield return new WaitForSeconds(0.5f);
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if collider has tag is ProjectilePlayer (this is bullet which shooted from  Player), then taken damage
        // If collider has other tag that differ from ProjectilePlayer, enemy don't take damage.
        if (other.gameObject.CompareTag("ProjectilePlayer"))
        {
            if (health > 0)
            {
                takeDamage(30);
                if (health == 0)
                {
                    Destroy(gameObject);
                    PlayerController.Instance.increaseScore();
                }
            }
        }
    }

/*    private GameObject GetProjectilePooling()
    {
        foreach (var projectile in projectileList)
        {
            if (!projectile.activeInHierarchy)
            {
                projectile.SetActive(true);
                return projectile;
            }
        }
        var returnProjectile = Instantiate(projectile);
        Transform firePoint = firePosition.transform;
        Instantiate(projectile, firePoint.position, Quaternion.identity)
        *//*   var returnProjectile = Instantiate(projectile);*//*
        projectileList.Add(returnProjectile);
        return returnProjectile;
    }*/

    public void takeDamage(int damage)
    {
        health -= damage;
    }
}
