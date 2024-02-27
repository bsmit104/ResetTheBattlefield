// using UnityEngine;

// public class GameManager : MonoBehaviour
// {
//     public static GameManager Instance;

//     public int CurrentHealth { get; private set; }
//     public int MaxHealth { get; private set; } = 90;

//     private void Awake()
//     {
//         if (Instance == null)
//         {
//             Instance = this;
//             DontDestroyOnLoad(gameObject);
//         }
//         else if (Instance != this)
//         {
//             Destroy(gameObject);
//         }

//         CurrentHealth = MaxHealth; // Initialize health when the game starts
//     }

//     public void ChangeHealth(int amount)
//     {
//         CurrentHealth += amount;
//         CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
//     }
// }