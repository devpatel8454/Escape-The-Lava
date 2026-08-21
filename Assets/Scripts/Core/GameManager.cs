using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState
    {
        Playing,
        Won,
        Lost
    }

    [Header("Game Settings")]
    [SerializeField] private int startingLives = 5;

    [Header("Managers")]
    [SerializeField] private GridManager gridManager;
    [SerializeField] private GameTimer gameTimer;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private DamageEffect damageEffect;

    [Header("Score Popup")]
    [SerializeField] private ScorePopup scorePopupPrefab;
    [SerializeField] private Transform popupContainer;

    private GameState currentState;

    private int lives;
    private int score;
    private int totalDiamonds;

    public int Lives => lives;
    public int Score => score;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        currentState = GameState.Playing;

        lives = startingLives;
        score = 0;

        uiManager.UpdateLives(lives);
        uiManager.UpdateScore(score);

        gridManager.GenerateGrid();

        gameTimer.StartTimer();
    }

    public void SetTotalDiamonds(int amount)
    {
        totalDiamonds = amount;
    }

    public void HandleTileClick(
        Tile tile,
        Vector2 clickPosition)
    {
        if (currentState != GameState.Playing)
            return;

        switch (tile.Type)
        {
            case TileType.Green:

                // Safe zone
                break;

            case TileType.Diamond:

                CollectDiamond(tile, clickPosition);
                break;

            case TileType.Lava:

                HitLava();
                break;
        }
    }

    private void CollectDiamond(
        Tile tile,
        Vector2 clickPosition)
    {
        tile.CollectDiamond();

        score++;

        uiManager.UpdateScore(score);

        ShowScorePopup(clickPosition);

        CheckWinCondition();
    }

    private void HitLava()
    {
        lives--;

        uiManager.UpdateLives(lives);

        if (damageEffect != null)
        {
            damageEffect.PlayDamageEffect();
        }

        if (lives <= 0)
        {
            GameOver();
        }
    }

    private void CheckWinCondition()
    {
        if (score >= totalDiamonds)
        {
            WinGame();
        }
    }

    public void TimeUp()
    {
        if (currentState != GameState.Playing)
            return;

        GameOver();
    }

    private void WinGame()
    {
        if (currentState != GameState.Playing)
            return;

        currentState = GameState.Won;

        gameTimer.StopTimer();

        gridManager.DisableAllTiles();

        uiManager.ShowWin(score);
    }

    private void GameOver()
    {
        if (currentState != GameState.Playing)
            return;

        currentState = GameState.Lost;

        gameTimer.StopTimer();

        gridManager.DisableAllTiles();

        uiManager.ShowGameOver(score);
    }

    private void ShowScorePopup(Vector2 screenPosition)
    {
        if (scorePopupPrefab == null)
            return;

        ScorePopup popup =
            Instantiate(
                scorePopupPrefab,
                popupContainer
            );

        RectTransform popupRect =
            popup.GetComponent<RectTransform>();

        RectTransform canvasRect =
            popupContainer.GetComponent<RectTransform>();

        Canvas canvas =
            popupContainer.GetComponentInParent<Canvas>();

        Camera cam = null;

        if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
        {
            cam = canvas.worldCamera;
        }

        Vector2 localPosition;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPosition,
            cam,
            out localPosition
        );

        popupRect.localPosition = localPosition;
    }

    public void RestartGame()
    {
        Scene currentScene =
            SceneManager.GetActiveScene();

        SceneManager.LoadScene(
            currentScene.buildIndex
        );
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void UpdateTimerUI(float time)
    {
        uiManager.UpdateTimer(time);
    }
}