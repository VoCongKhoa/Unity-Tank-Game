using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("va cham enemy");
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("va cham Ground");
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("va cham Player");
            Destroy(gameObject);
        }
    }

}
