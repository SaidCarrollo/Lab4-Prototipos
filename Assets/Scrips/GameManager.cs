using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonPersistent<GameManager>
{
    private float gameTime = 0f;
    private bool isPaused = false;

    [Header("Eventos")]
    [SerializeField] private GameBoolEvent victoryDefeatEvent; // Asignar en Inspector

    public bool LastGameResult { get; private set; }

    private void OnEnable()
    {
        if (victoryDefeatEvent != null)
            victoryDefeatEvent.Register(GetComponent<GameBoolEventListener>());
    }

    private void OnDisable()
    {
        if (victoryDefeatEvent != null)
            victoryDefeatEvent.Unregister(GetComponent<GameBoolEventListener>());
    }

    public void HandleVictoryCondition(bool isVictory)
    {
        LastGameResult = isVictory;
        SceneManager.LoadScene("Results");
    }
    private void Update()
    {
        if (!isPaused)
        {
            gameTime += Time.deltaTime;
            UIEventManager.Instance.OnTimeUpdated?.Invoke(gameTime);
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        UIEventManager.Instance.OnPause?.Invoke();
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        UIEventManager.Instance.OnResume?.Invoke();
    }
}