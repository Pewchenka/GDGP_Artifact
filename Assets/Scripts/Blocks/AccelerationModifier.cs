using UnityEngine;

public class AccelerationModifier : ModifierBlock
{
    private float accelerationMultiplier = 2f;


    //Encapsulated property to access from other classes
    public float Multiplier => accelerationMultiplier;

    //Constructor
    public AccelerationModifier(float multiplier)
    {
        accelerationMultiplier = multiplier;
    }

    // Override method to apply the acceleration modifier to the projectile
    public override void ApplyMod(Projectile projectile)
    {
        projectile.MultiplySpeed(accelerationMultiplier);
    }
}
