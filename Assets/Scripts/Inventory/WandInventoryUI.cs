using TMPro;
using UnityEngine;

public class WandInventoryUI : MonoBehaviour
{
    [SerializeField] private Wand wand;
    /*[SerializeField] private TMP_Text splitCountText;

    private void Start()
    {
        RefreshSplitCount();
    }

    private void RefreshSplitCount()
    {
        int count = wand.GetAvailableBlockCount(SpellBlockId.Split);
        splitCountText.text = $"Split Blocks Available: {count}";
    }

    public void RemoveTestBlock()
    {
        if (wand.TryRemoveBlock(3))
        {
            Debug.Log("Removed block from slot 3.");
            RefreshSplitCount();
        }
        else
        {
            Debug.Log("Failed to remove block from slot 3.");

        }
    }

    public void InstallTestBlock()
    {
        if (wand.TryInstallBlock(3, SpellBlockId.Split))
        {
            RefreshSplitCount();
        }
    }
    */

    // Inventory selection logic
    private SpellBlockId? selectedBlockId;

    public void SelectBlock(SpellBlockId blockId)
    {
        selectedBlockId = blockId;
        Debug.Log($"Selected block: {blockId}");
    }
}
