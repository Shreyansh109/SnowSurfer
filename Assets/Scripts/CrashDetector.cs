using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] ParticleSystem particleEffect;
    PlayerMove playerMove; 


    void OnTriggerEnter2D(Collider2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Floor");
        if(collision.gameObject.layer==layerIndex)
        {
            playerMove=FindFirstObjectByType<PlayerMove>();
            playerMove.crash=true;
            particleEffect.Play();
            Invoke("ReloadScene", 1f); //create delay in scene
            // SceneManager.LoadScene(0);
        }
    }
    void ReloadScene()
    {
        SceneManager.LoadScene(0);//load excisting scene with index or by name
    }
}
