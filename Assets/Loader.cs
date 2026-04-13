using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public PlayableDirector director;
    private bool loaded = false;

    void Update()
    {
        if (!loaded && director.state != PlayState.Playing)
        {
            loaded = true;
            SceneManager.LoadScene("SampleScene");
        }
    }
}