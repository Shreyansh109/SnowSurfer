using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    float rbTorque=4f;
    private InputAction moveAction;
    Rigidbody2D rb2d;
    Vector2 moveInput;
    [SerializeField] ParticleSystem snowEffect;
    [SerializeField] ParticleSystem powerupEffect;
    Winning winning;
    SurfaceEffector2D surfaceEffector2D;
    bool crash=false;
    float totalRotation;
    int flipCount;
    float previousRotation;
    bool speedup=false;
    
    
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        winning = FindFirstObjectByType<Winning>();
        surfaceEffector2D = FindFirstObjectByType<SurfaceEffector2D>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!crash){
            RotatePlayer();
            BoostPlayer();
            CountRotation();
        }else{
            surfaceEffector2D.speed = 3f;
        }
    }

    void RotatePlayer()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        // rb2d.AddTorque(rbTorque);
        rb2d.AddTorque(rbTorque * moveInput.x * -1);
    }

    void BoostPlayer()
    {
        if(moveInput.y > 0 && !speedup && !winning.Win())
        {
            surfaceEffector2D.speed = 30f;
        }
        else if(moveInput.y == 0 && !speedup && !winning.Win())
        {
            surfaceEffector2D.speed = 20f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        int layerIndex=LayerMask.NameToLayer("Floor");
        if(collision.gameObject.layer==layerIndex)
        {
            snowEffect.Play();
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        int layerIndex=LayerMask.NameToLayer("Floor");
        if(collision.gameObject.layer==layerIndex)
        {
            snowEffect.Stop();
        }
    }

    public void DisableControls()
    {
        crash=true;
    }

    public int CountRotation()
    {
        float currentRotation = transform.rotation.eulerAngles.z;
        totalRotation += Mathf.DeltaAngle(previousRotation, currentRotation);
        if(Mathf.Abs(totalRotation) >= 300f)
        {
            totalRotation = 0f; // Reset total rotation after a full spin
            flipCount++;
        }
        // print("Total Rotation: " + totalRotation);
        previousRotation = currentRotation;
        return flipCount;
    }

    void ResetSpeed()
    {
        speedup=false;
        surfaceEffector2D.speed = 20f;
        powerupEffect.Stop();
    }
    void ResetTorque()
    {
        rbTorque = 4f;
        powerupEffect.Stop();
    }

    public void ActivatePowerUp(PowerUpSO powerUpSO)
    {
        var main = powerupEffect.main;
        if(powerUpSO.GetPowerUpName() == "SpeedShort")
        {
            speedup = true;
            surfaceEffector2D.speed = powerUpSO.GetPowerUpValue();
            main.startColor = new Color(1f, 0.5f, 0f); // Orange
            powerupEffect.Play();
            Invoke("ResetSpeed", powerUpSO.GetPowerUpDuration());
        }
        else if(powerUpSO.GetPowerUpName() == "Torque")
        {
            rbTorque = powerUpSO.GetPowerUpValue();
            main.startColor = new Color(0.659f, 0.333f, 0.969f); // Purple
            powerupEffect.Play();
            Invoke("ResetTorque", powerUpSO.GetPowerUpDuration());
        }
    }
}
