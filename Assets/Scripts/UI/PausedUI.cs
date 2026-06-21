using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PausedUI : MonoBehaviour
{
   [SerializeField] private Button resumeButton;
   [SerializeField] private Button mainMenuButton;
   [SerializeField] private Button soundVolumeButton;
   [SerializeField] private Button musicVolumeButton;
   [SerializeField] private TextMeshProUGUI musicVolumeTextMesh;
   [SerializeField] private TextMeshProUGUI soundVolumeTextMesh;
  
   
   private void Awake()
    {
        soundVolumeButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeSoundVolume();
            soundVolumeTextMesh.text = "SOUND " + SoundManager.Instance.GetSoundVolume();
        });
        musicVolumeButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeMusicVolume();
            musicVolumeTextMesh.text = "MUSIC " + MusicManager.Instance.GetMusicVolume();
        });

        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.UnPauseGame();
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.Scene.MainMenuScene);
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;
        
        soundVolumeTextMesh.text = "SOUND " + SoundManager.Instance.GetSoundVolume();
        musicVolumeTextMesh.text = "MUSIC " + MusicManager.Instance.GetMusicVolume();
        Hide();
        
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }
    private void GameManager_OnGameUnPaused(object sender, System.EventArgs e)
    {
        Hide();
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
