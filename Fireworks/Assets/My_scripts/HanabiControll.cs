using UnityEngine;
using System.Collections;

public class DetachOnCondition : MonoBehaviour
{

    public HingeJoint hingeJoint;

    public float thresholdVelocity = 5.0f;

    public FurikoLine furikoLineScript;
    private Rigidbody rb;


    public GameObject prefabToRespawn;

    public float respawnTime = 3.0f;

    private Vector3 initialPosition;

    private Quaternion initialRotation;

	public ParticleSystem particleSystem2;
	public ParticleSystem particleSystem3;
	public ParticleSystem particleSystem4;
	public ParticleSystem particleSystem5;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (hingeJoint == null)
            hingeJoint = GetComponent<HingeJoint>();
        if (furikoLineScript == null)
            furikoLineScript = GetComponent<FurikoLine>();

        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        if (rb.linearVelocity.magnitude > thresholdVelocity)
            DetachAndDrop();
    }

    private void DetachAndDrop()
    {
        if (hingeJoint == null)
            return;
            
        transform.parent = null;

        hingeJoint.breakForce = 0;
        hingeJoint = null;

        if (furikoLineScript != null)
            furikoLineScript.DetachLine();

		if (particleSystem2 != null)
		{
			particleSystem2.Stop();
		}
		if (particleSystem3 != null)
		{
			particleSystem3.Stop();
		}
		if (particleSystem4 != null)
		{
			particleSystem4.Stop();
		}
		if (particleSystem5 != null)
		{
			particleSystem5.Stop();
		}

        StartCoroutine(RespawnAfterDelay());
        
        this.enabled = false;
    }

    private System.Collections.IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnTime);

        Instantiate(prefabToRespawn, initialPosition, initialRotation);

        Destroy(gameObject);
    }
}
