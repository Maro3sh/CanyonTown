using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{

    [SerializeField] private string sceneToLoad1;
    [SerializeField] private string sceneToLoad2;


    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject resume;

    void Start()
    {
        if (PauseMenu != null)
        {
            PauseMenu.SetActive(false);
        }
         Time.timeScale = 1f;


    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    public void SwitchScene1()
    {
        SceneManager.LoadScene(sceneToLoad1);
         Time.timeScale = 1f;
    }

    public void SwitchScene2()
    {
        SceneManager.LoadScene(sceneToLoad2);
        Time.timeScale = 1f;
    }
    public void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;

    }
    public void QuitGame(){
        Application.Quit();
    }
}
