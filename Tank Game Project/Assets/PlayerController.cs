using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, ICanDestroy
{
    public GameObject firePosition;
    public GameObject projectile;
    private float xInput;
    private float zInput;
    [Range(0, 10000)]
    [SerializeField]
    private float speed;
    [Range(50, 200)]
    [SerializeField]
    private float rotationSpeed;
    private Rigidbody rigidbody;
    private int health;
/*    private List<GameObject> projectileList = new List<GameObject>();*/

    public static int score;

    // SINGLETON
    private static PlayerController _instance;
    public static PlayerController Instance => _instance;

    private void Awake()
    {
        _instance = this;

    }


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        health = 90;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(firePosition.transform.position, firePosition.transform.forward * 100, Color.red);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Transform firePoint = firePosition.transform;
            Rigidbody rb = Instantiate(projectile, firePoint.position, Quaternion.identity).GetComponent<Rigidbody>();

            Debug.DrawRay(firePoint.position, firePoint.transform.forward * 100 , Color.red);

            rb.AddForce((firePosition.transform.forward) * 200f, ForceMode.Impulse);
            rb.AddTorque((firePosition.transform.right) * 2f, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ProjectileEnemy"))
        {
            if (health > 0)
            {
                takeDamage(30);
                if (health == 0)
                {
                    // Player khi destroy thi camera khong tim thay target => set false cho player
                    /*Destroy(gameObject);*/
                    gameObject.SetActive(false);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");

        // Xoay theo truc y
        var angle = xInput * Vector3.up;

        // Tuy chinh toc do xoay
        var lastAngle = angle * rotationSpeed;
        var moveDistance = zInput * speed;
        rigidbody.AddTorque(lastAngle);
        rigidbody.AddForce(moveDistance * transform.forward);
    }

/*    private GameObject GetProjectilePooling()
    {
        foreach(var projectile in projectileList)
        {
            if (!projectile.activeInHierarchy)
            {
                projectile.SetActive(true);
                return projectile;
            }
        }
        Transform firePoint = firePosition.transform;
        var returnProjectile = Instantiate(projectile, firePoint.position, Quaternion.identity);
        *//*var returnProjectile = Instantiate(projectile);*//*
        projectileList.Add(returnProjectile);
        return returnProjectile;
    }*/

    public void takeDamage(int damage)
    {
        health -= damage;
    }

    public void increaseScore()
    {
        score++;
    }

    public int getScore()
    {
        return score;
    }
}
