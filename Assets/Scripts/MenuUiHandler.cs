using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)] // Ensure this script runs after DataHolder is initialized
public class MenuUiHandler : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText;
    public TMP_InputField playerNameInputField;

    private int bestScore;
    private string bestPlayer;
    private string currentPlayer;

    

    private void Awake()
    {
        if (DataHolder.Instance != null)
        {
            bestScore = DataHolder.Instance.bestScore;
            bestPlayer = DataHolder.Instance.bestPlayer;
            currentPlayer = DataHolder.Instance.currentPlayerName;

        }
        bestScoreText.text = "Best Score : " + bestPlayer + ": " + bestScore.ToString();
        playerNameInputField.text = currentPlayer;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Save file path: " + Application.persistentDataPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartGame()
    {
        if (!string.IsNullOrEmpty(playerNameInputField.text))
        {
            
            DataHolder.Instance.currentPlayerName = playerNameInputField.text;
            DataHolder.Instance.Save(); // Save the current player name
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.LogWarning("Player name cannot be empty. Please enter a name before starting the game.");
        }
    }

    public void QuitGame()
    {
        DataHolder.Instance.Save(); // Save the current state before quitting
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
                        Application.Quit(); // original code to quit Unity player
        #endif

    }
}
