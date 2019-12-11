using UnityEngine;

public class ItemsAndChestsContainer : MonoBehaviour
{
    public GameObject[] Items;
    public ChestController[] Chests;

    // Start is called before the first frame update
    void Start()
    {
        bool valid = Items.Length != 0 && Items.Length << 1 == Chests.Length;

        if (!valid)
            Debug.LogError("GameManager: There should be twice as many chests as items");
    }
}