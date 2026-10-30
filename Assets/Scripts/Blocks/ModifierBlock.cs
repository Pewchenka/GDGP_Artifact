using UnityEngine;

public abstract class ModifierBlock : SpellBlock
{
    //Abstract method to apply the modifier to the projectile
    public abstract void ApplyMod(Projectile projectile);
}
