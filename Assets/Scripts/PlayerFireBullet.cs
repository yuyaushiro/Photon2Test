using UnityEngine;
using Photon.Pun;

public class PlayerFireBullet : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Bullet bulletPrefab = default;

    private void Update()
    {
        if (photonView.IsMine)
        {
            // 左クリックでカーソル方向に弾を発射する
            if (Input.GetMouseButtonDown(0))
            {
                Camera mainCamera = Camera.main;
                var distance = Vector3.Distance(transform.position, mainCamera.gameObject.transform.position);
                var mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
                var currentPosition = mainCamera.ScreenToWorldPoint(mousePosition);
                var direction = currentPosition - transform.position;

                FireBullet(direction);
            }
        }
    }

    // 弾を発射するメソッド
    private void FireBullet(Vector3 direction)
    {
        var bullet = Instantiate(bulletPrefab);
        bullet.Init(transform.position, direction);
    }
}
