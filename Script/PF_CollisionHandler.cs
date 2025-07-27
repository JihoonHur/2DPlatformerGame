using UnityEngine;
using UnityEngine.SceneManagement;

public class PF_CollisionHandler : MonoBehaviour
{
    public enum CollisionType
    {
        Gold,
        Chest,
        Spike,
        Enermy,
        Water
    }

    [SerializeField] private CollisionType collisionType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (collisionType)
        {
            case CollisionType.Gold:
                Debug.Log("골드 획득");
                PF_GameManager.Instance.IncreaseGold();
                Destroy(this.gameObject);
                break;
            case CollisionType.Chest:
                Debug.Log("골 도착");
                PF_GameManager.Instance.NextStage();
                PF_GameManager.Instance.Initialized();
                break;
            case CollisionType.Spike:
                Debug.Log("스파이크에 닿음");
                PF_GameManager.Instance.DecreaseBonus();
                PF_GameManager.Instance.SpawnPlayer();
                break;
            case CollisionType.Enermy:
                PF_GameManager.Instance.DecreaseBonus();
                PF_GameManager.Instance.SpawnPlayer();                
                Debug.Log("적과 충돌");
                break;
            case CollisionType.Water:
                PF_GameManager.Instance.DecreaseBonus();
                PF_GameManager.Instance.SpawnPlayer();
                Debug.Log("물과 충돌");
                break;
        }

    }
}
