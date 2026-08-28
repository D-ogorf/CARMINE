using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class mchCamera : MonoBehaviour
{
    [SerializeField] private float horizontalMov;
    [SerializeField] private float verticalMov;
    const byte addSens = 35;
    private setVideo s;
    private GameObject p;
    void Start()
    {
        this.s = GameObject.Find("s").GetComponent<setVideo>();
        this.p = GameObject.Find("mchCollision");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        UpdateCameraMov();
        RotateCamera();
    }

    private void UpdateCameraMov()
    {
        this.horizontalMov += Time.deltaTime * Input.GetAxis("Mouse X") * s.horizontalSens * addSens * s.invertIntH;
        this.verticalMov -= Time.deltaTime * Input.GetAxis("Mouse Y") * s.verticalSens * addSens * s.invertIntV;
    }

    private void RotateCamera()
    {
        this.transform.localEulerAngles = new Vector3(this.verticalMov, 0, 0);
        this.verticalMov = Mathf.Clamp(this.verticalMov, -90, 85);
        this.p.transform.localEulerAngles = new Vector3(0, this.horizontalMov, 0);
    }
}
