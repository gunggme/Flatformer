using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject projectilePrefab;

    public int projectileIdxNum;
    
    public void FireProjectile()
    {
        GameObject projectile = GameManager.instance.poolManager.Get(projectileIdxNum);
        Projectile projecLogic = projectile.GetComponent<Projectile>();
        projecLogic.pro = this.gameObject;
        projectile.transform.position = launchPoint.transform.position;
        Vector3 origScale = projectile.transform.localScale;
        projectile.transform.localScale = new Vector3(
            Mathf.Abs(origScale.x) * transform.localScale.x > 0 ? 1 : -1,
            origScale.y,
            origScale.z
        );
        projecLogic.Fire();
        
    }
}
