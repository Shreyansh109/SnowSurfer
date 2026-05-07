using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float rbTorque=0.5f;
    private InputAction moveAction;
    Rigidbody2D rb2d;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.AddTorque(rbTorque);
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        print(moveInput.x);
        rb2d.AddTorque(moveInput.x * rbTorque);
    }
}
