using UnityEngine;
using System.Collections.Generic;

public class Projectile : MonoBehaviour
{
    private float speed;

    public void Initialize(float initialSpeed)
    {
        speed = initialSpeed;
    }

    private void Update()
    {
        // Move the projectile forward based on its speed
        transform.position = transform.position + transform.right * speed * Time.deltaTime;
    }

    // Method to change the speed of the projectile, can be called by modifiers
    public void MultiplySpeed(float multiplier)
    {
        speed *= multiplier;
    }


    public void ApplyModifiers(List<ModifierBlock> modifiers)
    {
        //Apply all collected modifiers to the projectile
        // Polymorphic
        //Temporary, before the splittingModifier
        for (int ammountMod = 0; ammountMod < modifiers.Count; ammountMod++)
        {
            modifiers[ammountMod].ApplyMod(this);
        }
    }

}
