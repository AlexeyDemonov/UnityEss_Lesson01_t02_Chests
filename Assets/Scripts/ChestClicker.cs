using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class ChestClicker : MonoBehaviour
{
    Animator _animator;
    bool _opened;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _opened = false;
    }

    // OnMouseDown is called when the user has pressed the mouse button while over the GUIElement or Collider
    private void OnMouseDown()
    {
        _opened = !_opened;
        _animator.SetBool("Open", _opened);
    }
}