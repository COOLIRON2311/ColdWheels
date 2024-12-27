using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalCanvasExampleScript : MonoBehaviour
{
    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
}
