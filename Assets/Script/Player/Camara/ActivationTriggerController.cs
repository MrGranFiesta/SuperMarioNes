using UnityEngine;

public class ActivationTriggerController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IActivable go))
        {
            go.OnActivate();
        }
    }
}
