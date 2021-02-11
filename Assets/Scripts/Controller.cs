using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public Button butonStart;
    public Button Exit;
    public Button go;
    public string SceneName;
    public string SceneName2;


    private void Start()
    {
        butonStart.onClick.AddListener(AtClic);
        go.onClick.AddListener(AtClic2);
        Exit.onClick.AddListener(exit);
    }
    private void AtClic()
    {
        SceneManager.LoadScene(SceneName);
    }

    private void AtClic2()
    {
        SceneManager.LoadScene(SceneName2);
    }

    private void exit()
    {
        Application.Quit();
    }
}
