using UnityEngine;
using System.Collections.Generic;

public class Projectile : MonoBehaviour
{
    private float speed;

    // Chain of modifiers
    private List<ModifierBlock> modifierChain;
    private int nextModifierIndex;

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
        modifierChain = modifiers;
        nextModifierIndex = 0;

        //Apply all collected modifiers to the projectile
        // Polymorphic

        while (nextModifierIndex < modifierChain.Count)
        {
            ModifierBlock curMod = modifierChain[nextModifierIndex];
            nextModifierIndex++;
            curMod.ApplyMod(this);
        }

    }

}
