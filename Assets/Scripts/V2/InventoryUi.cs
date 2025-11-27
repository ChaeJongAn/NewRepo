using UnityEngine;
using UnityEngine.UI;

public class InventoryUi : MonoBehaviour
{
    public Inventory inventory;

    public Image knifeIcon;
    public Text knifeText;

    public Image frogIcon;
    public Text frogText;

    // public Image lanternIcon;
    // public Image ghostSwordIcon;

    void Update()
    {
        if (inventory == null) return;

        knifeText.text = inventory.knifeCount.ToString();
        frogText.text = inventory.frogCount.ToString();

        // lanternIcon.gameObject.SetActive(inventory.hasLantern);
        // ghostSwordIcon.gameObject.SetActive(inventory.hasGhostSword);
    }
}