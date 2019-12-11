using System.Collections.Generic;
using UnityEngine;

public class ItemsToChestsDispenser : MonoBehaviour
{
    public ItemsAndChestsContainer Container;

    // Start is called before the first frame update
    void Start()
    {
        List<ChestController> chests = new List<ChestController>(Container.Chests);
        GameObject[] items = Container.Items;

        int chestsToItemsRatio = chests.Count / items.Length;

        for (int itemIndex = 0; itemIndex < items.Length; itemIndex++)
        {
            for (int _ = 0; _ < chestsToItemsRatio; _++)
            {
                var itemClone = Instantiate(items[itemIndex]);
                int randomChestIndex = Random.Range(0, chests.Count);
                chests[randomChestIndex].PlaceItem(item: itemClone, index: itemIndex);
                chests.RemoveAt(randomChestIndex);
            }
        }       
    }
}