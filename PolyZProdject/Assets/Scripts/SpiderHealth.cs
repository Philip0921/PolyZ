using UnityEngine;
using System.Collections;

public class SpiderHealth : MonoBehaviour
{
    public int spiderHealth = 15;
    public GameObject TheSpider;


    void Update()
    {
        if (spiderHealth <= 0)
        {
            this.GetComponent<SpiderFollow>().enabled = false;
            TheSpider.GetComponent<Animation>().Play("die");
            StartCoroutine(KillSpider());
        }   
    }


    public void TakeDamage(int DamageAmount)
    {
        spiderHealth -= DamageAmount;
    }

    IEnumerator KillSpider()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
