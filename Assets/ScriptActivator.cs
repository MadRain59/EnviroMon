using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptActivator : MonoBehaviour
{
    public GameObject BossEnemy;
    public GameObject hexBotActivateUI;

    private Damageable BossDamage;
    // Start is called before the first frame update
    void Start()
    {
        BossDamage = BossEnemy.GetComponent<Damageable>();
    }

    // Update is called once per frame
    void Update()
    {

        if (BossDamage.Health <= 0)
        {
            Dialouge();
        }
    }

    private void Dialouge()
    {
        hexBotActivateUI.SetActive(true);

        Invoke("DialogueDeactivate", 3.2f);
    }

    private void DialogueDeactivate()
    {
        Destroy(hexBotActivateUI);
    }
}
