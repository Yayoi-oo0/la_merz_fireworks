using UnityEngine;
using TMPro;
using System.Collections;

public class FurikoParticleController : MonoBehaviour
{
    public ParticleSystem particleSystem;
	public ParticleSystem particleSystem2;
	public ParticleSystem particleSystem3;
	public ParticleSystem particleSystem4;
	public ParticleSystem particleSystem5;

	//debug
    //public TextMeshProUGUI debugText;

	//timer
	public TextMeshProUGUI timerText;
	float count = 0.0f;
	private bool isTimerRunning = false;

	public AudioSource audioSource;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Candle"))
        {
            particleSystem.Play();

			if (particleSystem2 != null)
			{
				particleSystem2.Play();
				StartCoroutine(PlayParticle3AfterDelay());
			}
			if (audioSource != null && audioSource.clip != null)
			{
				audioSource.Play();
			}
			//debug
            // if (debugText != null)
            // {
            //     debugText.text = "Hit!";
            // }
			if (!isTimerRunning)
			{
				isTimerRunning = true;
				StartCoroutine(UpdateTimer());
			}
        }
    }

	private IEnumerator UpdateTimer()
	{
		while (isTimerRunning)
		{
			count += Time.deltaTime;
			if (timerText != null)
			{
				timerText.text = "elapsed time: " + count.ToString("F2") + " s";
			}
			yield return null;
		}
	}

	public void StopTimer()
	{
		isTimerRunning = false;
	}

	private IEnumerator PlayParticle3AfterDelay()
	{
		yield return new WaitForSeconds(30f);
		if (particleSystem3 != null)
		{
			particleSystem3.Play();
			StartCoroutine(PlayParticle4AfterDelay());
		}
	}

    private IEnumerator PlayParticle4AfterDelay()
    {
        yield return new WaitForSeconds(30f);
        if (particleSystem4 != null)
        {
            particleSystem4.Play();
			StartCoroutine(PlayParticle5AfterDelay());
        }
    }

    private IEnumerator PlayParticle5AfterDelay()
    {
        yield return new WaitForSeconds(30f);
        if (particleSystem5 != null)
        {
            particleSystem5.Play();
        }
    }

	//debug
    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag("Candle"))
    //     {
    //         if (debugText != null)
    //         {
    //             debugText.text = "testing...";
    //         }
    //     }
    // }
}
