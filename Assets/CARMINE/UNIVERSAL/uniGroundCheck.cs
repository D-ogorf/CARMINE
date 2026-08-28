using UnityEngine;

public class uniGroundCheck : MonoBehaviour
{
    public bool _isGrounded;

    private void OnTriggerEnter(Collider other)
    {
        _isGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _isGrounded = false;
    }
}
