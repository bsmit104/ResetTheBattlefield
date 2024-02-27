//Inspo from lecture
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private int maxHealth = 90;
    public static bool died = false;
    private int currentHealth;
    public GameObject playerObject;
    public GameObject gameOver;

    // public GameObject score;

    // public GameObject win;
    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar == null) {
            GameObject.Find("healthBar");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeHealth(-5);
        }
    }

    public void ChangeHealth(int amount) {
        currentHealth += amount;
        if (currentHealth <= 0) {
            DeadPause();
        }
        if (currentHealth >= maxHealth) {
            currentHealth = maxHealth;
        }
        healthBar.value = currentHealth;
    }

     public void BackAlive() {
        gameOver.SetActive(false);
        // win.SetActive(false);
        Time.timeScale = 1f;
        died = false;
        ChangeHealth(90);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerObject != null)
        {
            playerObject.transform.position = new Vector3(640.9f, 14.021f, 65.394f);
        }
        else {
            Debug.Log("playerObject is null");
        }
    }

    void DeadPause() {
        gameOver.SetActive(true);
        Time.timeScale = 0f;
        died = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

//////////////////////if using game manager to preserve health/////////////////
// using UnityEngine;
// using UnityEngine.UI;

// public class PlayerHealth : MonoBehaviour
// {
//     [SerializeField] private Slider healthBar;
//     public GameObject playerObject;
//     public GameObject gameOver;

//     void Start()
//     {
//         UpdateHealthUI();
//     }

//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Space))
//         {
//             GameManager.Instance.ChangeHealth(-5);
//             UpdateHealthUI();
//         }
//     }

//     private void UpdateHealthUI()
//     {
//         if (healthBar != null)
//         {
//             healthBar.value = (float)GameManager.Instance.CurrentHealth / GameManager.Instance.MaxHealth;
//         }
//     }

//     public void BackAlive()
//     {
//         gameOver.SetActive(false);
//         Time.timeScale = 1f;
//         GameManager.Instance.ChangeHealth(GameManager.Instance.MaxHealth - GameManager.Instance.CurrentHealth); // Fully restore health
//         Cursor.lockState = CursorLockMode.Locked;
//         Cursor.visible = false;

//         if (playerObject != null)
//         {
//             playerObject.transform.position = new Vector3(640.9f, 14.021f, 65.394f);
//         }
//         else
//         {
//             Debug.Log("playerObject is null");
//         }
//     }

//     void DeadPause()
//     {
//         gameOver.SetActive(true);
//         Time.timeScale = 0f;
//         Cursor.lockState = CursorLockMode.None;
//         Cursor.visible = true;
//     }
// }