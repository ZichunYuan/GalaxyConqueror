
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenuControl : MonoBehaviour
{
    public void Play01()
    {
        SceneManager.LoadScene("Level01");
       
        //TODO: SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Play02()
    {
        SceneManager.LoadScene("Level02");

        //TODO: SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Play03()
    {
        SceneManager.LoadScene("Level03");

        //TODO: SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
