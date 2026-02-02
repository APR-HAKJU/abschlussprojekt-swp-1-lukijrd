using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class platforms : MonoBehaviour
{
    [SerializeField] private Renderer cubeRenderer;
    [SerializeField] private Vector3 textSpawnPosition = Vector3.zero;
    
    // Static list to track which platforms have been stepped on
    private static List<GameObject> platformsSteppedOn = new List<GameObject>();
    private static GameObject currentRedPlatform = null;
    private static bool allCubesPressed = false;

    public GameObject zahl;

    private TextMeshPro zahlText;

    // Color definitions
    private Color redColor = Color.red;
    private Color whiteColor = Color.white;

    void Start()
    {
        // Get this cube's renderer if not assigned
        if (cubeRenderer == null)
        {
            cubeRenderer = GetComponent<Renderer>();
        }

        zahlText = zahl.GetComponent<TextMeshPro>();
        
        
        // Initialize on first platform found
        if (platformsSteppedOn.Count == 0)
        {
            // Set the first platform to red
            GameObject[] allPlatforms = GameObject.FindGameObjectsWithTag("Platform");
            if (allPlatforms.Length > 0)
            {
                // Sort platforms to ensure consistent ordering
                System.Array.Sort(allPlatforms, (a, b) => a.name.CompareTo(b.name));
                
                currentRedPlatform = allPlatforms[0];
                Renderer firstRenderer = currentRedPlatform.GetComponent<Renderer>();
                if (firstRenderer != null)
                {
                    firstRenderer.material.color = redColor;
                }
            }
            
            zahlText.text = "";
        }
    }

    void Update()
    {
        
    }

    // Called when player steps on the cube
    private void OnTriggerEnter(Collider collision)
    {
        // Check if it's the player
        if (collision.gameObject.CompareTag("Player") && !allCubesPressed)
        {
            Debug.Log("Player collision detected on: " + gameObject.name);
            
            // Check if this is the current red platform
            if (gameObject == currentRedPlatform && cubeRenderer != null)
            {
                Debug.Log("Stepping on current red platform!");
                
                // Turn current platform white
                cubeRenderer.material.color = whiteColor;
                platformsSteppedOn.Add(gameObject);
                
                // Check if all platforms have been stepped on
                GameObject[] allPlatforms = GameObject.FindGameObjectsWithTag("Platform");
                if (platformsSteppedOn.Count >= allPlatforms.Length)
                {
                    // All platforms have been stepped on
                    allCubesPressed = true;
                    Debug.Log("All platforms stepped on! Level complete!");
                    zahlText.text = "4";
                    
                }
                else
                {
                    // Find and activate the next platform
                    ActivateNextPlatform(allPlatforms);
                }
            }
            else if (gameObject != currentRedPlatform && cubeRenderer != null)
            {
                Debug.Log("Attempted to step on non-red platform: " + gameObject.name);
                // Player stepped on wrong platform - could add penalty here
            }
        }
    }
    
    // Find and activate the next platform that hasn't been stepped on
    private void ActivateNextPlatform(GameObject[] allPlatforms)
    {
        // Sort platforms to ensure consistent ordering
        System.Array.Sort(allPlatforms, (a, b) => a.name.CompareTo(b.name));
        
        foreach (GameObject platform in allPlatforms)
        {
            if (!platformsSteppedOn.Contains(platform))
            {
                currentRedPlatform = platform;
                Renderer nextRenderer = platform.GetComponent<Renderer>();
                if (nextRenderer != null)
                {
                    nextRenderer.material.color = redColor;
                    Debug.Log("Next platform activated: " + platform.name);
                }
                return;
            }
        }
    }
}