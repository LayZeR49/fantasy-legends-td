using UnityEngine.SceneManagement;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource BGMMain;
    public AudioSource BGMFight;
    public string MainMenuScene = "MainMenu";
    public string LevelSelectScene = "LevelSelect";

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        BGMMain.ignoreListenerPause = true;
        BGMFight.ignoreListenerPause = true;
    }

    void OnEnable() 
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }
 
    void OnDisable() 
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }
 
    private void OnSceneLoad(Scene scene, LoadSceneMode mode) {
        Debug.Log(scene.name);
        if(scene.name == MainMenuScene || scene.name == LevelSelectScene)
        {
            StopBGMFight();
            PlayBGMMain();
        }
        else
        {
            StopBGMMain();
            PlayBGMFight();
        }
    }
 
    public void PlayBGMMain()
    {
        if (BGMMain.isPlaying) return;
        BGMMain.Play();
    }
 
    public void StopBGMMain()
    {
        BGMMain.Stop();
    }

    public void PlayBGMFight()
    {
        if (BGMFight.isPlaying) return;
        BGMFight.Play();
    }
 
    public void StopBGMFight()
    {
        BGMFight.Stop();
    }
}
