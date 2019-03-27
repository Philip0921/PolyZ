using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    public int EnemyHealth = 10;
    public GameObject TheZombie;


    void Update()
    {
        if (EnemyHealth <= 0)
        {
            this.GetComponent<EnemyFollow>().enabled = false;
            TheZombie.GetComponent<Animation>().Play("Dying");
            StartCoroutine(KillZombie());
        }   
    }


    public void TakeDamage(int DamageAmount)
    {
        EnemyHealth -= DamageAmount;
    }

    IEnumerator KillZombie()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
