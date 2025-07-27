using UnityEngine;
using UnityEngine.SceneManagement;
public class PF_SceneLoader : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("HUD", LoadSceneMode.Additive);        
    }
}
