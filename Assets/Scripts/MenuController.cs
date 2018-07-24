using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Small behaviour to handle menu button callbacks.
 */
public class MenuController : MonoBehaviour
{
    private void Start()
    {
        Screen.SetResolution(1215, 500, false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    /*
     * When the start button is pressed, load the Game scene.
     */
    public void OnLeftClicked()
  {
    SceneManager.LoadScene("Game");
  }

  public void OnRightClicked()
    {
        SceneManager.LoadScene("Game2");
    }

  /*
   * When the back button is clicked, load the Menu scene.
   */
  public void OnBackClicked()
  {
    SceneManager.LoadScene("Menu");
  }
}
