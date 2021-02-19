using Photon.Pun;
using TMPro;

public class PlayerNameDisplay : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        var nameLabel = GetComponent<TextMeshPro>();
        // プレイヤー名とプレイヤーIDの取得
        var playerName = photonView.Owner.NickName;
        var playerId = photonView.OwnerActorNr;
        // 表示する
        nameLabel.text = $"{playerName}({playerId})";
    }
}
