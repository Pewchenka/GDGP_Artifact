using UnityEngine;

public class Wand : MonoBehaviour
{
    private int size;
    private SpellBlock[] blocks;
    private int currentBlockIndex;

    //temporary
    [SerializeField] private GameObject projectilePrefab;


    private void Awake()
    {
        size = 4;
        blocks = new SpellBlock[size];
        currentBlockIndex = 0;

        //temporary
        blocks[0] = new ProjectileBlock(projectilePrefab, 10f);
        blocks[2] = new ProjectileBlock(projectilePrefab, 3f);
    }

    //Test method (right click on the scirpt component in the inspector)
    [ContextMenu("Use Spell")]
    private void UseSpell()
    {
        // skip empty blocks and find the next available spell block
        for (int i = 0; i < size; i++)
        {
            SpellBlock block = blocks[currentBlockIndex];

            currentBlockIndex = (currentBlockIndex + 1) % size; // Move to the next block circulary


            if (block is ProjectileBlock projectileBlock)
            {
                GameObject projectile = Instantiate(projectileBlock.Prefab, transform.position, transform.rotation);
                projectile.GetComponent<Projectile>().Initialize(projectileBlock.Speed);
                return;
            }

        }
    }
}
