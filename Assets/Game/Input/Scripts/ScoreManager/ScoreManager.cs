using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] 
    private InputManager _inputManager; 
    
    private int _score; 
    
    
    public void AddScore(int value) 
    { 
        _score = _score + value; 
    }
}
