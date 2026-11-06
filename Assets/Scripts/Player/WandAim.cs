using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;


public class WandAim : MonoBehaviour
{
    [SerializeField] private Camera aimCamera;

    [SerializeField] private Wand wand;

    private void LateUpdate()
    {
        // Update aim direction based on camera
        Vector2 mouseScreen = Mouse.current.position.ReadValue();
        float depth = transform.position.z - aimCamera.transform.position.z;

        Vector3 mouseWorld = aimCamera.ScreenToWorldPoint(new Vector3(mouseScreen.x, mouseScreen.y, depth));


        Vector2 aimDirection = mouseWorld - transform.position;

        if (aimDirection.sqrMagnitude > 0.000001f)
        {
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        bool pointerOverUI = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(Mouse.current.deviceId);

        if (Mouse.current.leftButton.wasPressedThisFrame && !pointerOverUI)
        {
            wand.UseSpell();
        }
    }
}
