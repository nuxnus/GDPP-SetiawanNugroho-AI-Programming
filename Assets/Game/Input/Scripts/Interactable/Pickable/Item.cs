using UnityEngine;

public class Item : MonoBehaviour,IPickable,IInteractable
{
    // Membuat variable untuk menentukan data item
    [SerializeField]
    private ItemData _itemData;
 
    // Wajib membuat property Name
    // Property Name diisikan nilai variable name dari item data
    public string Name => _itemData.Name;
 
    // Wajib membuat function abstract Interact
    public void Interact()
    {
        // Ketika interact dengan item, item akan diambil
        Pickup();
    }
 
    public void Pickup()
    {
        // Membuat kode program ketika item diambil
    }
}
