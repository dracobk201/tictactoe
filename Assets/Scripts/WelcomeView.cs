using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeView : MonoBehaviour
{
    [Header("Editor References")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private GameplayView gameplayView;

    [Space(10), Header("Settings")]
    [SerializeField] private AudioClip sfxClick;
    [SerializeField] private float delay = 0.4f;

    private void OnEnable()
    {
        playButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(QuitGame);
        gameplayView.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveListener(PlayGame);
        quitButton.onClick.RemoveListener(QuitGame);
    }

    private void PlayGame()
    {
        PlayButtonSound();
        gameObject.SetActive(false);
        gameplayView.gameObject.SetActive(true);
    }

    private void QuitGame()
    {
        PlayButtonSound();
        Invoke(nameof(CloseApp), delay);
    }

    private void CloseApp()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();    
        #endif
    }

    private void PlayButtonSound()
    {
        sfxAudioSource.PlayOneShot(sfxClick);
    }
}
