using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class ChestController : MonoBehaviour
{
    //===================================================
    //Fields
    public Transform ItemHolder;

    Animator _animator;
    bool _opened;

    //===================================================
    //Properties
    public int ItemIndex { get; private set; }

    //===================================================
    //Events
    public static event Action<ChestController> ChestOpened;

    //===================================================
    //Methods
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _opened = false;
    }

    // OnMouseDown is called when the user has pressed the mouse button while over the GUIElement or Collider
    private void OnMouseDown()
    {
        if (!_opened)
        {
            _opened = true;
            _animator.SetBool("Open", true);

            ChestOpened?.Invoke(this);
        }
    }

    public void PlaceItem(GameObject item, int index)
    {
        item.transform.SetParent(ItemHolder);
        item.transform.localPosition = Vector3.zero;
        ItemIndex = index;
    }

    public void Close()
    {
        _opened = false;
        _animator.SetBool("Open", false);
    }
}