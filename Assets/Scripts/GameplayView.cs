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
    [SerializeField] private CellView[] cells;
    [Space(10), Header("Audio")]
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioClip sfxClick;
    [SerializeField] private AudioClip sfxWin;
    [SerializeField] private AudioClip sfxLose;

    private void OnEnable()
    {
        quitButton.onClick.AddListener(ExitGame);
        foreach (var cell in cells)
        {
            cell.OnCellClicked += OnCellClickHandler;
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
        gameplayController.Setup(this);
        modalWin.SetActive(false);
        modalLose.SetActive(false);
        modalDraw.SetActive(false);
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

    private void ExitGame()
    {
        PlaySFX(sfxClick);
        gameObject.SetActive(false);
        welcomeView.gameObject.SetActive(true);
    }

    private void PlaySFX(AudioClip clip)
    {
        sfxAudioSource.PlayOneShot(clip);
    }

    private void OnCellClickHandler(int cellNumber)
    {
        PlaySFX(sfxClick);
        Debug.Log($"Clicked on {cellNumber}");
        gameplayController.MarkSpecificCell(cellNumber, Mark.X);
    }
}
