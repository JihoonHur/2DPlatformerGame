using UnityEngine;
using UnityEngine.SceneManagement;

public class PF_Title : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
        {
            //PF_GameManager.Instance.ResetStege();
            SceneManager.LoadScene(1); // 실제 게임 씬 이름으로 변경
        }
    }
}
