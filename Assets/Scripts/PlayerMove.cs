using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    float rbTorque=4f;
    private InputAction moveAction;
    Rigidbody2D rb2d;
    Vector2 moveInput;
    [SerializeField] ParticleSystem particleEffect;
    SurfaceEffector2D surfaceEffector2D;
    bool crash=false;
    float totalRotation;
    int flipCount;
    float previousRotation;
    bool speedup=false;
    
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
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
        if(moveInput.y > 0 && !speedup)
        {
            surfaceEffector2D.speed = 30f;
        }
        else if(moveInput.y == 0 && !speedup)
        {
            surfaceEffector2D.speed = 20f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        int layerIndex=LayerMask.NameToLayer("Floor");
        if(collision.gameObject.layer==layerIndex)
        {
            particleEffect.Play();
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        int layerIndex=LayerMask.NameToLayer("Floor");
        if(collision.gameObject.layer==layerIndex)
        {
            particleEffect.Stop();
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
    }

    public void ActivatePowerUp(PowerUpSO powerUpSO)
    {
        if(powerUpSO.GetPowerUpName() == "SpeedShort")
        {
            speedup = true;
            surfaceEffector2D.speed = powerUpSO.GetPowerUpValue();
            Invoke("ResetSpeed", powerUpSO.GetPowerUpDuration());
        }
    }
}
