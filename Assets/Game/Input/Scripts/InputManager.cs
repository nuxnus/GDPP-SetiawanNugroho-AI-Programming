using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using static GameInputAction; 

// Implementasi interface IPlayerActions 
// untuk mendeteksi action input dari action map Player 
public class InputManager : MonoBehaviour, IPlayerActions 
{
    private GameInputAction _inputAction; 
    // Membuat event OnSpaceInput yang mengirim satu data integer  
    public UnityEvent<int> OnSpaceInput;
    private IPlayerActions _playerActionsImplementation;

    private void Update() 
    { 
        if (Input.GetKeyDown(KeyCode.Space)) 
        { 
            // Trigger event OnSpaceInput dan mengirim data bernilai 10  
            OnSpaceInput?.Invoke(10);
        } 
    }
    private void Awake() 
    { 
        // Membuat object GameInputAction dan menyimpan reference nya 
        // ke variable _inputAction 
        _inputAction = new GameInputAction(); 
        // Mengaktifkan input action 
        _inputAction.Enable(); 
        // Mengaktifkan action map Player 
        _inputAction.Player.Enable(); 
        // Memberi tahu bahwa kelas ini akan mendeteksi input dari 
        // action map Player 
        _inputAction.Player.SetCallbacks(this); 
    } 
    public void OnInteract(InputAction.CallbackContext context) 
    { 
        // Menulis code yang akan dieksekusi 
        // ketika tombol interact ditekan 
        // contect.performed digunakan untuk mengecek apakah input ditekan 
        if (context.performed) 
        { 
            // Memunculkan log interact di console  
            // ketika input interact ditekan 
            Debug.Log("Interact"); 
        } 
    } 
    public void OnMove(InputAction.CallbackContext context) 
    { 
        // Menulis code yang akan dieksekusi 
        // ketika tombol move ditekan 
        // context.ReadValue() digunakan untuk membaca nilai input 
        // dengan tipe vector, kemudian dimunculkan pada log di console 
        Debug.Log( context.ReadValue<Vector2>()); 
    }
    
}
