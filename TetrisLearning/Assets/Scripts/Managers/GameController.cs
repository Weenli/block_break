using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private Board gameboard;

    SkillsManager skillManager;

    private Spawner spawner;

    ScoreManager scoremanager;

    Shape ActiveShape;

    Ghost ghost;

    public Text text;

    public IconToggle Swipe_Button_Icon;

    int TimeSkillCoins = 300;
    int DeleteRowCoins = 450;
    int QueueCoins = 600;

    bool m_clockwise = true;
    public float dropIntervalModded;
    float dropInterval = 0.8f;
    float TimeToDrop  = 0f;
    /*****************/
    [Range(0.02f, 1f)]
    public float KeyLeftRight= 0.25f;
    float TimeToNextKeyLeftRight = 0f;
    /*****************/
    [Range(0.01f, 1f)]
    public float KeyDownTime = 0.08f;
    float TimeToNextKeyDown = 0f;
    /*****************/
    [Range(0.02f, 1f)]
    public float KeyRotateTime = 0.1f;
    float TimeToNextKeyRotate = 0f;
    /*****************/
    bool GameOver = false;
    /*****************/
    public GameObject DeleteRawPanel;
    public GameObject GameOverPanel;
    public GameObject GameStartPanel;
    public GameObject GameMainPanel;
    public GameObject QueuePanel;
    public GameObject DonatePanel;
    public GameObject SettingsPanel;
    public GameObject ButtonManager;
    public GameObject FortuneWheelPanel;
    public GameObject FortuneWheelButton;
    SoundManager soundmanager;
    /*****************/
    public bool pressed = false;
    public bool PressedLeft = false;
    public bool PressedRight = false;
    /*****************/
    public bool IsPaused = false;
    public int IsButtonControl = 1;
    public GameObject PausePanel;
    bool gamestart = false;
    enum Direction { none, left, right, down, up}
    Direction SwipeDirection = Direction.none;
    Direction SwipeEndDirection = Direction.none;
    public void MoveRight()
    {
        ActiveShape.MoveRight();
        TimeToNextKeyLeftRight = Time.time + KeyLeftRight;
        if (!gameboard.IsValidPosition(ActiveShape))
        {
            ActiveShape.MoveLeft();
            PlaySound(soundmanager.ErrorSound);
        }
        else
        {
            PlaySound(soundmanager.MoveSound);
        }
    }
    public void MoveLeft()
    {
        
        ActiveShape.MoveLeft();
        TimeToNextKeyLeftRight = Time.time + KeyLeftRight;
        if (!gameboard.IsValidPosition(ActiveShape))
        {
            ActiveShape.MoveRight();
            PlaySound(soundmanager.ErrorSound);
        }
        else
        {
            PlaySound(soundmanager.MoveSound);
        }
    }
    public void Rotate()
    {
        //m_activeShape.RotateRight();
        ActiveShape.RotateClockwise(m_clockwise);
        TimeToNextKeyRotate = Time.time + KeyRotateTime;

        if (!gameboard.IsValidPosition(ActiveShape))
        {
            //m_activeShape.RotateLeft();
            ActiveShape.RotateClockwise(!m_clockwise);
            PlaySound(soundmanager.ErrorSound, 0.5f);
        }
        else
        {
            PlaySound(soundmanager.MoveSound, 0.5f);
        }
    }
    public void ButtonMoveDown()
    {

        pressed = !pressed;
    }
    public void BottonMoveLeft()
    {
        PressedLeft = !PressedLeft;
    }
    public void BottonMoveRight()
    {
        PressedRight = !PressedRight;
    }
    public void MoveDown()
    {
        TimeToNextKeyDown = Time.time + KeyDownTime;
        
        TimeToDrop = Time.time + dropIntervalModded;
        
        ActiveShape.MoveDown();
        
        if (!gameboard.IsValidPosition(ActiveShape))
        {
            if (gameboard.TopLimit(ActiveShape))
            {
                GameOverFu();
            }
            else
            {
                SwipeDirection = Direction.none;
                SwipeEndDirection = Direction.none;
                LandShape();
            }
        }
    }
    void Start()
    {
        gameboard = GameObject.FindObjectOfType<Board>();
        spawner = GameObject.FindObjectOfType<Spawner>();
        soundmanager = GameObject.FindObjectOfType<SoundManager>();
        scoremanager = GameObject.FindObjectOfType<ScoreManager>();
        skillManager = GameObject.FindObjectOfType<SkillsManager>();
        ghost = GameObject.FindObjectOfType<Ghost>();
        if (!gameboard)
        {
            Debug.Log("WARNING!");
        }
        if (!soundmanager)
        {
            Debug.Log("WARNING!");
        }
        if (!scoremanager)
        {
            Debug.Log("Warning!");
        }
        if (!spawner)
        {
            Debug.Log("WARNING!");
        }
        else
        {
            spawner.transform.position = VectorRound.Round(spawner.transform.position);            
        }
        if (PausePanel)
        {
            PausePanel.SetActive(false);
        }
        IsButtonControl = PlayerPrefs.GetInt("Manager");
        if (IsButtonControl == 0)
        {
            Swipe_Button_Icon.ToggleIconX(IsButtonControl);
            KeyLeftRight = .2f;
            text.text = "Button";
        }
        else if(IsButtonControl == 1)
        {
            Swipe_Button_Icon.ToggleIconX(IsButtonControl);
            KeyLeftRight = .05f;
            text.text = "Swipe";
        }
        dropIntervalModded = dropInterval;
    }
 
    void LateUpdate()
    {
        if (ghost)
        {
            ghost.DrawGhost(ActiveShape, gameboard);
        }
    }

    void PlayerInput()
    {
        if (!gameboard || !spawner)
        {
            return;
        }
        if (Input.GetButton("MoveRight") && Time.time > TimeToNextKeyLeftRight && IsButtonControl == 1)
        {
            MoveRight();
        }
        else if (Input.GetButton("MoveLeft") && Time.time > TimeToNextKeyLeftRight && IsButtonControl == 1)
        {
            MoveLeft();
        }
        else if (Input.GetButtonDown("Rotate") && Time.time > TimeToNextKeyRotate && IsButtonControl == 1)
        {
            Rotate();
        }
        else if (IsButtonControl == 1 && Input.GetButton("MoveDown") && Time.time > TimeToNextKeyDown || Time.time > TimeToDrop )
        {
            MoveDown();
        }
        else if (IsButtonControl == 1 && ((SwipeDirection == Direction.right && Time.time > TimeToNextKeyLeftRight) || SwipeEndDirection == Direction.right))
        {
            MoveRight();
            SwipeDirection = Direction.none;
            SwipeEndDirection = Direction.none;
        }
        else if (IsButtonControl == 1 && ((SwipeDirection == Direction.left && Time.time > TimeToNextKeyLeftRight) || SwipeEndDirection == Direction.left)) 
        {
            MoveLeft();
            SwipeDirection = Direction.none;
            SwipeEndDirection = Direction.none;
        }
        else if (SwipeEndDirection == Direction.up && IsButtonControl == 1)
        {
            Rotate();
            SwipeEndDirection = Direction.none;
        }
        else if (SwipeDirection == Direction.down && Time.time > TimeToNextKeyDown && IsButtonControl == 1)
        {
            MoveDown();
            SwipeDirection = Direction.none;
        }
        else if(IsButtonControl == 0 && Time.time > TimeToDrop)
        {
            MoveDown();
        }
    }
    void Update()
    {
        if (!gameboard || !spawner || GameOver || !soundmanager || !scoremanager)
        {
            return;
        }
        if (gamestart)
        {
            if (!ActiveShape )
            {
                ActiveShape = spawner.SpawnShape();
            }
            if (!IsPaused)
            {
                PlayerInput();
            }
        }
        if (IsButtonControl == 0)
        {
            Swipe_Button_Icon.ToggleIconX(IsButtonControl);
            text.text = "Button";
            if (pressed && Time.time > TimeToNextKeyDown || Time.time > TimeToDrop)
            {
                MoveDown();
            }
            else if (PressedRight && Time.time > TimeToNextKeyLeftRight)
            {
                MoveRight();
            }
            else if (PressedLeft && Time.time > TimeToNextKeyLeftRight)
            {
                MoveLeft();
            }
        }
        else if (IsButtonControl == 1)
        {
            Swipe_Button_Icon.ToggleIconX(IsButtonControl);
            text.text = "Swipe";
        }
    }

    void LandShape()
    {
        ActiveShape.MoveUp();
        gameboard.StoreShapeInGrid(ActiveShape);
        if (ghost)
        {
            ghost.Reset();
        }
        ActiveShape = spawner.SpawnShape();
        gameboard.ClearAllRows();
        PlaySound(soundmanager.DropSound);
        if(gameboard.clearedrows > 0)
        {
            scoremanager.ScoreLines(gameboard.clearedrows);
            if (scoremanager.DidLevelUp)
            {
                dropIntervalModded = Mathf.Clamp(dropInterval - ((float) scoremanager.level -1) * 0.08f, 0.15f, 1f);
            }
            if (gameboard.clearedrows > 1 )
            {
                AudioClip random = soundmanager.RandomVoice[Random.Range(0, soundmanager.RandomVoice.Length)];
                PlaySound(random);
            }
                PlaySound(soundmanager.ClearRowSound);
        }
    }
    public void Restart()
    {
        if (scoremanager.GetScore() > PlayerPrefs.GetInt("score", 0))
        {
            PlayerPrefs.SetInt("score", scoremanager.GetScore());
            scoremanager.SetHighScore();
        }
        if (IsButtonControl == 0)
        {
            ButtonManager.SetActive(true);
        }
        scoremanager.SetCoins();
        Time.timeScale = 1f; 
        gameboard.GameOverClear();
        scoremanager.Reset();
        if (GameOverPanel)
        {
            GameOverPanel.SetActive(false);
        }
        if (IsPaused)
        {
            PausePanel.SetActive(false);
            IsPaused = !IsPaused;
        }
        Destroy(ActiveShape.gameObject);
        ActiveShape = spawner.SpawnShape();
        ghost.Reset();
        gamestart = true;
        GameOver = false;
        dropIntervalModded = dropInterval;
        skillManager.Restart();
        soundmanager.MusicSource.volume = (IsPaused) ? soundmanager.MusicVolume * 0.25f : soundmanager.MusicVolume;
    }
    void GameOverFu()
    {
        ActiveShape.MoveUp();
       // Debug.Log(ActiveShape.name + " is over the limit");
        GameOver = true;
        gamestart = false;
        if (GameOverPanel)
        {
            GameOverPanel.SetActive(true);
        }
        if(IsButtonControl == 0)
        {
            ButtonManager.SetActive(false);
        }
        PlaySound(soundmanager.GameOverSound, 5f);
        PlaySound(soundmanager.GameOverVoice, 5f);
    }
    public void GameStartfu()
    {
        GameStartPanel.SetActive(false);
        GameMainPanel.SetActive(true);
        QueuePanel.SetActive(true);
        gameboard.DrawEmptyBoard();
        if(IsButtonControl == 0)
        {
            ButtonManager.SetActive(true);
        }
        spawner.InitQueue();
        gamestart = true;
    }
    void PlaySound(AudioClip audioclip, float x = 1f)
    {
        if (soundmanager.FxEnabled && audioclip)
        {
            AudioSource.PlayClipAtPoint(audioclip, Camera.main.transform.position, soundmanager.FxVolume * x) ;
        }
    }
    public void TogglePause()
    {
        if (GameOver)
        {
            return;
        }
        IsPaused = !IsPaused;
        if (IsButtonControl == 0)
        {
            ButtonManager.SetActive(!IsPaused);
        }
        if (PausePanel)
        {
            PausePanel.SetActive(IsPaused);
            if (soundmanager)
            {
                soundmanager.MusicSource.volume = (IsPaused) ? soundmanager.MusicVolume * 0.25f : soundmanager.MusicVolume;
            }
            Time .timeScale = (IsPaused) ? 0 : 1;
        }
    }
    public void ReturnToMenu()
    {
        if (scoremanager.GetScore() > PlayerPrefs.GetInt("score", 0))
        {
            PlayerPrefs.SetInt("score", scoremanager.GetScore());
            scoremanager.SetHighScore();
        }
        scoremanager.SetCoins();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void SwipeHandler(Vector2 SwipeMovement)
    {
        SwipeDirection = GetDirection(SwipeMovement);
    }
    void SwipeEndHandler(Vector2 SwipeMovement)
    {
        SwipeEndDirection = GetDirection(SwipeMovement);
    }
    void OnEnable()
    {
        TouchController.SwipeEvent += SwipeHandler;
        TouchController.SwipeEndEvent += SwipeEndHandler;
    }
    void OnDisable()
    {
        TouchController.SwipeEvent -= SwipeHandler;
        TouchController.SwipeEndEvent -= SwipeEndHandler;
    }
    Direction GetDirection(Vector2 SwipeMovement)
    {
        Direction SwipeDir = Direction.none;
        if(Mathf.Abs(SwipeMovement.x) > Mathf.Abs(SwipeMovement.y))
        {
            SwipeDir = (SwipeMovement.x >= 0) ? Direction.right : Direction.left;
        }
        else
        {
            SwipeDir = (SwipeMovement.y >= 0) ? Direction.up : Direction.down;
        }
        return SwipeDir;
    }
    public void TimeReduction()
    {
        if (skillManager.CanUseSkill(0) && scoremanager.UseSkill(TimeSkillCoins) && scoremanager.level > 4)
        {
            dropIntervalModded = dropIntervalModded * 2f;
            skillManager.PutOnCooldown(0);
            scoremanager.UseSkillCost(TimeSkillCoins);
        }
    }
    public void ResetQueue()
    {
        if (skillManager.CanUseSkill(2) && scoremanager.UseSkill(QueueCoins))
        {
            scoremanager.UseSkillCost(QueueCoins);
            spawner.InitQueue();
            skillManager.PutOnCooldown(2);
        }
    }
 
    public void DeleteRaw()
    {
        if(skillManager.CanUseSkill(1) && scoremanager.UseSkill(DeleteRowCoins))
        {
            scoremanager.UseSkillCost(DeleteRowCoins);
            skillManager.PutOnCooldown(1);
            DeleteRawPanel.SetActive(true);
            IsPaused = true;
            Time.timeScale = (IsPaused) ? 0 : 1;
        }
    }
    public void DeleteRawEnd()
    {
        IsPaused = !IsPaused;
        DeleteRawPanel.SetActive(false);
        Time.timeScale = (IsPaused) ? 0 : 1;
    }
    public void OpenSettings(bool IsOpened)
    {
        SettingsPanel.SetActive(IsOpened);
        GameStartPanel.SetActive(!IsOpened);
        FortuneWheelButton.SetActive(!IsOpened);
        FortuneWheelPanel.SetActive(false);
        
    }
    public void Button_Skill()
    {
        
        if (IsButtonControl == 1)
        {
            IsButtonControl = 0;
            PlayerPrefs.SetInt("Manager", IsButtonControl);
            KeyLeftRight = 0.2f;
            text.text = "Button";
        }
        else if (IsButtonControl == 0)
        {
            IsButtonControl = 1;
            PlayerPrefs.SetInt("Manager", IsButtonControl);
            KeyLeftRight = 0.05f;
            text.text = "Swipe";
        }
        // ButtonManager.SetAcrive(IsButtonControl);
        Swipe_Button_Icon.ToggleIconX(PlayerPrefs.GetInt("Manager"));
        
    }
    public void OpenFortuneWheel(bool IsOpened)
    {
        FortuneWheelPanel.SetActive(IsOpened);
        FortuneWheelButton.SetActive(!IsOpened);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
