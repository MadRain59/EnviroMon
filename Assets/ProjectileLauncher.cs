using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform spawnPoint;

    //public variable for projectile
    public void FireProjectile()
    {
        //Instantiate, call projectile and transform position of projectile while calling upon original position (spawn point)
        //and rotation of the projectile
        Instantiate(projectilePrefab, spawnPoint.position, projectilePrefab.transform.rotation);
    }

}
