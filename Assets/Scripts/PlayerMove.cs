using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    float rbTorque=2f;
    private InputAction moveAction;
    Rigidbody2D rb2d;
    [SerializeField] ParticleSystem particleEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Vector2 moveInput = moveAction.ReadValue<Vector2>();
        // print(moveInput.x);
        // rb2d.AddTorque(moveInput.x * rbTorque * -1);

        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        // rb2d.AddTorque(rbTorque);
        rb2d.AddTorque(rbTorque * moveInput.x * -1);
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
