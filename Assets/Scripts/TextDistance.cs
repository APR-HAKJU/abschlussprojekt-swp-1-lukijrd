using UnityEngine;

public class DistanceText : MonoBehaviour
{
    public Transform player;
    public float maxDistance = 8f;

    private Canvas canvas;

    void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    void Start()
    {
        // Try to auto-find player or camera safely
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                player = p.transform;
            else if (Camera.main != null)
                player = Camera.main.transform;
        }

        // IMPORTANT: start visible so we can debug
        canvas.enabled = true;
    }

    void Update()
    {
        // If we STILL don't have a player, do nothing
        if (player == null)
            return;

        // Measure distance from the OBJECT, not the canvas
        Transform target = transform.parent != null ? transform.parent : transform;

        float distance = Vector3.Distance(player.position, target.position);

        canvas.enabled = distance <= maxDistance;
    }
}
