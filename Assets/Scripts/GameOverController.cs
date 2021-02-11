using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public Button Exit;
    public Button go;
    public string SceneName;


    private void Start()
    {
        go.onClick.AddListener(AtClic);
        Exit.onClick.AddListener(exit);

    }
    private void AtClic()
    {
        SceneManager.LoadScene(SceneName);
    }

    private void exit()
    {
        Application.Quit();
    }

}
