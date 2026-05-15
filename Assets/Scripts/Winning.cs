using Unity.VisualScripting;
using UnityEngine;

public class Winning : MonoBehaviour
{
    [SerializeField] ParticleSystem particleEffect;
    SurfaceEffector2D surfaceEffector2D;
    bool win=false;
    void Start()
    {
        surfaceEffector2D = FindFirstObjectByType<SurfaceEffector2D>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Player");
        if(collision.gameObject.layer==layerIndex)
        {
            particleEffect.Play();
            surfaceEffector2D.speed = 0f;
            win = true;
        }
    }

    public bool Win()
    {
        return win;
    }
}
