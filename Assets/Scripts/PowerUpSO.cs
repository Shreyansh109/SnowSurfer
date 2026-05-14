using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpSO", menuName = "Scriptable Objects/PowerUp")]
public class PowerUpSO : ScriptableObject
{
    [SerializeField] private string powerUpName;
    [SerializeField] private float powerUpValue;
    [SerializeField] private float powerUpDuration;

    public string GetPowerUpName()
    {
        return powerUpName;
    }
    public float GetPowerUpValue()
    {
        return powerUpValue;
    }
    public float GetPowerUpDuration()
    {
        return powerUpDuration;
    }
}
