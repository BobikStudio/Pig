using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] [Min(0)] private float _viewRadius;
    [SerializeField] [Min(0.01f)] private float _scanDelay = 0.1f;

    [SerializeField] private LayerMask _obstacles;
    [SerializeField] private LayerMask _ignoreLayers;
    [SerializeField] private LayerMask _targets;

    [HideInInspector] public List<GameObject> VisibleTargets = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(FindTargetsWithDelay(_scanDelay));
    }

    private void FindVisibleTargets()
    {
        VisibleTargets.Clear();

        Collider2D[] viewColliders = Physics2D.OverlapCircleAll(transform.position, _viewRadius, _targets);

        foreach (Collider2D collider in viewColliders)
        {
            if (Physics2D.Linecast(transform.position, collider.transform.position, ~_ignoreLayers))
            {
                RaycastHit2D hit = Physics2D.Linecast(transform.position, collider.transform.position, ~_ignoreLayers);
                if (hit.collider == collider)
                {
                    VisibleTargets.Add(hit.collider.gameObject);
                }
            }
        }
    }

    private IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }
}
