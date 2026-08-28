using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class setVideo : MonoBehaviour
{
    [Range(1,12)] public byte horizontalSens;
    [Range(1,12)] public byte verticalSens;
    public bool invertH;
    public bool invertV;
    public int invertIntH;
    public int invertIntV;

    private void Start()
    {
        ResetToDefault();
    }

    private void ResetToDefault()
    {
        this.horizontalSens = 5;
        this.verticalSens = 5;
        this.invertH = false;
        this.invertV = false;

        ChangeIntergers();
    }

    private void ChangeIntergers()
    {
        this.invertIntH = invertH? -1 : 1;
        this.invertIntV = invertV? -1 : 1;
    }
}
