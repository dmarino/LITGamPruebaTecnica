using UnityEngine;

//every object with this script won't be destroyed between the change of scenes
public class DDOL : MonoBehaviour
{
    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
    }
}
