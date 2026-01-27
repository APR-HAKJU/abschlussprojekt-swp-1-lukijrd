using UnityEngine;

public class DoorKeyScript : MonoBehaviour
{
    public string requiredKeyID;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();

            if (inventory != null && inventory.HasKey(requiredKeyID))
            {
                OpenDoor();
            }
        }
    }

    void OpenDoor()
    {
        Destroy(gameObject);
    }
}
