using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float ChestCloseOnFailDelay;

    WaitForSeconds _delay;
    Coroutine _chestsClosing;
    ChestController _firstChest;

    // Start is called just before any of the Update methods is called the first time
    private void Start()
    {
        _delay = new WaitForSeconds(ChestCloseOnFailDelay);

        ChestController.ChestOpened += HandleChestOpening;
    }

    // This function is called when the MonoBehaviour will be destroyed
    private void OnDestroy()
    {
        ChestController.ChestOpened -= HandleChestOpening;
    }

    void HandleChestOpening(ChestController chest)
    {
        if(_chestsClosing != null)//If currently other chests are on delay to close
        {
            chest.Close();//Immediately
            return;
        }

        if(_firstChest == null)
        {
            _firstChest = chest;
            return;
        }
        else/*if(_lastOpenedChest != null)*/
        {
            if(_firstChest.ItemIndex == chest.ItemIndex)
            {
                _firstChest = null;
            }
            else
            {
                _chestsClosing = StartCoroutine(CloseChestsAfterDelay(chest));
            }
        }
    }

    IEnumerator CloseChestsAfterDelay(ChestController secondChest)
    {
        yield return _delay;

        _firstChest.Close();
        secondChest.Close();

        _firstChest = null;
        _chestsClosing = null;
    }
}