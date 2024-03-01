using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimation : MonoBehaviour
{
    //animation referance 
    //https://forum.unity.com/threads/animator-control-reset-current-animation-being-played.286216/
    //https://www.youtube.com/watch?v=58Ci8UjvTsQ&t=805s&ab_channel=Tvtig

    Animator animation1;

    public int maxBullets = 10;
    private int currentShots = 10;
    private bool isReloading = false;
    public float reloadTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        animation1 = GetComponent<Animator>();

        // Play the animation from the start and pause it
        //animation1.Play("Shoot", 0, 0.0f);
        animation1.speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Reloading();
        if (Input.GetButtonDown("Fire1"))
        {
            if (currentShots > 0)
            {
                currentShots--;

                if (animation1.speed == 0)
                {
                    animation1.speed = 1;
                }
                else
                {
                    //Reset Animation on first frame.
                    animation1.Play("Shoot", 0, 0.0f);
                }
            }
        }
    }

    void Reloading()
    {
        if (Input.GetKeyDown(KeyCode.R) && currentShots < 10)
        {

            //Reload();
            isReloading = true;
            animation1.Play("Reload", 0, 0.0f);
            StartCoroutine(ReloadCoroutine());
        }
    }

    IEnumerator ReloadCoroutine()
    {


        yield return new WaitForSeconds(reloadTime);
        currentShots = maxBullets;
        isReloading = false;

    }

}