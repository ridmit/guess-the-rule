using UnityEngine;

public class DisableCatchBlockInVoid : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("CatchBlock"))
        {
            other.gameObject.SetActive(false);
        }
    }
}