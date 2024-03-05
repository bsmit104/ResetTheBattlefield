using UnityEngine;

public class GuardianBeam : MonoBehaviour
{
    public float beamRange = 5f;
    public ParticleSystem beamParticlesPrefab;  // Assign this in the inspector with your Particle System prefab

    private ParticleSystem beamParticles;  // Reference to the instantiated Particle System
    private Transform playerTransform;

    public float killDistance = 5f;
    private bool hasInitializedParticles = false;

    void Start()
    {
        // Assuming the player has a "Player" tag, find the player's transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if (playerTransform == null)
        {
            Debug.Log("Player null");
        }
    }

    void Update()
    {
        bool isPlayerInRange = IsPlayerInRange();

        if (isPlayerInRange && !hasInitializedParticles)
        {
            InitializeParticles();
        }
        else if (!isPlayerInRange && hasInitializedParticles)
        {
            ResetParticles();
        }

        if (isPlayerInRange)
        {
            UpdateParticlePositionAndRotation();
        }
    }

    void InitializeParticles()
    {
        Debug.Log("Initializing particles");
        // Instantiate the Particle System from the prefab at the guardian's position
        beamParticles = Instantiate(beamParticlesPrefab, transform.position, Quaternion.identity);

        // Ensure the instantiated Particle System is active
        if (!beamParticles.gameObject.activeSelf)
        {
            beamParticles.gameObject.SetActive(true);
        }

        // Debug logs to check the position
        Debug.Log("Guardian position: " + transform.position);
        Debug.Log("Particle System position: " + beamParticles.transform.position);

        hasInitializedParticles = true;
    }

    void ResetParticles()
    {
        Debug.Log("Resetting particles");
        // Stop and destroy the Particle System
        if (beamParticles != null)
        {
            beamParticles.Stop();
            Destroy(beamParticles.gameObject);
        }

        hasInitializedParticles = false;
    }

    void UpdateParticlePositionAndRotation()
    {
        Debug.Log("In a funk");
        if (playerTransform != null && beamParticles != null)
        {
            // Set the position of the particle system to the guardian's position
            beamParticles.transform.position = transform.position;

            // Rotate the particle system to face the player
            beamParticles.transform.LookAt(playerTransform.position);
        }
    }

    bool IsPlayerInRange()
    {
        // Check if the player is in range continuously
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        return distanceToPlayer < killDistance;
    }
}
// using UnityEngine;

// public class GuardianBeam : MonoBehaviour
// {
//     public float beamRange = 10f;
//     //public ParticleSystem beamParticles;
//     public ParticleSystem beamParticles;

//     private Transform playerTransform;
//     public GameObject playerObject;

//     public float killDistance = 15f;

//     void Start()
//     {
//         // Assuming the player has a "Player" tag, find the player's transform
//         playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
//         // player = GameObject.FindGameObjectWithTag("Player").transform;

//         if (playerTransform == null)
//         {
//             Debug.Log("Player null");
//         }
//     }

//     void Update()
//     {
//         if (IsPlayerInRange())
//         {
//             Debug.Log("beamtime yo");
//             // Enable Particle System and dynamically adjust the size
//             beamParticles.Play();
//             Debug.Log("beamtime playin");
//             // UpdateParticleSize();
//             UpdateParticlePositionAndRotation();
//             Debug.Log("updating");
//         }
//         else
//         {
//             // Disable Particle System
//             beamParticles.Stop();
//             Debug.Log("beamtime stop");
//         }
//     }

//     void UpdateParticlePositionAndRotation()
//     {
//         Debug.Log("in a funk");
//         if (playerTransform != null)
//         {
//             // Set the position of the particle system to the midpoint between the guardian and the player
//             Vector3 midpoint = (transform.position + playerTransform.position) / 2f;
//             beamParticles.transform.position = midpoint;

//             // Rotate the particle system to face the player
//             beamParticles.transform.LookAt(playerTransform.position);
//         }
//     }


//     // void UpdateParticleSize()
//     // {
//     //     if (playerTransform != null)
//     //     {
//     //         // Calculate the distance between the guardian and the player
//     //         float distance = Vector3.Distance(transform.position, playerTransform.position);

//     //         // Adjust the size based on the distance (you can customize this formula)
//     //         float sizeFactor = Mathf.Clamp01(1f - distance / beamRange);

//     //         // Set the start size of the particle system based on the size factor
//     //         var mainModule = beamParticles.main;
//     //         mainModule.startSizeMultiplier = sizeFactor;
//     //     }
//     // }

//     bool IsPlayerInRange()
//     {
//         // Your existing code to check if the player is in range
//         // ...
//         Debug.Log("in range yo");
//         float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
//         if (distanceToPlayer < killDistance)
//         {
//             return true;
//         }

//         return false;  // Replace this with your actual implementation
//     }
// }