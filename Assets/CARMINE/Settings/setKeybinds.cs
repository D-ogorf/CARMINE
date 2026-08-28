using UnityEngine;

public class setKeybinds : MonoBehaviour
{
    public KeyCode forward;
    public KeyCode backward;
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;

    private void Start()
    {
        DefaultKeybinds();
    }

    public void DefaultKeybinds()
    {
        this.forward = KeyCode.W;
        this.backward = KeyCode.S;
        this.left = KeyCode.A;
        this.right = KeyCode.D;
        this.jump = KeyCode.Space;
    }
}