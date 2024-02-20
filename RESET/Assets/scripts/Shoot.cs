using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Shoot : MonoBehaviour
{

    public GameObject bulletObj;
    public float bulletSpeed = 50f;
    public int maxBullets = 10;
    private int currentShots = 10;
    private bool isReloading = false;

    public float reloadTime = 2f;

    public GameObject gun;


    //sound
    //https://www.youtube.com/watch?v=D9xuTISZ5h4&ab_channel=VionixStudio
    // public AudioSource audioSource;
    // public AudioClip shootSound;

    // public AudioSource reload;
    // public AudioClip reloadSound;

    // Start is called before the first frame update
    void Start()
    {
        if (gun.activeSelf)
        {
            Debug.Log("Object is enabled.");
        }
        else
        {
            Debug.Log("Object is disabled.");
        }
        // audioSource.clip = shootSound;
        // reload.clip = reloadSound;

    }



    // Update is called once per frame
    void Update()
    {
        Shooting();
        Reloading();
    }

    void Shooting()
    {
        if (Input.GetButtonDown("Fire1") && !PauseButtons.Paused && !isReloading)
        {
            if (currentShots > 0)
            {
                ShootBullet();

                currentShots--;
                // audioSource.Play();



                //Debug.Log("Shots remaining: " + currentShots);
            }
        }
    }



    void Reloading()
    {
        if (Input.GetKeyDown(KeyCode.R) && currentShots < 10)
        {

            //Reload();
            isReloading = true;
            StartCoroutine(ReloadCoroutine());
            // reload.Play();
        }
    }

    IEnumerator ReloadCoroutine()
    {


        yield return new WaitForSeconds(reloadTime);
        currentShots = maxBullets;
        isReloading = false;

    }

    void ShootBullet()
        {
            //ChatGPT How can I make the Bullet shoot base on where the camera is pointing
            //Fixed lecture notes code.
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, 200.0f))
            {
                Debug.Log(hit.collider.gameObject.name);
                Debug.DrawRay(transform.position, transform.forward * 2.0f, Color.green, 0.2f, true);
            }
            else
            {
                //Debug.Log("Missed");
                Debug.DrawRay(transform.position, transform.forward * 2.0f, Color.red, 0.2f, true);
            }


            Vector3 spawnPosition = transform.position + transform.forward + new Vector3(0, 0.5f, 0);

            GameObject newBullet = Instantiate(bulletObj, spawnPosition, transform.rotation);



            Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(Camera.main.transform.forward * bulletSpeed, ForceMode.VelocityChange);

        }


   
    }
