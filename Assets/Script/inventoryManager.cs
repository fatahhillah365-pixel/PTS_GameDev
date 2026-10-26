
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class inventoryManager : MonoBehaviour, GrabableItem
{
    private InputAction inventoryAction;

    [SerializeField] private GameObject InventoryCanvas;
    [SerializeField] private int slotCount = 16;

    private bool InventoryActive = false;

    public itemSlot[] itemSlot;
    //cek ItemDetailPanel
    [SerializeField] private ItemDetailsPanel detailsPanel;

    void Start()
    {
        // Periksa InventoryCanvas
        if (InventoryCanvas == null)
        {
            Debug.LogError("InventoryCanvas belum dihubungkan di Inspector!", this);
            enabled = false;
            return;
        }

        // Ambil Input Action Inventory
        inventoryAction = InputSystem.actions.FindAction("inventory");

        if (inventoryAction == null)
        {
            Debug.LogError("Action 'Inventory' tidak ditemukan di Input Actions!");
            enabled = false;
            return;
        }

        // Buat slot inventory
        CreateSlots();

        // Sembunyikan inventory saat game dimulai
        InventoryCanvas.SetActive(false);
        InventoryActive = false;
    }

    void Update()
    {

        if (detailsPanel == null)
        {
            Debug.LogWarning("Tidak Ada detail panel", this);
        }

        if (inventoryAction == null || InventoryCanvas == null)
            return;

        if (inventoryAction.triggered)
        {
            InventoryActive = !InventoryActive;

            InventoryCanvas.SetActive(InventoryActive);

            // Pause dan lanjutkan game
            Time.timeScale = InventoryActive ? 0f : 1f;

            Debug.Log(InventoryActive
                ? "Inventory dibuka"
                : "Inventory ditutup");

            ShowFirstItem();

        }
    }


    private void CreateSlots()
    {
        // Ambil prefab dari folder Resources
        itemSlot slotPrefab = Resources.Load<itemSlot>("item (1)");

        if (slotPrefab == null)
        {
            Debug.LogError(
                "Prefab itemSlot tidak ditemukan. Periksa Assets/Resources/itemSlot.prefab",
                this
            );
            itemSlot = new itemSlot[0];
            return;
        }

        // Cari GridLayoutGroup di InventoryCanvas
        GridLayoutGroup grid =
            InventoryCanvas.GetComponentInChildren<GridLayoutGroup>(true);

        if (grid == null)
        {
            Debug.LogError(
                "GridLayoutGroup tidak ditemukan di InventoryCanvas!",
                this
            );
            itemSlot = new itemSlot[0];
            return;
        }

        // Buat slot inventory
        itemSlot = new itemSlot[slotCount];

        for (int i = 0; i < slotCount; i++)
        {
            itemSlot[i] = Instantiate(slotPrefab, grid.transform);
        }

        Debug.Log("Berhasil membuat " + itemSlot.Length + " slot inventory.");
    }


    public void AddItem(ItemDetails item)
    {
        if (item == null)
        {
            Debug.LogWarning("ItemDetails belum diisi pada item yang diambil!");
            return;
        }

        Debug.Log(
            "Item added to inventory: " +
            item.ItemName +
            " Quantity: " +
            item.Quantity
        );

        if (itemSlot == null || itemSlot.Length == 0)
        {
            Debug.LogWarning("Slot inventory belum berhasil dibuat!");
            return;
        }

        // for (int i = 0; i < itemSlot.Length; i++)
        // {
        //     if (itemSlot[i] == null)
        //         continue;

        //     if (!itemSlot[i].isFull)
        //     {
        //         itemSlot[i].AddItem(item);
        //         return;
        //     }
        // }

        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].CanStackWith(item.ItemName))
            {
                itemSlot[i].AddQuantity(item.Quantity);
                return;
            }
        }

        for (int i = 0; i < itemSlot.Length; i++)
        {
            // if(!itemSlot[i].isFull)
            //chek kalau kosong
            if (itemSlot[i].IsEmpty)
            {
                itemSlot[i].AddItem(item);
                return;
            }
        }

        Debug.LogWarning(
            "Inventory penuh, item '" + item.ItemName + "' tidak dapat ditambahkan."
        );
    }
    public void SelectSlot(itemSlot slot)
    {
        DeselectAllSlots();

        slot.SelectedPanel.SetActive(true);
        slot.isItemSelected = true;

        if(detailsPanel != null)
        {
            detailsPanel.show(slot.itemDetails, slot.itemQuantity);
        }
        
    }

    public void DeselectAllSlots()
    {
        foreach (itemSlot slot in itemSlot)
        {
            slot.isItemSelected = false;
            slot.SelectedPanel.SetActive(false);
        }
    }
    private void ShowFirstItem()
    {
        if (itemSlot != null)
        {
            foreach (itemSlot slot in itemSlot)
            {
                if (!slot.IsEmpty)
                {
                    SelectSlot(slot);
                    return;
                }
            }
        }

        if (detailsPanel != null)
        {
            detailsPanel.Clear();
        }
    }


}
