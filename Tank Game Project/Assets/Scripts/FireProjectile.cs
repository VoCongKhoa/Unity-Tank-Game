using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
   /* public GameObject projectile;
    public float fireVelocity = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject firedProjectile = Instantiate(projectile, transform.position, transform.rotation);
            firedProjectile.GetComponent<Rigidbody>().AddRelativeForce((transform.position - firedProjectile.transform.position)*fireVelocity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        *//*        if (collision.gameObject.CompareTag("Player"))
                {
                    GameObject[] destroyProjectiles = GameObject.FindGameObjectsWithTag("Projectile");
                    for(int i = 0; i< destroyProjectiles.Length; i++)
                    {
                        Destroy(destroyProjectiles[i]);
                    }
                }*//*

        Debug.Log("va cham");
    }*/
}
