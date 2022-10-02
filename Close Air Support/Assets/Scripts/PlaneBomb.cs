using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlaneBomb : MonoBehaviour
{
    //CHECKING THE PLANE'S ROTATION
    public GameObject planeBody;

    //BOMB DROP (WILL BE THE ONE SHOOTING OUT)
    public GameObject bombPrefab;
    //BOMB PROP (WILL !ENABLED WHEN PRESS SPACE)
    public GameObject bombLoad;

    //public float bombSpeed = 0.0f;
    //public float planeAngle = 0.0f;

    public bool planeRotation = false;

    public PlanePilot planePilot;
    public AudioSource jerichoTrumpet;
    public float jerichoVolume = 0.0f;
    public float jerichoVolumeStart = 0.0f;
    public float jerichoVolumeEnd = 0.0f;

    public void Start()
    {
        jerichoTrumpet.enabled = false;
        jerichoTrumpet.volume = 0;
    }

    public void Update()
    {
        //planeAngle = Mathf.Clamp(planeAngle, 40, 100); (THIS DOES NOT WORK)

        if (planeBody.transform.rotation.eulerAngles.x >= 40 && planeBody.transform.rotation.eulerAngles.x <= 100)
        {
            planeRotation = true;
            jerichoTrumpet.volume = jerichoTrumpet.volume + jerichoVolumeStart;
            jerichoTrumpet.enabled = true;
            //jerichoTrumpetEnd.enabled = false;
        }
        else
        {
            planeRotation = false;
            jerichoTrumpet.volume = jerichoTrumpet.volume - jerichoVolumeEnd;

            if(jerichoTrumpet.volume == 0)
            {
                jerichoTrumpet.enabled = false;
            }

            //jerichoTrumpetEnd.enabled = true;
        }

        if(planeRotation == true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                bombLoad.SetActive(false);
                GameObject currentBomb = Instantiate(bombPrefab, bombLoad.transform.position, bombLoad.transform.rotation);
                currentBomb.GetComponent<Rigidbody>().velocity = currentBomb.transform.forward * (planePilot.speed + 50);

                /*if(planePilot.speed > 100)
                {
                    currentBomb.GetComponent<Rigidbody>().velocity = currentBomb.transform.forward * (planePilot.speed - 100);
                }
                else if(planePilot.speed < 100)
                {
                    currentBomb.GetComponent<Rigidbody>().velocity = currentBomb.transform.forward * 50;
                }*/
            }
        }
    }
}