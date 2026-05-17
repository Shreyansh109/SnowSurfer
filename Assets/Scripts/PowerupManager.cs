using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    [SerializeField] PowerUpSO speedUP_short;
    PlayerMove playerMove;
    void OnTriggerEnter2D(Collider2D collision)
    {
        playerMove = FindFirstObjectByType<PlayerMove>();
        int layerIndex=LayerMask.NameToLayer("Player");
        if(collision.gameObject.layer==layerIndex)
        {
            playerMove.ActivatePowerUp(speedUP_short);
            Destroy(gameObject, 0.2f);
        }
    }
}
