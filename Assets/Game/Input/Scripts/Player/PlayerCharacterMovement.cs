using System;
using UnityEngine;
public class PlayerCharacterMovement : MonoBehaviour
{
    // Membuat variable untuk menyimpan data gerakkan arah gerakan character 
    private Vector3 _movementDirection;
    // Membuat variable untuk menentukan data kecepatan 
    // Secara default kecepatannya sebesar 1 
    private float _currentSpeed = 1f; 
    // Membuat variable untuk menyimpan arah dan kecepatan gerakkan character 
    private Vector3 _velocityXZ; 
    
    // Membuat variable untuk menyimpan reference ke component CharacterController 
    [SerializeField] 
    private CharacterController _characterController; 
    
    [SerializeField] 
    private float _gravityScale = 1; 
    private float _velocityY; 
    private bool _isGrounded; 
    private bool _isSprint; 
    
    // Membuat variable untuk menentukan kecepatan saat berjalan 
    [SerializeField] 
    private float _walkSpeed = 1; 
    // Membuat variable untuk menentukan kecepatan saat berlari 
    [SerializeField] 
    private float _sprintSpeed = 2; 
    
    // Membuat variable untuk menentukan besar acceleration 
    [SerializeField] 
    private float _acceleration = 0.5f; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Mengecek apakah character berada di ground,  
        // proses pengecekan dilakukan terus menerus 
        CheckIsGrounded(); 
        // Menghitung acceleration selama terus menerus 
        CalculateAcceleration();
        // Mengecek apakah velocity y perlu di-reset,  
        // proses pengecekan dilakukan terus menerus 
        ResetVelocityY();
        // Memanggil Move di dalam Update untuk menggerakan character terus menerus 
        // sesuai dengan arah dan kecepatan gerakan character. 
        Move();
    }
    

    // Function untuk mengisi arah input ke arah gerakan character  
    // Function ini akan listen ke event OnMoveInput 
    public void SetMoveDirection(Vector2 inputDirection) 
    { 
        // Mengisikan arah input sumbu x ke arah gerakan character sumbu x 
        // Mengisikan arah input sumbu y ke arah gerakan character sumbu z 
        _movementDirection = new Vector3(inputDirection.x, 0, inputDirection.y); 
    }
    public void Move() 
    { 
        // Menghitung arah dan kecepatan gerakan character di sumbu x dan z 
        CalculateVelocityXZ(); 
        // Menghitung arah dan kecepatan gerakan character di sumbu y 
        CalculateVelocityY(); 
        // Menggabung arah dan kecepatan gerakan character di sumbu x, y, dan z 
        Vector3 velocity = new Vector3(_velocityXZ.x, _velocityY, _velocityXZ.z); 
        // Menggerakkan character sesuai arah dan kecepatan yang sudah dihitung 
        _characterController.Move(velocity); 
    }
    private void CheckIsGrounded() 
    { 
        // Mendapatkan layer Ground 
        LayerMask groundLayer = LayerMask.GetMask("Ground"); 
        // Membuat pendeteksi berbentuk bola  
        // Posisi pendeteksi nya di transform.position(posisi kaki character) 
        // Radius = 0.5 
        // Layer object yang dicek adalah layer ground 
        // Jika terdeteksi _isGrounded bernilai true,  
        // jika tidak _isGrounded bernilai false 
        _isGrounded = Physics.CheckSphere(transform.position, 0.5f, groundLayer); 
    } 
    private void CalculateVelocityXZ() 
    { 
        // Mendapatkan transform camera untuk mendapatkan rotasi camera 
        Transform cameraTransform = Camera.main.transform; 
        // Menghitung arah gerakkan sumbu x  
        // disesuaikan dengan arah samping camera  
        Vector3 xDirection = _movementDirection.x * cameraTransform.right; 
        // Menghitung arah gerakkan sumbu z  
        // disesuaikan dengan arah depan camera 
        Vector3 zDirection = _movementDirection.z * cameraTransform.forward; 
        // Menggabung arah gerakkan sumbu x dan sumbu z ke dalam satu vector 
        Vector3 direction = xDirection + zDirection; 
        // Arah gerakkan y dibuat nol,  
        // karena tidak ada gerakan ke arah atas dan bawah 
        direction.y = 0; 
        // Mengecek apakah menekan input bergerak atau tidak 
        // Magnitude digunakan untuk mengecek besar nilai vector 
        // Magnitude movement direction > 0 -> ada input move ditekan  
        // (karakter bergerak) 
        // Magnitude movement direction == 0 -> tidak ada input move  
        // ditekan (karakter tidak bergerak) 
        if (_movementDirection.magnitude > 0.01) 
        { 
            // Jika ada input move ditekan  
            // maka karakter bergerak berdasarkan arah dan kecepatan 
            // direction.normalized diguakan  
            // untuk hanya mendapatkan arah dari direction yang sudah dihitung 
            // Dikalikan Time.deltaTime supaya kecepatan bergerak  
            // yang sama di semua komputer dengan FPS berbeda 
            _velocityXZ = direction.normalized * _currentSpeed * Time.deltaTime; 
        } 
        else 
        { 
            // Jika tidak ada input move ditekan  
            // maka karakter tidak bergerak (kecepatan nya nol di semua sumbu) 
            // Vector3.zero = Vector3(0,0,0) 
            _velocityXZ = Vector3.zero; 
        } 
    } 
    private void CalculateVelocityY() 
    { 
        // Menghitung kecepatan gerakan charater di sumbu y 
        _velocityY = _velocityY + Physics.gravity.y * _gravityScale * Time.deltaTime; 
    } 
    private void ResetVelocityY() 
    { 
        // Mengecek apakah character sudah ada di ground 
        if (_isGrounded == true && _velocityY < 0) 
        { 
            // Jika iya maka reset velocity ke -2 
            // Sebenarnya bisa di-reset ke 0 
            // Tapi jika masih ingin menarik karakter nya sedikit,  
            // reset velocity ke -2 atau -1 
            _velocityY = -2; 
        } 
    } 
    public void SetSprint(bool isSprint) 
    { 
        _isSprint = isSprint; 
    } 
    private void CalculateAcceleration() 
    { 
        // Mengecek apakah player character bergerak atau tidak 
        if (_movementDirection.magnitude > 0.01) 
        { 
            // Jika karakter bergerak maka akan mengecek 
            // Apakah character sedang sprint atau tidak 
            if (_isSprint) 
            { 
                // Jika sedang sprint maka kecepatan akan bertambah  
                // sebesar nilai acceleration setiap detik 
                _currentSpeed = _currentSpeed + _acceleration * Time.deltaTime; 
            } 
            else 
            { 
                // Jika berhenti sprint maka kecepatan akan berkurang  
                // sebesar nilai acceleration setiap detik 
                _currentSpeed = _currentSpeed - _acceleration * Time.deltaTime; 
            } 
            // Membatasi kecepatan, minimum: walk speed maksimum: sprint speed  
            _currentSpeed = Mathf.Clamp(_currentSpeed, _walkSpeed, _sprintSpeed); 
        } 
        else 
        { 
            // Jika tidak bergerak kecepatan diset nol,  
            // agar character tidak bisa bergerak 
            _currentSpeed = 0; 
        } 
    } 
    
}
