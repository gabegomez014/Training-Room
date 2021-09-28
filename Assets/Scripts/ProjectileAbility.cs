using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/ProjectileAbility")]
public class ProjectileAbility : Ability
{

    public float projectileForce = 500f;
    public GameObject projectile;
    public GameObject emissionVFX;

    private ProjectileShooter launcher;

    public override void Initialize(GameObject obj)
    {
        launcher = obj.GetComponent<ProjectileShooter>();
    }

    public override void TriggerAbility()
    {
        launcher.Launch(emissionVFX, projectile, projectileForce);
    }

}
