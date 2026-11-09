using UnityEngine;

public class SplitTwice_Modifier : ModifierBlock
{
    private float splitDelay = 0.5f;

    private int amount = 2;
    // 180 / 2 = 90 --> split up and down
    private float angle = 180f;

    public override bool ApplyMod(Projectile projectile)
    {
        //delete current projectile and spawn 2 new projectiles up and down
        projectile.BeginSplit(splitDelay, amount, angle);
        return false;
    }

    public override SpellBlockId Id => SpellBlockId.Split;
}
