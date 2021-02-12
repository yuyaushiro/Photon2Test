using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable
{
    /// <summary>
    /// 移動速度
    /// </summary>
    [SerializeField]
    private float _velocity = 5f;

    /// <summary>
    /// 最大スタミナ
    /// </summary>
    [SerializeField]
    private float _maxStamina = 6f;

    /// <summary>
    /// スタミナ減少率
    /// </summary>
    [SerializeField]
    private float _decreaseRate = 1f;

    /// <summary>
    /// スタミナ回復率
    /// </summary>
    [SerializeField]
    private float _recoveryRate = 2f;

    /// <summary>
    /// スタミナ表示バーオブジェクト
    /// </summary>
    [SerializeField]
    private Slider _staminaBar = default;

    /// <summary>
    /// 現在のスタミナ
    /// </summary>
    private float _currentStamina = 0f;

    void Start()
    {
        // スタミナの初期化
        _currentStamina = _maxStamina;
        // スタミナ表示オブジェクトの取得
        if (_staminaBar == null)
        {
            GameObject statusUi = transform.Find("StatusUi").gameObject;
            _staminaBar = statusUi.transform.Find("StaminaBar").GetComponent<Slider>();
        }
    }

    void Update()
    {
        // 自身が生成したオブジェクト
        if (photonView.IsMine)
        {
            // 移動方向
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");
            Vector3 dir = Vector3.Scale(new Vector3(inputX, inputY, 0).normalized,
                                        new Vector3(Mathf.Abs(inputX), Mathf.Abs(inputY), 1));
            Vector3 dv = dir * _velocity * Time.deltaTime;

            // 移動あり
            if (dv.sqrMagnitude > 0f)
            {
                // スタミナを減少
                _currentStamina = Mathf.Max(0f, _currentStamina - Time.deltaTime*_decreaseRate);
                // 座標の移動
                transform.Translate(dv.x, dv.y, 0f);
            }
            // 移動なし
            else
            {
                // スタミナを回復
                _currentStamina = Mathf.Min(_currentStamina + Time.deltaTime*_recoveryRate, _maxStamina);
            }
        }

        // スタミナをゲージに表示
        _staminaBar.value = _currentStamina / _maxStamina;
    }

    /// <summary>
    /// パラメータのリアルタイム同期
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="info"></param>
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // 書き込み
        if (stream.IsWriting)
        {
            // 自身のアバターのスタミナを送信
            stream.SendNext(_currentStamina);
        }
        // 読み込み
        else
        {
            // 他プレイヤーのアバターのスタミナを受信
            _currentStamina = (float)stream.ReceiveNext();
        }
    }
}
