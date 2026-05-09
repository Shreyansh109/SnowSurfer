using Unity.VisualScripting;
using UnityEngine;

public class Winning : MonoBehaviour
{
    [SerializeField] ParticleSystem particleEffect;
    void OnTriggerEnter2D(Collider2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Player");
        if(collision.gameObject.layer==layerIndex)
        {
            particleEffect.Play();
        }
    }
}
