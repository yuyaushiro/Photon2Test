using UnityEngine;
using Photon.Pun;

public class GamePlayer : MonoBehaviourPunCallbacks
{
    [SerializeField]
    float velocity = 5f;
    // Start is called before the first frame update
    void Update()
    {
        // 自身が生成したオブジェクトのみに移動処理
        if (photonView.IsMine)
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");
            Vector3 dir = Vector3.Scale(new Vector3(inputX, inputY, 0).normalized, new Vector3(Mathf.Abs(inputX), Mathf.Abs(inputY), 1));
            Vector3 dv = velocity * dir * Time.deltaTime;

            transform.Translate(dv.x, dv.y, 0f);
        }
    }
}
