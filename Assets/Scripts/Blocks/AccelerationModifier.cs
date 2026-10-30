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
}
