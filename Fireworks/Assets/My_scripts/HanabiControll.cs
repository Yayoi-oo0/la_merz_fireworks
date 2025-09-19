using UnityEngine;

public class DetachOnCondition : MonoBehaviour
{
    // HingeJoint
    public HingeJoint hingeJoint;
    // 閾値となる速度
    public float thresholdVelocity = 5.0f;

    // FurikoLineスクリプト
    public FurikoLine furikoLineScript;
    private Rigidbody rb;

    // 再生成用のプレハブを設定
    public GameObject prefabToRespawn;
    // 再生成までの時間
    public float respawnTime = 3.0f;
    // 初期位置を保存
    private Vector3 initialPosition;
    // 初期回転を保存
    private Quaternion initialRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (hingeJoint == null)
            hingeJoint = GetComponent<HingeJoint>();
        if (furikoLineScript == null)
            furikoLineScript = GetComponent<FurikoLine>();

        // オブジェクトの初期位置と回転を保存
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // 速度が閾値を超えたらジョイントを切り離す
        if (rb.linearVelocity.magnitude > thresholdVelocity)
            DetachAndDrop();
    }

    private void DetachAndDrop()
    {
        // 既に切り離し処理が実行済みなら、再度実行しない
        if (hingeJoint == null)
            return;
            
        // 親子関係を解除
        transform.parent = null;

        // HingeJointを破断
        hingeJoint.breakForce = 0;
        hingeJoint = null; // ジョイントが切れたことを示す

        if (furikoLineScript != null)
            furikoLineScript.DetachLine();

        // 3秒後に再生成するコルーチンを開始
        StartCoroutine(RespawnAfterDelay());
        
        // このスクリプトはもう不要なので無効化
        this.enabled = false;
    }

    private System.Collections.IEnumerator RespawnAfterDelay()
    {
        // 指定時間待機
        yield return new WaitForSeconds(respawnTime);

        // 新しいインスタンスを生成
        Instantiate(prefabToRespawn, initialPosition, initialRotation);
        
        // 現在のオブジェクトを破棄
        Destroy(gameObject);
    }
}
