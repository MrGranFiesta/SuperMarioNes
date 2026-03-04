using System.Collections;
using UnityEngine;

public class LaunchCoinAnimation : MonoBehaviour
{
    protected Vector3 _posInitial;

    public void Awake()
    {
        _posInitial = transform.position;
    }

    void Start()
    {
        StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation()
    {
        Vector3 posTarget = transform.position + Vector3.up * 3.5f;
        while (transform.position != posTarget)
        {
            MoveTowards(posTarget);
            yield return null;
        }

        while (transform.position != _posInitial)
        {
            MoveTowards(_posInitial);
            yield return null;
        }

        Destroy(gameObject);
    }

    private void MoveTowards(Vector3 posTarget)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            posTarget,
            GameConstants.LaunchCoinAnim * Time.deltaTime
        );
    }
}
