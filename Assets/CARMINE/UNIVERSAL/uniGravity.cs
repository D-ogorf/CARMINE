using UnityEngine;
using UnityEngine.Rendering;

public class uniGravity : MonoBehaviour
{
    const float gravity = 250;
    private Rigidbody r;

    private void Start()
    {
        this.r = this.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        this.r.AddForce(gravity * -this.transform.up);
    }
}
