using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    // Membuat variable untuk menentukan nilai Property Name
    [SerializeField]
    private string _name;
    // Wajib membuat property Name
    // Property Name diisikan nilai variable _name
    public string Name => _name;
 
    // Wajib membuat function abstract Interact
    public void Interact()
    {
        // Membuka atau menutup pintu
    }
}
