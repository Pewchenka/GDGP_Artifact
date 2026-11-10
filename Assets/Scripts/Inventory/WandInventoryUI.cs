using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WandInventoryUI : MonoBehaviour
{
    [SerializeField] private Wand wand;
    private TMP_Text[] slotLabels;
    [SerializeField] private Button slotButtonPrefab;
    [SerializeField] private Transform slotsContainer;

    [SerializeField] private GameObject inventoryPanel;

    [SerializeField] private InventoryBlockButtons inventoryButtonPrefab;
    [SerializeField] private Transform inventoryContent;

    private void Start()
    {
        BuildSlots();
        BuildInventory();
        RefreshSlots();
    }

    // Inventory selection logic
    private SpellBlockId? selectedBlockId;

    public void SelectBlock(SpellBlockId blockId)
    {
        selectedBlockId = blockId;
        Debug.Log($"Selected block: {blockId}");
    }

    // For all wand's slots build UI slots
    private void BuildSlots()
    {
        slotLabels = new TMP_Text[wand.SlotCount];

        for (int i = 0; i < slotLabels.Length; i++)
        {
            int slotIndex = i;
            Button button = Instantiate(slotButtonPrefab, slotsContainer);

            button.GetComponent<WandSlotClick>().Setup(this, slotIndex);

            slotLabels[i] = button.GetComponentInChildren<TMP_Text>(true);
            button.onClick.AddListener(() => ClickSlot(slotIndex));
        }
    }

    private void BuildInventory()
    {
        foreach (SpellBlockId id in System.Enum.GetValues(typeof(SpellBlockId)))
        {
            InventoryBlockButtons button = Instantiate(inventoryButtonPrefab, inventoryContent);

            button.Setup(this, id, wand.GetAvailableBlockCount(id));
        }
    }

    public void ClickSlot(int slotIndex)
    {
        if (!selectedBlockId.HasValue)
        {
            return;
        }

        SpellBlockId blockId = selectedBlockId.Value;

        if (wand.TryInstallBlock(slotIndex, blockId))
        {
            Debug.Log($"Installed {blockId} in slot {slotIndex + 1}");
            RefreshSlots();
        }
    }

    private void RefreshSlots()
    {
        for (int i = 0; i < slotLabels.Length; i++)
        {
            SpellBlockId? blockId = wand.GetBlockIdAt(i);
            slotLabels[i].text = $"{i + 1}: {blockId?.ToString() ?? "Empty"}";
        }

        for (int i = 0; i < inventoryContent.childCount; i++)
        {
            InventoryBlockButtons button = inventoryContent.GetChild(i).GetComponent<InventoryBlockButtons>();
            button.Setup(this, button.GetComponent<InventoryBlockButtons>().BlockId, wand.GetAvailableBlockCount(button.GetComponent<InventoryBlockButtons>().BlockId));
        }
    }
    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    // Remove block from wand's slot
    public void RemoveFromSlot(int slotIndex)
    {
        if (wand.TryRemoveBlock(slotIndex))
        {
            RefreshSlots();
        }
    }
}
