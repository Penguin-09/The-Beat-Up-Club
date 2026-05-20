using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player1;
    public Transform player2;

    private Vector3 offset = new Vector3(0f, 0.5f, -5f);

    void Start()
    {
        
    }

    void Update()
    {
        // Position the camera inbetween the two players
        Vector3 midPoint = (player1.position + player2.position) / 2f;
        
        // Move the camera back based on the distance between the two players
        float distance = Vector3.Distance(player1.position, player2.position);
        offset.z = -3f - distance * 0.3f;

        transform.position = midPoint + offset;
    }
}
