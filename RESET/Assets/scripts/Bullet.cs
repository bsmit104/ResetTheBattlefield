using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float destroyTime = 3f;
    // public GameObject bulletDecalPrefab;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, destroyTime);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("shotted");
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("shot");
        // Check if the bullet collided with an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the enemy GameObject
            Destroy(collision.gameObject);

            // Destroy the bullet GameObject
            Destroy(gameObject);
        }
        // else if (collision.gameObject.CompareTag("Wall"))
        // {
        //     // Instantiate bullet decal on the wall
        //     InstantiateBulletDecal(collision.contacts[0].point, collision.contacts[0].normal);

        //     // Destroy the bullet GameObject
        //     Destroy(gameObject);
        // }
    }

    //     // Instantiate bullet decal on the wall
    // void InstantiateBulletDecal(Vector3 position, Vector3 normal)
    // {
    //     GameObject bulletDecal = Instantiate(bulletDecalPrefab, position + normal * 0.001f, Quaternion.LookRotation(normal));
    //     Destroy(bulletDecal, destroyTime);  // Optionally, you can destroy the decal after a certain time
    // }

    }
