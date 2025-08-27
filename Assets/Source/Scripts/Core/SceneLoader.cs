using UnityEngine.SceneManagement;

public class SceneLoader 
{
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
