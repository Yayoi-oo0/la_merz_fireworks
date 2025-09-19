using UnityEngine;

public class FurikoLine : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;

	private bool isAttached = true;

    void Update()
    {
        if (isAttached)
        {
            var positions = new Vector3[] { startPoint.position, endPoint.position, };
            lineRenderer?.SetPositions(positions);
        }
    }

	public void DetachLine()
	{
		isAttached = false;
	}
}
