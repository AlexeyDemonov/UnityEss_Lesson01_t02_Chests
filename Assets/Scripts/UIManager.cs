using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public int PlayerMoves;
    public Text MovesCounter;

    public GameObject GameEndPanel;
    public Text GameOverText;

    public GameManager GameManager;
    public ItemsAndChestsContainer Container;

    int _movesLeft;
    int _countToVictory;

    // Start is called before the first frame update
    void Start()
    {
        bool valid = Container.Chests.Length << 31 == 0;// == (Length % 2 == 0)

        if (!valid)
            Debug.LogError("Chests count must be even number");

        _countToVictory = Container.Chests.Length >> 1;// == Length / 2
        _movesLeft = PlayerMoves;
        MovesCounter.text = PlayerMoves.ToString();

        GameManager.PlayerMovedSuccessfully += HandlePlayerMove;
    }

    void HandlePlayerMove(bool success)
    {
        _movesLeft--;
        MovesCounter.text = _movesLeft.ToString();

        if(success)
        {
            _countToVictory--;

            if(_countToVictory == 0)
            {
                GameOverText.text = "You win";
                EndGame();
            }
        }
        else if (_movesLeft == 0)
        {
            GameOverText.text = "You lose";
            EndGame();
        }
    }

    void EndGame()
    {
        GameManager.GameIsRunning = false;
        GameEndPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}