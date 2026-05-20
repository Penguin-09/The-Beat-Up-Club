using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    private Vector3 velocity;

    public float smoothTime = 0.2f;

    void LateUpdate()
    {
        // Calculate the midpoint between the two players
        Vector3 midPoint = (player1.position + player2.position) / 2f;

        // Move the camera away based on the distance between the players
        float distance = Vector3.Distance(player1.position, player2.position);
        
        Vector3 targetPosition = midPoint + new Vector3(0f, 0f, -3f - distance * 0.3f);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}