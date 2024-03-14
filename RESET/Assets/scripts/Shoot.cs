using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Shoot : MonoBehaviour
{

    public GameObject bulletObj;
    public GameObject decalPrefab;
    public float bulletSpeed = 50f;
    public int maxBullets = 10;
    private int currentShots = 10;
    private bool isReloading = false;
    public ParticleSystem muzzle;

    public float reloadTime = 2f;

    public GameObject gun;
    private List<GameObject> decals = new List<GameObject>();
    public int maxDecals = 10;


    //sound
    //https://www.youtube.com/watch?v=D9xuTISZ5h4&ab_channel=VionixStudio
    public AudioSource audioSource;
    public AudioClip shootSound;

    public AudioSource reload;
    public AudioClip reloadSound;

    Animator animation1;

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
        audioSource.clip = shootSound;
        reload.clip = reloadSound;

        animation1 = GetComponent<Animator>();
        animation1.speed = 0;



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

                if (animation1.speed == 0)
                {
                    animation1.speed = 1;
                }

                ShootBullet();
                muzzle.Play();
                animation1.Play("Shoot", 0, 0.0f);
                currentShots--;
                audioSource.Play();



                //Debug.Log("Shots remaining: " + currentShots);
            }
        }
    }



    void Reloading()
    {
        if (Input.GetKeyDown(KeyCode.R) && currentShots < 10)
        {

            //Reload();

            if (animation1.speed == 0)
            {
                animation1.speed = 1;
            }
            isReloading = true;
            animation1.Play("Reload", 0, 0.0f);
            StartCoroutine(ReloadCoroutine());
            reload.Play();
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

        
    if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 200.0f))
    {
        CreateDecal(hit);
        Debug.Log("Hit object name: " + hit.collider.gameObject.name);
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 2.0f, Color.green, 0.2f, true);
        if (hit.collider.CompareTag("Enemy"))
        {
            // Implement enemy damage logic here
            Debug.Log("pew pew bitch!");
            Destroy(hit.collider.gameObject);
        }
    }
    else
    {
        Debug.Log("Missed");
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 2.0f, Color.red, 0.2f, true);
    }
        // if (Physics.Raycast(transform.position, transform.forward, out hit, 200.0f))
        // {
        //     CreateDecal(hit);
        //     Debug.Log(hit.collider.gameObject.name);
        //     Debug.DrawRay(transform.position, transform.forward * 2.0f, Color.green, 0.2f, true);
        // }
        // else
        // {
        //     Debug.Log("Missed");
        //     Debug.DrawRay(transform.position, transform.forward * 2.0f, Color.red, 0.2f, true);
        // }


        // Vector3 spawnPosition = transform.position + transform.forward + new Vector3(0, 0.5f, 0);

        // GameObject newBullet = Instantiate(bulletObj, spawnPosition, transform.rotation);



        // Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();
        // bulletRb.AddForce(Camera.main.transform.forward * bulletSpeed, ForceMode.VelocityChange);

    }

    void CreateDecal(RaycastHit hit)
    {
        Debug.Log("Hey");
        Debug.Log("Hit point: " + hit.point);
        Debug.Log("Hit normal: " + hit.normal);
        Debug.Log("Collider name: " + hit.collider.gameObject.name);
        if (decals.Count >= maxDecals)
        {
            Destroy(decals[0]);
            decals.RemoveAt(0);
        }

        var decal = Instantiate(decalPrefab, hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal));
        decals.Add(decal);
    }



}
