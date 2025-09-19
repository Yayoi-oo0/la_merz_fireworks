// using UnityEngine;

// public class FurikoParticleController : MonoBehaviour
// {
//     public ParticleSystem particleSystem; 
    
//     private void OnTriggerEnter(Collider other)
//     {c
//         if (other.CompareTag("Candle"))
//             particleSystem.Play();
//     }
// }
using UnityEngine;
using TMPro; // TMPを扱うために必要

public class FurikoParticleController : MonoBehaviour
{
    // パーティクルシステムをインスペクターから設定
    public ParticleSystem particleSystem; 
    // 表示するText (TMP)コンポーネントをインスペクターから設定
    public TextMeshProUGUI debugText;
	public AudioSource audioSource;

    // 接触時に呼び出されるメソッド
    private void OnTriggerEnter(Collider other)
    {
        // 接触したオブジェクトのタグをチェック
        if (other.CompareTag("Candle"))
        {
            // パーティクルを再生
            particleSystem.Play();

			if (audioSource != null && audioSource.clip != null)
			{
				audioSource.Play();
			}
            // デバッグ用の文字をUIに表示
            if (debugText != null)
            {
                debugText.text = "Hit!";
            }
        }
    }

    // (補足) 接触が離れたときに文字を消したい場合
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Candle"))
        {
            if (debugText != null)
            {
                debugText.text = "testing..."; // 空欄に戻す
            }
        }
    }
}
