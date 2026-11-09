using UnityEngine;

public class InventoryBlockButtons : MonoBehaviour
{
    [SerializeField] private WandInventoryUI inventoryUI;
    [SerializeField] private SpellBlockId blockId;

    public void Select()
    {
        inventoryUI.SelectBlock(blockId);
    }
}
