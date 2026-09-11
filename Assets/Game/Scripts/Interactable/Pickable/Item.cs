using UnityEngine;
using UnityEngine.Events;
public class Item : MonoBehaviour,IInteractable, IPickable
{
    // Membuat variable untuk menentukan data item
    [SerializeField]
    public ItemData _itemData;
    // Membuat event untuk memberi tahu module lain 
    // jika item telah diambil
    public UnityEvent OnItemPicked;
    // Wajib membuat property Name
    // Property Name diisikan nilai variable name dari item data
    public string Name => _itemData.Name;
 
    // Wajib membuat function abstract Interact
    // Membuat context menu supaya function inetract bisa
    // dipanggil melalui menu component Item di inspector
    [ContextMenu("Interact Item")]
    public void Interact(PlayerCharacter character)
    {
        // Ketika interact dengan item, item akan diambil
        Pickup(character);
    }
 
    public void Pickup(PlayerCharacter character)
    {
        // Membuat variable salinan data dari variable _data
        ItemData newData = new ItemData(_itemData.ID, _itemData.Name);
        // Menambahkan salinan data ke list di inventory
        // menggunakan reference PlayerCharacter
        character.Inventory.AddItems(newData);
        // Memanggil event ketika item diambil
        OnItemPicked?.Invoke();
        // Menghapus item ketika item diambil
        Destroy(gameObject);
    }
}
