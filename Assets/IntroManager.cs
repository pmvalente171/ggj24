using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    public Sprite screenShot2;
    public Image image;
    
    IEnumerator Start()
    {
        yield return new WaitForSeconds(11f);
        image.sprite = screenShot2;
        
        // Load scene after 7 seconds
        yield return new WaitForSeconds(6f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }
}
