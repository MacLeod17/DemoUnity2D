using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EngFlag : MonoBehaviour
{
    bool switchingScenes;

    public void Start()
    {
        switchingScenes = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(switchingScenes == false)
        {
            print(other.gameObject.tag);
            if (other.gameObject.tag == "Player")
            {
                 GameController.Instance.OnLoadGameScene((SceneManager.GetActiveScene().buildIndex + 1).ToString());
                switchingScenes = true;
            }
        }
    }
}
