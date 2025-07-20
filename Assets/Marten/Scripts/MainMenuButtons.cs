using Marten.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    
    public void Play()
    {
        SceneManager.LoadScene((int)Scenes.Level);
    }
    
    public void Options()
    {
        optionsMenu.SetActive(true);
    }

    public void OptionsBack()
    {
        optionsMenu.SetActive(false);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
