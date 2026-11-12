using UnityEngine;
using System.Collections.Generic;

public class Wand : MonoBehaviour
{
    private int size;
    private SpellBlock[] blocks;
    private int currentBlockIndex;

    // Shoot from wand
    [SerializeField] private Transform shootPoint;

    //temporary
    [SerializeField] private GameObject projectilePrefab;

    //Inventory test
    private readonly SpellInventory spellInventory = new SpellInventory();
    public int SlotCount => blocks.Length;

    private SpellBlock CreateBlock(SpellBlockId blockId)
    {
        switch (blockId)
        {
            case SpellBlockId.BasicProjectile:
                return new ProjectileBlock(projectilePrefab, 5f);
            case SpellBlockId.Acceleration:
                return new AccelerationModifier(2f);
            case SpellBlockId.Split:
                return new SplitTwice_Modifier();
            default:
                throw new System.ArgumentException($"Unknown SpellBlockId: {blockId}");
        }
    }

    // Try to remove a block from the specified slot index
    public bool TryRemoveBlock(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= size)
        {
            Debug.LogError("Invalid slot index.");
            return false;
        }
        if (blocks[slotIndex] == null)
        {
            return false;
        }
        spellInventory.Return(blocks[slotIndex].Id);
        blocks[slotIndex] = null;
        return true;
    }

    public bool TryInstallBlock(int slotIndex, SpellBlockId blockId)
    {
        if (slotIndex < 0 || slotIndex >= size)
        {
            Debug.LogError("Invalid slot index.");
            return false;
        }
        if (blocks[slotIndex] != null)
        {
            return false;
        }
        if (spellInventory.TryTake(blockId))
        {
            blocks[slotIndex] = CreateBlock(blockId);
            return true;
        }
        else
        {
            Debug.LogWarning($"No available blocks of type {blockId} to install.");
            return false;
        }
    }

    public int GetAvailableBlockCount(SpellBlockId blockId)
    {
        return spellInventory.GetAvailableCount(blockId);
    }

    public SpellBlockId? GetBlockIdAt(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= blocks.Length)
        {
            return null;
        }

        return blocks[slotIndex]?.Id;
    }

    private void Awake()
    {
        size = 6;
        blocks = new SpellBlock[size];
        currentBlockIndex = 0;

        TryInstallBlock(2, SpellBlockId.BasicProjectile);

        //temporary to test the spell system
        /*
        Debug.Log(TryInstallBlock(1, SpellBlockId.BasicProjectile)); // true
        Debug.Log(TryInstallBlock(0, SpellBlockId.Split));            // true
        Debug.Log(TryInstallBlock(0, SpellBlockId.Split));            // false
        Debug.Log(TryInstallBlock(2, SpellBlockId.Split));            // true
        Debug.Log(TryInstallBlock(3, SpellBlockId.Split));            // false 

        Debug.Log(TryRemoveBlock(0));                     // true
        Debug.Log(TryRemoveBlock(0));                     // false
        Debug.Log(TryInstallBlock(3, SpellBlockId.Split)); // true

        Debug.Log(GetAvailableBlockCount(SpellBlockId.Split));
        Debug.Log(GetAvailableBlockCount(SpellBlockId.Acceleration));

        Debug.Log(GetBlockIdAt(0)); // null
        Debug.Log(GetBlockIdAt(1)); // BasicProjectile
        */
    }

    //Test method (right click on the scirpt component in the inspector)
    //[ContextMenu("Use Spell")]
    public void UseSpell()
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

                GameObject projectile = Instantiate(projectileBlock.Prefab, shootPoint.position, shootPoint.rotation);
                Projectile projectileComponent = projectile.GetComponent<Projectile>();
                projectileComponent.Initialize(projectileBlock.Speed, projectileBlock.Prefab);

                // Apply all collected modifiers to the projectile
                projectileComponent.ApplyModifiers(modifiers);

                return;
            }

        }
    }
}
