using UnityEngine;
using System.Collections.Generic;

public class SpellInventory
{
    private Dictionary<SpellBlockId, int> availableBlocks = new Dictionary<SpellBlockId, int>
    {
        { SpellBlockId.BasicProjectile, 2 },
        { SpellBlockId.Acceleration, 2 },
        { SpellBlockId.Split, 2 }
    };

    public bool TryTake(SpellBlockId blockId)
    {
        if (availableBlocks.ContainsKey(blockId) && availableBlocks[blockId] > 0)
        {
            availableBlocks[blockId]--;
            return true;
        }
        return false;
    }

    public void Return(SpellBlockId blockId)
    {
        if (availableBlocks.ContainsKey(blockId))
        {
            availableBlocks[blockId]++;
        }
    }
}
