using Photon.Pun;
using UnityEngine;


public class PlayerHitBullet : MonoBehaviourPunCallbacks
{
    private void OnTriggerEnter(Collider other)
    {
        if (photonView.IsMine)
        {
            if (other.TryGetComponent<Bullet>(out var bullet))
            {
                if (bullet.OwnerId != PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    photonView.RPC(nameof(HitBullet), RpcTarget.All, bullet.Id, bullet.OwnerId);
                }
            }
        }
    }

    [PunRPC]
    private void HitBullet(int id, int ownerId)
    {
        var bullets = FindObjectsOfType<Bullet>();
        foreach (var bullet in bullets)
        {
            if (bullet.Equals(id, ownerId))
            {
                Destroy(bullet.gameObject);
                break;
            }
        }
    }
}