using UnityEngine;
using UnityEngine.SceneManagement;

/*
    there are a lot of ways to pass the information from one scene to the other,
    i could've created a static class to store the name of the animation
    i could've used an scriptable object to do the same

    i opted to flag the character to dont destroy on load, this way i don't spend resources creating a new one
    and the value is already stored in the animator so i don't have to worry about it 
*/
public class LoadSceneByName : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    public void LoadScene()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
