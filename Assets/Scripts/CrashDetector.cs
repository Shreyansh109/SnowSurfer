using UnityEngine;

public class CrashDetector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Floor");
        if(collision.gameObject.layer==layerIndex)
        {
            Debug.Log("You lose!");
        }
    }
}
