using UnityEngine;

public class ProjectileBlock : SpellBlock
{
    private GameObject projectilePrefab;
    private float projectileSpeed;


    //Incapsulated properties to access from other classes
    public GameObject Prefab => projectilePrefab;
    public float Speed => projectileSpeed;

    //Constructor to initialize the projectile block with a prefab and speed
    //Example: ProjectileBlock block = new ProjectileBlock(fireballPrefab, 10f)
    public ProjectileBlock(GameObject prefab, float speed)
    {
        projectilePrefab = prefab;
        projectileSpeed = speed;
    }
}
