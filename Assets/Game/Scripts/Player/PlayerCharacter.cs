using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    // Variable untuk reference ke module PlayerCharacterMovement
    [SerializeField]
    private PlayerCharacterMovement _movement;
    // Variable untuk reference ke module PlayerCharacterStamina
    [SerializeField]
    private PlayerCharacterStamina _stamina;
    // Variable untuk reference ke module InventoryManager
    [SerializeField]
    private InventoryManager _inventory;
    // Variable untuk reference ke module InteractDetector
    [SerializeField]
    private InteractDetector _interactDetector;
    // Property untuk mengakses variable _interactDetector
    public InteractDetector InteractDetector => _interactDetector;
    
    // Property untuk mengakses variable _movement
    public PlayerCharacterMovement Movement => _movement;
    // Property untuk mengakses variable _stamina
    public PlayerCharacterStamina Stamina => _stamina;
    // Property untuk mengakses variable _inventory
    public InventoryManager Inventory => _inventory;
    
    private void Awake()
    {
        // Ketika game dijalankan,
        // cursor mouse akan disembunyikan
        Cursor.visible = false;
        // cursor mouse akan dikunci di tengah layar
        Cursor.lockState = CursorLockMode.Locked;
    }
}
