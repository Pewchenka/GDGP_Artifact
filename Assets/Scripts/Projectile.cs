using UnityEngine;
using System.Collections.Generic;

public class Projectile : MonoBehaviour
{
    private float speed;

    // Split variables
    private bool isWatingToSplit;
    private float splitTimer;
    private GameObject sourcePrefab;
    private int amountOfProjectiles;
    private float splitAngle;

    [SerializeField, Min(0.1f)] private float lifetime = 3f;

    // Chain of modifiers
    private List<ModifierBlock> modifierChain;
    private int nextModifierIndex;

    public void Initialize(float initialSpeed, GameObject prefab)
    {
        speed = initialSpeed;
        sourcePrefab = prefab;
    }

    private void Update()
    {
        // Move the projectile forward based on its speed
        transform.position = transform.position + transform.right * speed * Time.deltaTime;

        // Handle splitting logic
        if (isWatingToSplit)
        {
            splitTimer -= Time.deltaTime;
            if (splitTimer <= 0f)
            {
                isWatingToSplit = false;
                Split();
            }
        }
    }

    private void Awake()
    {
        Destroy(gameObject, lifetime);
    }

    public void ApplyModifiers(List<ModifierBlock> modifiers)
    {
        modifierChain = modifiers;
        nextModifierIndex = 0;
        ExecuteModifiers();
    }

    private void ExecuteModifiers()
    {
        //Apply all collected modifiers to the projectile
        // Polymorphic

        while (nextModifierIndex < modifierChain.Count)
        {
            ModifierBlock curMod = modifierChain[nextModifierIndex];
            nextModifierIndex++;
            //curMod.ApplyMod(this);

            bool canContinue = curMod.ApplyMod(this);
            if (!canContinue)
            {
                return; // Stop applying further modifiers if the current one indicates to stop
            }
        }
    }


    // MODIFIERS

    // Method to change the speed of the projectile, can be called by modifiers
    public void MultiplySpeed(float multiplier)
    {
        speed *= multiplier;
    }

    // Split
    public void BeginSplit(float delay, int amount, float angle)
    {
        splitTimer = delay;
        isWatingToSplit = true;
        amountOfProjectiles = amount;
        splitAngle = angle;
    }

    private void Split()
    {
        // Get list of remaining modifiers after the current one
        List<ModifierBlock> remainingModifiers = modifierChain.GetRange(nextModifierIndex, modifierChain.Count - nextModifierIndex);

        for (int i = 0; i < amountOfProjectiles; i++)
        {
            // Calculate the angle for each new projectile
            float angleOffset = splitAngle * (i - (amountOfProjectiles - 1) / 2f);
            // Instantiate a new projectile
            GameObject newProjectile = Instantiate(sourcePrefab, transform.position, transform.rotation);
            newProjectile.transform.Rotate(0f, 0f, angleOffset); // Rotate the projectile to the desired angle
            // Initialize the new projectile with the same speed and source prefab
            Projectile newProjectileComponent = newProjectile.GetComponent<Projectile>();
            newProjectileComponent.Initialize(speed, sourcePrefab);
            // Apply remaining modifiers to the new projectile
            newProjectileComponent.ApplyModifiers(remainingModifiers);
        }
        Destroy(gameObject); // Destroy the original projectile after splitting
    }
}
