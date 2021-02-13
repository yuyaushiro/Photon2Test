using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 velocity;

    public void Init(Vector3 origin, Vector3 direction)
    {
        transform.position = origin;
        velocity = 9f * direction;
    }

    private void Update()
    {
        transform.Translate(velocity * Time.deltaTime);
    }

    // 画面外に移動したら削除する
    // （Unityのエディター上では、シーンピューの画面も影響するので注意）
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
