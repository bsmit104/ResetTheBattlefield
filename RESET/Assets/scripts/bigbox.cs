using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigbox : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform player;
    public GameObject playerObject;
    public GameObject health;
    private PlayerHealth playerHealth;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
        {
            Debug.Log("Player null");
        }

        playerHealth = health.GetComponent<PlayerHealth>();

        if (playerHealth == null)
        {
            Debug.Log("PlayerHealth null");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (playerHealth != null)
            {
                // playerObject.transform.position = new Vector3(640.9f, 14.021f, 65.394f);
                playerHealth.ChangeHealth(-110);
            }
            else
            {
                Debug.Log("PlayerHealth is null");
            }
        }
    }
}
