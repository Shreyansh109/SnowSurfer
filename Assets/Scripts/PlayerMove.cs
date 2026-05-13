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
    public bool crash=false;
    
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!crash){
            RotatePlayer();
            BoostPlayer();
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
        surfaceEffector2D = FindFirstObjectByType<SurfaceEffector2D>();
        if(moveInput.y > 0)
        {
            surfaceEffector2D.speed = 30f;
        }
        else
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
}
