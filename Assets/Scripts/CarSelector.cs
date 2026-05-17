using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CarSelector : MonoBehaviour
{
    [SerializeField] GameObject scoreCanvas;
    [SerializeField] GameObject dinoCar;
    [SerializeField] GameObject snowCar;
    [SerializeField] CinemachineCamera cinemachineCam;

    void Start()
    {
        Time.timeScale = 0f;
    }

    void BeginGame()
    {
        Time.timeScale = 1f;
        scoreCanvas.SetActive(true);
        gameObject.SetActive(false);
    }

    public void SelectDino()
    {
        dinoCar.SetActive(true);
        BeginGame();
    }

    public void SelectFrog()
    {
        cinemachineCam.Follow = snowCar.transform;
        snowCar.SetActive(true);
        BeginGame();
    }
}
