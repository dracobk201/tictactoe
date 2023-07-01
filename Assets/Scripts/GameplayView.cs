using UnityEngine;
using UnityEngine.UI;

public class GameplayView : MonoBehaviour
{
    [SerializeField] private GameplayController gameplayController;
    [SerializeField] private WelcomeView welcomeView;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject modalWin;
    [SerializeField] private GameObject modalLose;
    [SerializeField] private GameObject modalDraw;
    [SerializeField] private Image modalActualTurn;
    [SerializeField] private CellView[] cells;
    [SerializeField] private Sprite markXSprite;
    [SerializeField] private Sprite markOSprite;
    [SerializeField] private Sprite turnXSprite;
    [SerializeField] private Sprite turnOSprite;
    [Space(10), Header("Audio")]
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioClip sfxClick;
    [SerializeField] private AudioClip sfxWin;
    [SerializeField] private AudioClip sfxLose;
    private static Color transparent = new Color(1f, 1f, 1f, 0f);
    private static Color opaque = new Color(1f, 1f, 1f, 1f);

    private void OnEnable()
    {
        quitButton.onClick.AddListener(ExitGame);
        foreach (var cell in cells)
        {
            cell.OnCellClicked += OnCellClickHandler;
            cell.SetSprite(markXSprite);
        }
        InitGame();
    }

    private void OnDisable()
    {
        quitButton.onClick.RemoveListener(ExitGame);
        foreach (var cell in cells)
        {
            cell.OnCellClicked -= OnCellClickHandler;
        }
    }

    public void InitGame()
    {
        modalWin.SetActive(false);
        modalLose.SetActive(false);
        modalDraw.SetActive(false);
        modalActualTurn.color = transparent;
        gameplayController.Setup(this);
    }

    public void ShowWinner()
    {
        PlaySFX(sfxWin);
        modalWin.SetActive(true);
    }

    public void ShowLoser()
    {
        PlaySFX(sfxLose);
        modalLose.SetActive(true);
    }

    public void ShowDraw()
    {
        PlaySFX(sfxClick);
        modalDraw.SetActive(true);
    }

    public void ShowCurentTurn(Mark currentTurn)
    {
        modalActualTurn.color = opaque;
        if (currentTurn.Equals(Mark.X))
        {
            modalActualTurn.sprite = turnXSprite;
        }
        else if (currentTurn.Equals(Mark.O))
        {
            modalActualTurn.sprite = turnOSprite;
        }
    }

    private void ExitGame()
    {
        PlaySFX(sfxClick);
        gameObject.SetActive(false);
        welcomeView.gameObject.SetActive(true);
    }

    public void ActivateCell(int index)
    {
        cells[index].SetSprite(markOSprite);
        cells[index].OnButtonClicked();
    }

    private void PlaySFX(AudioClip clip)
    {
        sfxAudioSource.PlayOneShot(clip);
    }

    private void OnCellClickHandler(int cellNumber)
    {
        PlaySFX(sfxClick);
        gameplayController.MarkSpecificCell(cellNumber, Mark.X);
    }
}
