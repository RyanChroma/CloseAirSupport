using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlaneGun : MonoBehaviour
{
    //BULLET
    public GameObject bullet;

    //BULLET FORCE
    public float shootForce, upwardForce;

    //GUN STATS
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    int bulletsLeft, bulletsShot;

    //BOOLS
    bool shooting, readyToShoot, reloading;

    //REFERENCE
    public Camera fpsCam;
    public Transform attackPoint;

    //GRAPHICS
    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;

    //BUG FIXING
    public bool allowInvoke = true;

    public void Awake()
    {
        //MAKE SURE MAGAZINE IS FULL
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    public void Update()
    {
        MyInput();

        //SET AMMO DISPLAY, IF IT EXISTS
        if(ammunitionDisplay != null)
		{
            ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);
		}
    }

    public void MyInput()
	{
        //CHECK IF ALLOWED TO HOLD DOWN BUTTON AND TAKE CORRESPONDING INPUT
        if(allowButtonHold)
		{
            shooting = Input.GetKey(KeyCode.Mouse0);
		}
		else
		{
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
		}

        //RELOADING
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Reload();
        }

        //RELOAD AUTOMATICALLY WHEN TRYING TO SHOOT WITHOUT AMMO
        if(readyToShoot && shooting && !reloading && bulletsLeft <= 0)
		{
            Reload();
		}

        //SHOOTING
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
		{
            //SET BULLETS SHOT TO 0
            bulletsShot = 0;

            Shoot();
		}
	}

    public void Shoot()
	{
        readyToShoot = false;

        //FIND THE EXACT HIT POSITION USING A RAYCAST

        // JUST A RAY THROUGH THE MIDDLE OF YOUR SCREEN (WE DO NOT NEED RAYCAST FOR AIRPLANE)
        /*Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        //CHECK IF RAY HITS SOMETHING
        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit))
		{
            targetPoint = hit.point;
		}
        else
		{
            //JUST A POINT FAR AWAY FROM THE PLAYER
            targetPoint = ray.GetPoint(75);
		}*/

        //CALCULATE DIRECTION FROM ATTACKPOINT TO TARGETPOINT
        //Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //CALCULATE SPREAD
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //CALCULATE NEW DIRECTION WITH SPREAD

        //JUST ADD SPREAD TO LAST DIRECTION
        //Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        //INSTANTIATE BULLET/PROJECTILE
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, attackPoint.rotation);
        //ROTATE BULLET TO SHOOT DIRECTION
        //currentBullet.transform.forward = directionWithoutSpread.normalized;

        //ADD FORCES TO BULLET
        //currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(attackPoint.transform.forward * shootForce, ForceMode.Impulse);

        //INSTANTIATE MUZZLE FLASH, IF YOU HAVE ONE
        if(muzzleFlash != null)
		{
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
		}

        bulletsLeft--;
        bulletsShot++;

        //INVOKE RESETSHOT FUNCTION (IF NOT ALREADY INVOKED)
        if(allowInvoke)
		{
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
		}

        //IF MORE THAN ONE BULLETSPERTAP MAKE SURE TO REPEAT SHOOT FUNCTION
        if(bulletsShot < bulletsPerTap && bulletsLeft > 0)
		{
            Invoke("Shoot", timeBetweenShots);
		}
	}

    public void ResetShot()
	{
        //ALLOW SHOOTING AND INVOKING AGAIN
        readyToShoot = true;
        allowInvoke = true;
	}

    public void Reload()
	{
        bulletsLeft = magazineSize;
        reloading = false;
	}
}