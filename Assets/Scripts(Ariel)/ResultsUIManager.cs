using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ResultsUIManager : MonoBehaviour
{
    [SerializeField] private PlayerScoreData playerScoreData;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button menuButton;

    private void Start()
    {
        float finalTime = 0f;
        UIEventManager.Instance.OnTimeUpdated += (time) => finalTime = time;

        timeText.text = $"Tiempo total: {finalTime:F2} segundos";
        resultText.text = "¡Perdiste o Ganaste!";
        finalScoreText.text = $"Puntaje final: {playerScoreData.score}";

        retryButton.onClick.AddListener(() => UIEventManager.Instance.OnRestart?.Invoke());
        retryButton.onClick.AddListener(() => SceneManager.LoadScene("Gameplay"));

        menuButton.onClick.AddListener(() => UIEventManager.Instance.OnGoToMenu?.Invoke());
        menuButton.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
    }
}
