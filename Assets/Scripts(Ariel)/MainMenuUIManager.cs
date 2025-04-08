using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        playButton.onClick.AddListener(() => SceneManager.LoadScene("Gameplay"));
        quitButton.onClick.AddListener(() => Application.Quit());
    }
}

