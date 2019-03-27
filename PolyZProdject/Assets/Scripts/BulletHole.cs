using UnityEngine;

public class BulletHole : MonoBehaviour
{
    RaycastHit hit;
    public GameObject bulletHoleEffect;

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {


            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
            {
                GameObject BulletHole = Instantiate(bulletHoleEffect, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                Destroy(BulletHole, 5f);

            }
        }
    }
}
