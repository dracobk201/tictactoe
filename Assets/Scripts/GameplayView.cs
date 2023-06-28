using UnityEngine;
using UnityEngine.UI;

public class GameplayView : MonoBehaviour
{
    [SerializeField] private WelcomeView welcomeView;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject modalWin;
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

    }

    private void ShowWinner()
    {
        PlaySFX(sfxWin);
        modalWin.SetActive(true);
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
    }
}
