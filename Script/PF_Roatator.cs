using UnityEngine;

public class PF_Roatator : MonoBehaviour
{
    public float rotationSpeed = 90f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // z축 기준 회전 (2D)
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
