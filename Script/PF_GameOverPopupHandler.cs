using UnityEngine;
using UnityEngine.UI;
public class PF_GameOverPopupHandler : MonoBehaviour
{
    [SerializeField] Button replayButton;
    [SerializeField] Button titleButton;
    void Awake()
    {
        replayButton.onClick.AddListener(ExcuteReplayEvent);
        titleButton.onClick.AddListener(ExcuteTitleEvent);
        Debug.Log("이벤트 할당 됨");
    }

    void ExcuteReplayEvent()
    {
        PF_GameManager.Instance.GameStart();
    }

    void ExcuteTitleEvent()
    {
        PF_GameManager.Instance.TitleScreen();
    }

    void OnDisable()
    {
        replayButton.onClick.RemoveListener(ExcuteReplayEvent);
        titleButton.onClick.RemoveListener(ExcuteTitleEvent);
        Debug.Log("이벤트 해제 됨");
    }
}
