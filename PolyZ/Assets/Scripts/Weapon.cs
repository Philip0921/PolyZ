using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    public bool isReloading = false;
    public float fireRate;
    float nextFire = 0f;
    private float reloadTime = 3f;
    public int ammo;
    public int maxammo = 10;
    public Text ammoText;
    public int ammoClip = 55;
    public Text ammoClipText;
    public Text reloadText;
    public Slider reloadSlider;

    void Start()
    {
        updateammo();
        reloadSlider.gameObject.SetActive(false);
        reloadText.enabled = false;
    }

    void Update()
    {

        if (isReloading)
        {
            reloadSlider.gameObject.SetActive(true);
            reloadSlider.value -= Time.deltaTime;
            return;
        }
        else
        {
            reloadSlider.gameObject.SetActive(false);
            reloadSlider.value = 3f;
        }

        //When the player presses down the leftmb, the gun fires.
        if (Input.GetButton("Fire1") && Time.time >= nextFire && ammo > 0 && isReloading == false)
        {
            nextFire = Time.time + 1f / fireRate;
            Shoot();

        }

        //When the player presses R the player reloads.
        if (Input.GetKeyDown(KeyCode.R) && ammoClip > 0)
        {
            StartCoroutine(Reload());
            return;
        }

    }

    //Adds ammo when reload.
    public void addammo(int moreammo, int loseammo)
    {
        ammo += moreammo;
        ammo -= loseammo;

        updateammo();

    }
    //Shows your ammo and clips ingame.
    void updateammo()
    {
        ammoText.text = ammo.ToString();
        ammoClipText.text = ammoClip.ToString();
    }


    IEnumerator Reload()
    {
        isReloading = true;
        reloadText.enabled = true;
        reloadText.text = "Reloading...";

        yield return new WaitForSeconds(reloadTime);

        ammoClip += ammo;
        ammo = 0;
        ammoClip -= 10;
        addammo(10, 0);

        if (ammo == maxammo)
        {
            isReloading = false;
            reloadText.enabled = false;
        }

        if (ammoClip < 0)
        {
            ammoClip = 0;
        }



    }

    void Shoot()
    {
        muzzleFlash.Play();

        addammo(0, 1);

        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

            Destroy(impactGO, 2f);
        }
    }
}
