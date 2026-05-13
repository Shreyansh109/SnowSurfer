using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    float totalRotation;
    int flipCount;
    float previousRotation;
    PlayerMove playerMove;


    void Start()
    {
        scoreText.text = "Score: 1";
        playerMove = FindFirstObjectByType<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        ScoreController_Update();
    }

    private void ScoreController_Update()
    {
        float score = playerMove.CountRotation();
        scoreText.text = "Score: " + score.ToString();
    }
    
}
