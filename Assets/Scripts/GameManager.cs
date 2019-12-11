using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //===================================================
    //Fields
    public float ChestCloseOnFailDelay;

    WaitForSeconds _closeDelay;
    Coroutine _chestsClosing;
    ChestController _firstChest;

    //===================================================
    //Properties
    public bool GameIsRunning { get; set; } = true;

    //===================================================
    //Events
    public event Action<bool> PlayerMovedSuccessfully;

    //===================================================
    //Methods
    // Start is called just before any of the Update methods is called the first time
    private void Start()
    {
        _closeDelay = new WaitForSeconds(ChestCloseOnFailDelay);

        ChestController.ChestOpened += HandleChestOpening;
    }

    // This function is called when the MonoBehaviour will be destroyed
    private void OnDestroy()
    {
        ChestController.ChestOpened -= HandleChestOpening;
    }

    void HandleChestOpening(ChestController chest)
    {
        if (_chestsClosing != null || !GameIsRunning)//If currently other chests are on delay to close or game ended
        {
            chest.Close();//Immediately
            return;
        }

        if (_firstChest == null)
        {
            _firstChest = chest;
        }
        else/*if(_firstChest != null)*/
        {
            if (_firstChest.ItemIndex == chest.ItemIndex)
            {
                _firstChest = null;
                PlayerMovedSuccessfully?.Invoke(true);
            }
            else
            {
                _chestsClosing = StartCoroutine(CloseChestsAfterDelay(chest));
                PlayerMovedSuccessfully?.Invoke(false);
            }
        }
    }

    IEnumerator CloseChestsAfterDelay(ChestController secondChest)
    {
        yield return _closeDelay;

        _firstChest.Close();
        secondChest.Close();

        _firstChest = null;
        _chestsClosing = null;
    }
}