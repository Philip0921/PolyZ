using UnityEngine;
using System.Collections;

public class SpiderFollow : MonoBehaviour
{
    public GameObject ThePlayer;
    public float targetDistance;
    public float allowedRange = 10f;
    public GameObject TheEnemy;
    public float enemySpeed;
    public int attackTrigger;
    public RaycastHit shoot;

    public int IsAttacking;
    public GameObject ScreenFlash;
    public AudioSource Hurt01;
    public AudioSource Hurt02;
    public AudioSource Hurt03;
    public int PainSound;

    void Update()
    {
        transform.LookAt(ThePlayer.transform);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out shoot))
        {
            targetDistance = shoot.distance;

            if (targetDistance < allowedRange)
            {
                enemySpeed = 0.02f;

                if (attackTrigger == 0)
                {
                    TheEnemy.GetComponent<Animation>().Play("walk");
                    transform.position = Vector3.MoveTowards(transform.position, ThePlayer.transform.position, enemySpeed);
                }
            }

            else
            {
                enemySpeed = 0;
                TheEnemy.GetComponent<Animation>().Play("idle");
            }

        }

        if (attackTrigger == 1)
        {
            if (IsAttacking == 0)
            {
                StartCoroutine(EnemyDamage());
            }
            enemySpeed = 0;
            TheEnemy.GetComponent<Animation>().Play("attack");
        }
    }

    void OnTriggerEnter()
    {
        attackTrigger = 1;
    }

    void OnTriggerExit()
    {
        attackTrigger = 0;
    }

    IEnumerator EnemyDamage()
    {
        IsAttacking = 1;
        PainSound = Random.Range(1, 4);
        yield return new WaitForSeconds(0.4f);
        ScreenFlash.SetActive(true);
        GlobalHealth.PlayerHealth -= 10;
        if (PainSound == 1)
        {
            Hurt01.Play();
        }
        if (PainSound == 2)
        {
            Hurt02.Play();
        }
        if (PainSound == 3)
        {
            Hurt03.Play();
        }
        yield return new WaitForSeconds(0.5f);
        ScreenFlash.SetActive(false);
        yield return new WaitForSeconds(0.75f);
        IsAttacking = 0;
    }
}
