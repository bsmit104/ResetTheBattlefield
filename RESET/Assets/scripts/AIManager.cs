//asked chatgpt for FSM examples in Unity, it recommended attack, patrol, and chase
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class AIManager : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;  // Reference to the player's transform
    private bool isPlayerDetected = false;

    public float maxDetectionRange = 10f;
    public float chaseDistance = 5f;
    public float killDistance = 5f;
    public LayerMask obstacleLayer;

    public GameObject playerObject;

    public Transform[] patrolWaypoints;
    private int currentWaypointIndex = 0;

    public GameObject health;
    private PlayerHealth playerHealth;
    bool alreadystarted = true;

    private float distanceToPlayer;


    public enum AIState
    {
        Patrol,
        Chase,
        Attack
    }

    private AIState currentState = AIState.Patrol;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
        {
            Debug.Log("Player null");
        }
        SetNextWaypoint();

        playerHealth = health.GetComponent<PlayerHealth>();

        if (playerHealth == null)
        {
            Debug.Log("PlayerHealth null");
        }
    }

    void Update()
    {
        // Check line of sight to the player
        // if (Physics.Raycast(transform.position, (player.position - transform.position).normalized, out RaycastHit hit, maxDetectionRange, obstacleLayer))
        // {
        //     Debug.Log("hit");
        //     if (hit.collider.CompareTag("Player"))
        //     {
        //         // Player is in line of sight
        //         OnPlayerDetected();
        //     }
        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < chaseDistance)
        {
            OnPlayerDetected();
        }
        else
        {
            OnPlayerLost();
        }

        if (distanceToPlayer < killDistance)
        {
            Debug.Log("if");
            //killPlayer();
            if (alreadystarted) {
                StartCoroutine(StartKill());
            }
            alreadystarted = false;
        }
        else {
            Debug.Log("else");
            alreadystarted = true;
            StopCoroutine(StartKill());
        }
        // }
        // else
        // {
        //     Debug.Log("no hit");
        //     // The ray hit nothing, implying the player is not in line of sight
        //     OnPlayerLost();
        // }

        // FSM
        switch (currentState)
        {
            case AIState.Patrol:
                Debug.Log("Patrol State");
                // If player is detected, transition to Chase state
                if (isPlayerDetected)
                {
                    currentState = AIState.Chase;
                    break;
                }

                // If close to the current waypoint, set the next waypoint
                if (agent.remainingDistance < 1f)
                {
                    SetNextWaypoint();
                }

                // if (Vector3.Distance(transform.position, patrolWaypoints[currentWaypointIndex].position) < 1f)
                // {
                //     SetNextWaypoint();
                // }

                // Move towards the current waypoint
                agent.SetDestination(patrolWaypoints[currentWaypointIndex].position);
                break;

            case AIState.Chase:
                Debug.Log("Chase State");
                agent.SetDestination(player.position);

                if (Vector3.Distance(transform.position, player.position) < 2f)
                {
                    currentState = AIState.Attack;
                }
                break;

            case AIState.Attack:
                Debug.Log("Attack State");
                // not sure if gonna use
                break;

            default:
                break;
        }
    }

    private void OnPlayerDetected()
    {
        isPlayerDetected = true;
        currentState = AIState.Chase;
    }

    private void OnPlayerLost()
    {
        isPlayerDetected = false;
        currentState = AIState.Patrol;
    }

    private void killPlayer()
    {
        // if (playerHealth != null)
        // {
        //     playerObject.transform.position = new Vector3(640.9f, 14.021f, 65.394f);
        //     playerHealth.ChangeHealth(-5);
        // }
    //     if (Vector3.Distance(transform.position, player.position) < killDistance)
    // {
    //     // Apply damage to the player via the GameManager
    //     GameManager.Instance.ChangeHealth(-5); // Adjust the amount based on your game's needs

    //     // Optionally, if you want to respawn or move the player upon "death"
    //     // Consider handling this inside the GameManager or a dedicated method
    //     // after verifying the player's health reached 0 or below.
    //     // For instance:
    //     if (GameManager.Instance.CurrentHealth <= 0)
    //     {
    //         // Handle player death, like respawning or showing game over
    //         // This might reset the player's position, show UI, etc.
    //         Debug.Log("Player has been killed. Handle death logic here.");
    //         // Example: Reset player's position or trigger a respawn method
    //         playerObject.transform.position = new Vector3(640.9f, 14.021f, 65.394f);
    //     }
    // }
        if (playerHealth != null)
        {
            //playerObject.transform.position = new Vector3(640.9f, 14.021f, 65.394f);
            playerHealth.ChangeHealth(-5);
        }
        else
        {
            Debug.Log("PlayerHealth is null");
        }
    }

    private void SetNextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % patrolWaypoints.Length;
        agent.SetDestination(patrolWaypoints[currentWaypointIndex].position);
    }

    public void stopCoroutineAfterDie()
    {
        StopCoroutine(StartKill());
    }

    IEnumerator StartKill()
    {
        Debug.Log("enteredstartkill");
        while (distanceToPlayer < killDistance)
        {
            yield return new WaitForSeconds(1f); // 20 seconds is a good speed but less is for gameplay.

            // Decrease the score by 1.
            Debug.Log("hurtin");
            if (playerHealth != null)
            {
                playerHealth.ChangeHealth(-10);
            }
            else
            {
                Debug.Log("PlayerHealth is null");
            }
        }
    }
}