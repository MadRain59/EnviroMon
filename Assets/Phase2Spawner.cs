using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2Spawner : MonoBehaviour
{
    public GameObject BossPhase1;
    public GameObject BossPhase2;

    private Damageable BossPhase1Damageable;

    // Start is called before the first frame update
    void Start()
    {
        BossPhase1Damageable = BossPhase1.GetComponent<Damageable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (BossPhase1Damageable.Health <= 0)
        {
            // Set the X position of BossPhase2 to the last X position of BossPhase1
            Vector3 bossPhase2SpawnPosition = new Vector3(BossPhase1.transform.position.x, -18f,BossPhase1.transform.position.z);

            // Set the position of BossPhase2
            BossPhase2.transform.position = bossPhase2SpawnPosition;


            Invoke("ActivateBossPhase2", 1f);
        }
    }
    void ActivateBossPhase2()
    {
        BossPhase2.SetActive(true);
    }

}
