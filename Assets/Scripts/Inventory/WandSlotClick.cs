using UnityEngine;
using UnityEngine.EventSystems;

public class WandSlotClick : MonoBehaviour, IPointerClickHandler
{
    private WandInventoryUI inventoryUI;
    private int slotIndex;

    public void Setup(WandInventoryUI ui, int index)
    {
        inventoryUI = ui;
        slotIndex = index;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            inventoryUI.RemoveFromSlot(slotIndex);
        }
    }
}