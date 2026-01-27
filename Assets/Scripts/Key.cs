using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyID; // Unique ID for this key

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.AddKey(keyID);
                Destroy(gameObject); // Remove key after pickup
            }
        }
    }
}
