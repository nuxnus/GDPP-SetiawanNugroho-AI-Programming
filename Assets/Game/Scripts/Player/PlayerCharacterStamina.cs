using UnityEngine;

public class PlayerCharacterStamina : MonoBehaviour
{
    // Membuat variable untuk reference ke object module PlayerCharacterMovement 
    [SerializeField] 
    private PlayerCharacterMovement _characterMovement;
    // Membuat variable untuk menentukan maksimum stamina player 
    [SerializeField] 
    private float _maxStamina = 100; 
    // Membuat variable untuk menentukan jumlah stamina yang dibutuhkan untuk sprint 
    [SerializeField] 
    private float _sprintStaminaCost = 20; 
    // Membuat variable untuk menentukan nilai regenerasi stamina 
    [SerializeField] 
    private float _staminaRegenValue = 20; 
    // Membuat menghitung stamina yang dimiliki player character 
    private float _currentStamina; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Menghitung stamina terus menerus sepanjang game 
        CalculateStamina(); 
    }
    private void Awake() 
    { 
        // Di awal gae stamina player diset menjadi maksimum 
        _currentStamina = _maxStamina; 
    } 
    public void CalculateStamina() 
    { 
        // Mengecek apakah character sedang sprint atau tidak 
        if (_characterMovement.IsSprint) 
        { 
            // Jika sedang sprint, Mengecek apakah stamina masih ada 
            if (_currentStamina > 0) 
            { 
                // Jika sedang sprint dan stamina masih ada, stamina akan dikurangi. 
                // Dengan jumlah stamina yang dibutuhkan. 
                // Dikalikan dengan Time.deltaTime karena function ini akan dipanggil 
                // oleh function Update. 
                _currentStamina = _currentStamina - _sprintStaminaCost * Time.deltaTime; 
            } 
            else 
            { 
                // Jika sedang sprint dan stamina habis, maka player berhenti sprint. 
                _characterMovement.SetSprint(false); 
            } 
        } 
        else 
        { 
            // Jika character tidak sedang sprint maka tambahkan stamina dengan 
            // nilai regenerasi stamina. 
            // Dikalikan dengan Time.deltaTime karena function ini akan dipanggil 
            // oleh function Update. 
            _currentStamina = _currentStamina + _staminaRegenValue * Time.deltaTime; 
        } 
        // Membatasi stamina player, minimum: 0; maksimum: maximum stamina  
        _currentStamina = Mathf.Clamp(_currentStamina, 0, _maxStamina); 
    }
}
