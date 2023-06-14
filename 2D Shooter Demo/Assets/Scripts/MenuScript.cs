using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Button startButton;
    public Button closeButton;
    public float animationDuration = 0.5f;
    public float spaceRatio = 0.3f;

    private Vector3 startButtonStartPos;
    private Vector3 closeButtonStartPos;
    private Vector3 buttonEndPos;
    private float buttonHeight;

    public static bool GameIsPaused;
    [SerializeField] private GameObject pauseButton, pauseMenu,deadMenu;

    private static AsyncOperationHandle<SceneInstance> m_SceneLoadOpHandle;
    private void OnEnable()
    {
        
        Start();
    }
    private void OnDisable()
    {
        
    }
    private void Start()
    {
        // Store initial button positions
        startButtonStartPos = startButton.transform.position;
        closeButtonStartPos = closeButton.transform.position;

        // Calculate button height and vertical spacing
        buttonHeight = startButton.GetComponent<RectTransform>().rect.height;
        float spaceBetweenButtons = buttonHeight * spaceRatio;

        // Calculate end position at the middle of the screen
        float middleX = Screen.width / 2f;
        float middleY = Screen.height / 2f;
        float totalButtonHeight = buttonHeight * 2f + spaceBetweenButtons;
        float startY = middleY + totalButtonHeight / 2f;
        buttonEndPos = new Vector3(960, 645, 0f);

        // Move buttons to the initial position
        startButton.transform.position = startButtonStartPos - new Vector3(Screen.width, 0f, 0f);
        closeButton.transform.position = closeButtonStartPos - new Vector3(Screen.width, 0f, 0f);

        // Start button animation
        StartCoroutine(AnimateButtons(totalButtonHeight));
    }

    public void StartGame()
    {
        Debug.Log("Starting the game...");
        // Add your game start logic here
        Loading.lastScene = SceneManager.GetActiveScene().name;
        LoadNextLevel();

    }

    public void CloseGame()
    {
        Debug.Log("Closing the game...");
        // Add your game close logic here
        Application.Quit();
    }
    public void BacktoMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        Loading.lastScene = SceneManager.GetActiveScene().name;
        LoadNextLevel();
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
    }
    public void Pause()
    {
        
        GameIsPaused = true;
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseButton.SetActive(true);
        deadMenu.SetActive(false);
        SceneManager.LoadScene(1);
    }
    
    public static void LoadNextLevel()
    {
        m_SceneLoadOpHandle = Addressables.LoadSceneAsync("LoadingScene", activateOnLoad: true);
    }
    /*private System.Collections.IEnumerator AnimateButtons(float totalButtonHeight)
    {
        float elapsedTime = 0f;
        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;
            startButton.transform.position = Vector3.Lerp(startButtonStartPos - new Vector3(Screen.width, 0f, 0f), buttonEndPos, t);
            closeButton.transform.position = Vector3.Lerp(closeButtonStartPos - new Vector3(Screen.width, 0f, 0f), buttonEndPos - new Vector3(0f, totalButtonHeight, 0f), t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Ensure the final position is set correctly
        startButton.transform.position = buttonEndPos;
        closeButton.transform.position = buttonEndPos - new Vector3(0f, totalButtonHeight, 0f);
    }*/
    private System.Collections.IEnumerator AnimateButtons(float totalButtonHeight)
    {
        float elapsedTime = 0f;
        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;

            // Calculate button positions
            Vector3 startButtonTargetPos = Vector3.Lerp(startButtonStartPos - new Vector3(Screen.width, 0f, 0f), buttonEndPos, t);
            Vector3 closeButtonTargetPos = Vector3.Lerp(closeButtonStartPos + new Vector3(Screen.width, 0f, 0f), buttonEndPos - new Vector3(0f, totalButtonHeight, 0f), t);

            // Set button positions
            startButton.transform.position = startButtonTargetPos;
            closeButton.transform.position = closeButtonTargetPos;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position is set correctly
        startButton.transform.position = buttonEndPos;
        closeButton.transform.position = buttonEndPos - new Vector3(0f, totalButtonHeight, 0f);
        if (GameIsPaused)
        {
            Time.timeScale = 0f;
        }
    }
}