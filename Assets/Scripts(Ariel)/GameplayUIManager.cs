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
    [SerializeField] private GameIntEvent coinCollectedEvent;
    [SerializeField] private GameIntEvent healthChangedEvent;
    [SerializeField] private GameIntEvent damageTakenEvent;

    private float elapsedTime = 0f;
    private int maxHealth = 10;

    private void Start()
    {
        playerScoreData.ResetScore();
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;

        pauseButton.onClick.AddListener(() => UIEventManager.Instance.OnPause?.Invoke());

        foreach (var button in colorButtons)
        {
            Color color = button.GetComponent<Image>().color;
            button.onClick.AddListener(() => UIEventManager.Instance.OnColorChange?.Invoke(color));
        }

        // Suscripción a los nuevos eventos
        coinCollectedEvent.RegisterListener(OnCoinCollected);
        healthChangedEvent.RegisterListener(OnHealthChanged);
        damageTakenEvent.RegisterListener(OnDamageTaken);
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        timerText.text = $"Tiempo: {elapsedTime:F2}";
        UIEventManager.Instance.OnTimeUpdated?.Invoke(elapsedTime);
    }

    private void OnCoinCollected(int points)
    {
        playerScoreData.AddScore(points);
        scoreText.text = $"Puntaje: {playerScoreData.score}";
    }

    private void OnHealthChanged(int healAmount)
    {
        healthBar.value = Mathf.Min(healthBar.value + healAmount, maxHealth);
    }

    private void OnDamageTaken(int damage)
    {
        healthBar.value -= damage;
        if (healthBar.value <= 0)
        {
            SceneManager.LoadScene("Results");
        }
    }

    private void OnDestroy()
    {
        // Desuscripción de los eventos
        coinCollectedEvent.UnregisterListener(OnCoinCollected);
        healthChangedEvent.UnregisterListener(OnHealthChanged);
        damageTakenEvent.UnregisterListener(OnDamageTaken);
    }
}