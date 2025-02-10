using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUiHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private TextMeshProUGUI bestScoreText;
    [SerializeField]
    private TMP_InputField nameInput;
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button quitButton;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.LoadGameData();

        bestScoreText.text = GameManager.Instance.GetBestScoreText();
        playButton.interactable = false;
        nameInput.onValueChanged.AddListener(OnInputChanged);
        playButton.onClick.AddListener(StartNewGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    //Handles play button interactibility according to text input status.
    private void OnInputChanged(string input)
    {
        playButton.interactable = !string.IsNullOrEmpty(input);
    }

    //Starts new game
    private void StartNewGame()
    {
        GameManager.Instance.SetNewPlayerName(nameInput.text);
        SceneManager.LoadScene(1);
    }

    //Exits the game.
    private void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
