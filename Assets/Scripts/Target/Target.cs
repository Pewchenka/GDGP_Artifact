using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    private bool isHit;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color hitColor;
    private Color originalColor;

    private void Awake()
    {
        isHit = false;
        originalColor = spriteRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isHit) return;

        if (other.GetComponent<Projectile>() != null)
        {
            isHit = true;
            spriteRenderer.color = hitColor;
            Debug.Log("Target hit!");
        }
    }

    [ContextMenu("Reset Target")]
    public void ResetTarget()
    {
        isHit = false;
        spriteRenderer.color = originalColor;
    }
}
