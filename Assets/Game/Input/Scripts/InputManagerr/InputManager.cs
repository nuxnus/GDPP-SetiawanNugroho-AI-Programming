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
    
    //membuat event onmoveinput
    public UnityEvent<Vector2> OnMoveInput;
    // Membuat event OnSprintInput 
    public UnityEvent<bool> OnSprintInput; 
    private IPlayerActions _playerActionsImplementation;

    private void Update() 
    { 
        
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
        OnMoveInput.Invoke(context.ReadValue<Vector2>());
    }
    public void OnSprint(InputAction.CallbackContext context) 
    { 
        // Mengecek apakah input ditekan 
        if (context.performed) 
        { 
            // Jika input ditekan maka trigger event OnSprintInput 
            // Mengirim data true 
            OnSprintInput?.Invoke(true); 
        } 
        // Mengecek apakah input dilepas 
        if (context.canceled) 
        { 
            // Jika input dilepas maka trigger event OnSprintInput 
            // Mengirim data false 
            OnSprintInput?.Invoke(false); 
        } 
    }
}
