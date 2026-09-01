using UnityEngine;

public class uniGroundCheck : MonoBehaviour
{
    public bool _isGrounded;

    private void OnTriggerEnter(Collider other)
    {
        this._isGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        this._isGrounded = false;
    }
}
