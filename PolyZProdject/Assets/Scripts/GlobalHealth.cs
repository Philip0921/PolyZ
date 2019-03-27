using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GlobalHealth : MonoBehaviour
{
    public static int PlayerHealth = 100;
    public int internalHealth;
    //public int regeneration = 5;
    public GameObject HealthDisplay;

    void Update()
    {
        internalHealth = PlayerHealth;
        HealthDisplay.GetComponent<Text>().text = "Health: " + PlayerHealth;

        if (PlayerHealth == 0)
        {
            SceneManager.LoadScene(1);
        }
        //if (PlayerHealth < 100)
        //{
        //    RegenerateHealth();
        //}
    }

    //void RegenerateHealth()
    //{
    //    PlayerHealth += regeneration;
    //}
}
