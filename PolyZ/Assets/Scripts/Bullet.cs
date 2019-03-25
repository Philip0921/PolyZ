using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Bullet script
    public float speed = 12;
    public float lifeTime = 1.2f;

    public string tagToKill;

    //Makes the bullet go forward in a surtain speed.
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        //When we reache 1.2 sek we destroy the bullet.

        Destroy(gameObject, lifeTime);

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == tagToKill)
        {
            Destroy(other.gameObject);
        }
    }
}
