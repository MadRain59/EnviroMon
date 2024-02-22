using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDeadTrigger : MonoBehaviour
{
    
    public bool bossIsDead = false;
    public GameObject Boss;
    Damageable damageable;
    // Start is called before the first frame update
    void Start()
    {
        damageable = Boss.GetComponent<Damageable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (damageable.Health <= 0)
        {
            Invoke("LoadNextScene", 5f);
        }
    }
    void LoadNextScene()
    {
        // Load the next scene after the delay
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
