using UnityEngine;

public abstract class ModifierBlock : SpellBlock
{
    //Abstract bool to apply the modifier to the projectile and allow to continue the chain of modifiers
    public abstract bool ApplyMod(Projectile projectile);
}
