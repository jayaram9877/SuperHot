
using UnityEngine;

public class LedgeGrabber : MonoBehaviour
{
    public bool hanging = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("wall"))
        {
            hanging = true;
        }
    }
}
