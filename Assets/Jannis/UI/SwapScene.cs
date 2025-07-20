using UnityEngine;
using UnityEngine.SceneManagement;


public class SwapScene : MonoBehaviour
{


    public void SwapByBuildIndex(int buildIndex)
    {
        var scene = SceneManager.GetSceneByBuildIndex(buildIndex);
        SceneManager.SetActiveScene(scene);
    }
}
