using TMPro;
using UnityEngine;

public class InventoryBlockButtons : MonoBehaviour
{
    private WandInventoryUI inventoryUI;
    private SpellBlockId blockId;

    public SpellBlockId BlockId { get => blockId; set => blockId = value; }

    public void Setup(WandInventoryUI ui, SpellBlockId id, int count)
    {
        inventoryUI = ui;
        BlockId = id;

        TMP_Text label = GetComponentInChildren<TMP_Text>(true);
        label.text = $"{id}: {count}";
    }
    public void Select()
    {
        inventoryUI.SelectBlock(BlockId);
    }
}
