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
        GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, projectilePrefab.transform.rotation);
        Vector3 origScale = projectile.transform.localScale;

        projectile.transform.localScale = new Vector3
            (
            origScale.x * transform.localScale.x > 0 ? 1 : -1,
            origScale.y,
            origScale.z
            );
    }

}
