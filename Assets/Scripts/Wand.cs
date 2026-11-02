using UnityEngine;
using System.Collections.Generic;

public class Wand : MonoBehaviour
{
    private int size;
    private SpellBlock[] blocks;
    private int currentBlockIndex;

    //temporary
    [SerializeField] private GameObject projectilePrefab;


    private void Awake()
    {
        size = 6;
        blocks = new SpellBlock[size];
        currentBlockIndex = 0;

        //temporary to test the spell system
        blocks[0] = new SplitTwice_Modifier();
        blocks[1] = new AccelerationModifier(2f);
        blocks[2] = new ProjectileBlock(projectilePrefab, 5f);
        blocks[3] = new AccelerationModifier(2f);
        blocks[4] = new SplitTwice_Modifier();
        blocks[5] = new ProjectileBlock(projectilePrefab, 5f);

    }

    //Test method (right click on the scirpt component in the inspector)
    [ContextMenu("Use Spell")]
    private void UseSpell()
    {
        // List to hold modifier to apply to the projectile
        List<ModifierBlock> modifiers = new List<ModifierBlock>();

        // skip empty blocks and find the next available spell block
        for (int i = 0; i < size; i++)
        {
            SpellBlock block = blocks[currentBlockIndex];

            currentBlockIndex = (currentBlockIndex + 1) % size; // Move to the next block circulary

            // Collecting modifiers
            if (block is ModifierBlock modifier)
            {
                modifiers.Add(modifier);
                continue;
            }

            if (block is ProjectileBlock projectileBlock)
            {
                Debug.Log($"Modifiers collected: {modifiers.Count}");

                GameObject projectile = Instantiate(projectileBlock.Prefab, transform.position, transform.rotation);
                Projectile projectileComponent = projectile.GetComponent<Projectile>();
                projectileComponent.Initialize(projectileBlock.Speed, projectileBlock.Prefab);

                // Apply all collected modifiers to the projectile
                projectileComponent.ApplyModifiers(modifiers);

                return;
            }

        }
    }
}
