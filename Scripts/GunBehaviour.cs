using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviour : MonoBehaviour {

    public AudioClip shotSound;
	public float damage;
	public float range;
    public float force;
    public float fireRate;

    public Animator animator;
    public int maxAmmo;
    private int currentAmmo;
    public float reloadTime;
    private bool isReloading = false;

    public Camera fpscam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public bool isAutomatic;

    private float nextTimeToFire;


    void Start()
    {
        
        currentAmmo = maxAmmo;
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("isReloading", false);
    }


    // Update is called once per frame
    void Update () {

        if (isReloading)
            return;
        if (currentAmmo <= 0)
        {

            StartCoroutine(Reload());
            return;
        }

		if(Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && isAutomatic == false || Input.GetButton("Fire1") && Time.time >= nextTimeToFire && isAutomatic == true)
        {
            animator.SetTrigger("Shooting");
            GetComponent<AudioSource>().PlayOneShot(shotSound);
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
           
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
	}

    IEnumerator Reload()
    {
        isReloading = true;

        Debug.Log("Reloading...");

        animator.SetBool("isReloading",true);

        yield return new WaitForSeconds(reloadTime - .25f);

        animator.SetBool("isReloading", false);

        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

	void Shoot()
	{
        muzzleFlash.Play();
        animator.SetBool("isShooting", true);
        currentAmmo--;
        animator.SetBool("isShooting", false);
        RaycastHit hit;
		if(Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out hit, range))
		{
			

            TargetBehaviour target = hit.transform.GetComponent<TargetBehaviour>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody)
            {
                hit.rigidbody.AddForce(-hit.normal * force);
            }

            Instantiate(impactEffect,hit.point,Quaternion.LookRotation(hit.normal));
		}
	}
}
