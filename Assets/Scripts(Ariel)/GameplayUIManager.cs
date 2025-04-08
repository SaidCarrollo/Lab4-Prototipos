using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameplayUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button[] colorButtons;
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Scriptable Objects")]
    [SerializeField] private PlayerScoreData playerScoreData;

    private float elapsedTime = 0f;
    private int currentScore = 0;
    private int maxHealth = 10;

    private void Start()
    {
        playerScoreData.ResetScore(); 
        pauseButton.onClick.AddListener(() => UIEventManager.Instance.OnPause?.Invoke());

        foreach (var button in colorButtons)
        {
            Color color = button.GetComponent<Image>().color;
            button.onClick.AddListener(() => UIEventManager.Instance.OnColorChange?.Invoke(color));
        }

        UIEventManager.Instance.OnCoinCollected += UpdateScore;
        UIEventManager.Instance.OnHeartCollected += Heal;
        UIEventManager.Instance.OnDamageTaken += TakeDamage;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        timerText.text = $"Tiempo: {elapsedTime:F2}";
        UIEventManager.Instance.OnTimeUpdated?.Invoke(elapsedTime);
    }
    private void UpdateScore(int value)
    {
        playerScoreData.AddScore(value);
        scoreText.text = $"Puntaje: {playerScoreData.score}";
    }

    private void Heal(int value)
    {
        healthBar.value = Mathf.Min(healthBar.value + value, maxHealth);
    }

    private void TakeDamage(int value)
    {
        healthBar.value -= value;
        if (healthBar.value <= 0)
        {
            SceneManager.LoadScene("Results");
        }
    }

    private void OnDestroy()
    {
        // Unsubscribing to prevent memory leaks
        UIEventManager.Instance.OnCoinCollected -= UpdateScore;
        UIEventManager.Instance.OnHeartCollected -= Heal;
        UIEventManager.Instance.OnDamageTaken -= TakeDamage;
    }
}
