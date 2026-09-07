using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] 
    private InputManager _inputManager; 
    
    private int _score; 
    
    private void OnEnable() 
    { 
        // Menambahkan function AddScore menjadi listener dari event OnSpaceInput 
        _inputManager.OnSpaceInput.AddListener(AddScore); 
    } 
    
    private void OnDisable() 
    { 
        // Menghapus function AddScore dari listener event OnSpaceInput 
        _inputManager.OnSpaceInput.RemoveListener(AddScore); 
    } 
    
    public void AddScore(int value) 
    { 
        _score = _score + value; 
    }
}
