using UnityEngine;
using System.Collections;
using UniRx;

namespace OldPackage
{
    [RequireComponent(typeof(GroundChecker2D))]
    public class Player2D : Character2D
    {
        [Header("Player2D's Field")]
        [Range(1, 4)]
        [SerializeField]
        int playerID = 1;
        [SerializeField] int maxHp = 6;                                             // 体力の最大値
        [SerializeField] int hp = 6;                                                // 現在の体力
        [SerializeField] float moveSpeed = 5.0f;                                    // 移動速度
        [SerializeField] float jumpPower = 10.0f;                                   // ジャンプ力
        [SerializeField] int money = 100;                                           // お金（飛び道具を使うときに消費）
        [SerializeField] int rest = 0;                                              // 残機
        [SerializeField] PlayerAttackType attackType = PlayerAttackType.Normal;     // 現在の武器タイプ

        [SerializeField] float mutekiTime = 1f;

        [SerializeField] bool canMove;
        [SerializeField] bool damageFlg = true;

        [SerializeField] Transform offset;                                          // 向き変更用

        [Header("Normal Attack Settings")]
        [SerializeField]
        Vector2 attackOffset;
        [SerializeField] float attackDuration;
        [SerializeField] Collider2D attackTrigger;
        [SerializeField] TrailRenderer attackTrail;

        [Header("Missile Attack Settings")]
        [SerializeField]
        int missileCost = 10;                                      // 飛び道具のコスト
        [SerializeField] PlayerMissile TemariMissile;

        [Space]
        [SerializeField]
        LayerMask enemyLayer;

        [Header("Components")]
        [SerializeField]
        Animator animator;
        [SerializeField] Rigidbody2D myRigidbody2D;
        [SerializeField] GroundChecker2D groundChecker;
        [SerializeField] Renderer mainRenderer;

        [Header("SoundEffect")]
        [SerializeField]
        AudioClip jumpSE;
        [SerializeField] AudioClip getCoinSE;
        [SerializeField] AudioClip swingSE;
        [SerializeField] AudioClip weaponChangeSE;
        [SerializeField] AudioClip damageSE;
        [SerializeField] AudioClip deadSE;

        float moveDirection;
        Rigidbody[] rigidbodies;
        Renderer[] renderers;

        // イベント発行側
        Subject<int> hpSubject = new Subject<int>();
        Subject<int> moneySubject = new Subject<int>();
        Subject<int> restSubject = new Subject<int>();
        Subject<PlayerAttackType> attackTypeSubject = new Subject<PlayerAttackType>();

        // イベント購読側
        public IObservable<int> OnHpChanged { get { return hpSubject; } }
        public IObservable<int> OnMoneyChanged { get { return moneySubject; } }
        public IObservable<int> OnRestChanged { get { return restSubject; } }
        public IObservable<PlayerAttackType> OnAttackTypeChanged { get { return attackTypeSubject; } }

        public int MaxHp { get { return maxHp; } }
        public int Hp { get { return hp; } }
        public int Money { get { return money; } }
        public int Rest { get { return rest; } }
        public PlayerAttackType AttackType { get { return attackType; } }
        public GroundChecker2D GroundChecker { get { return groundChecker; } }

        public Renderer MainRenderer
        {
            get
            {
                return mainRenderer;
            }

            set
            {
                mainRenderer = value;
            }
        }

        public int PlayerID
        {
            get
            {
                return playerID;
            }

            set
            {
                playerID = value;
            }
        }

        // ダメージを受けたときのリアクション
        IEnumerator DamageReaction(float x)
        {

            if (damageFlg == false) yield break;

            // HP減少
            hpSubject.OnNext(hp - 1);

            // 0以下なら死ぬ
            if (hp <= 0)
            {

                Dead();

                yield break;

            }

            // 吹っ飛ばす
            moveDirection = x;
            myRigidbody2D.velocity = Vector2.up * jumpPower * 2 / 3;

            // 無敵処理
            StartCoroutine(Muteki(mutekiTime));

            // 効果音を鳴らす
            SoundManager.Instance.PlaySe(damageSE);

            // ダメージを受けたらしばらく動けない
            canMove = false;

            yield return new WaitForSeconds(0.25f);

            // 着地するまで動けない
            while (!groundChecker.IsGrounded)
            {
                yield return null;
            }

            attackTrail.enabled = false;

            // 動くフラグを元に戻す
            canMove = true;

            yield break;

        }

        // 無敵状態
        IEnumerator Muteki(float duration)
        {

            damageFlg = false;

            float targetTime = Time.time + mutekiTime;

            while (Time.time < targetTime)
            {

                SetRendererEnableAll(!renderers[0].enabled, renderers);

                yield return new WaitForSeconds(0.1f);

            }

            SetRendererEnableAll(true, renderers);

            // ゲーム進行中ならダメージフラグを元に戻す
            if (GameController.Instance.countFlg == true) damageFlg = true;

            yield break;

        }

        void Reset()
        {
            groundChecker = GetComponent<GroundChecker2D>();

            animator = GetComponent<Animator>();
            animator.applyRootMotion = false;

            myRigidbody2D = GetComponent<Rigidbody2D>();
        }

        void Awake()
        {

            rigidbodies = GetComponentsInChildren<Rigidbody>();
            renderers = GetComponentsInChildren<Renderer>();

            // イベント登録
            OnHpChanged.Subscribe(SetHp);
            OnMoneyChanged.Subscribe(SetMoney);
            OnRestChanged.Subscribe(SetRest);
            OnAttackTypeChanged.Subscribe(SetAttackType);
        }

        void Start()
        {

            canMove = true;

            SetIsKinematicAll(true, rigidbodies);

        }

        void FixedUpdate()
        {

            // 移動先計算
            Vector3 afterPos = transform.position + moveDirection * Vector3.right * moveSpeed * Time.fixedDeltaTime;

            // Clamp
            afterPos.Set(Mathf.Clamp(afterPos.x, GameController.Instance.ScreenLeft, GameController.Instance.ScreenRight), afterPos.y, afterPos.z);

            // 実際の移動
            transform.position = afterPos;

        }

        void Update()
        {

            // 移動
            Move();

            // 攻撃方法の切り替え
            if (canMove && Input.GetButtonDown("WeaponChange " + playerID) == true)
            {
                attackTypeSubject.OnNext(attackType == PlayerAttackType.Normal ? PlayerAttackType.Missile : PlayerAttackType.Normal);
                SoundManager.Instance.PlaySe(weaponChangeSE);
            }

            // 攻撃
            if (canMove && Input.GetButtonDown("Fire " + playerID) == true) Attack();


        }

        void OnTriggerEnter2D(Collider2D other)
        {

            if (other.GetComponent<CoinTrigger>())
            {

                moneySubject.OnNext(money + 5);

                SoundManager.Instance.PlaySe(getCoinSE);

            }

            if (other.GetComponent<GoalItem>())
            {
                // SE
                SoundManager.Instance.PlaySe(getCoinSE);

                GameController.Instance.Goal(this);
            }

            if (other.GetComponent<DeadArea>())
            {

                hpSubject.OnNext(0);

                Dead();

            }

            if (other.GetComponent<DamageArea>())
            {

                float dir = Mathf.Sign(transform.position.x - other.transform.position.x);
                Damage(dir);

            }

        }

        void OnTriggerStay2D(Collider2D other)
        {

            if (other.GetComponent<DamageArea>())
            {

                float dir = Mathf.Sign(transform.position.x - other.transform.position.x);
                Damage(dir);

            }

        }

        private void Move()
        {

            // 地上での処理
            if (groundChecker.IsGrounded)
            {

                if (canMove == true)
                {
                    // 入力情報を保存
                    moveDirection = Input.GetAxis("Horizontal " + playerID);

                    // 方向転換
                    if (moveDirection != 0) offset.rotation = Quaternion.LookRotation(Vector3.right * moveDirection);

                    // ジャンプ
                    if (Input.GetButtonDown("Jump " + playerID) == true)
                    {

                        myRigidbody2D.velocity = Vector3.up * jumpPower;

                        SoundManager.Instance.PlaySe(jumpSE);

                    }

                }

            }
            // 空中での処理
            else
            {

                if (canMove)
                {

                    if (-1 <= moveDirection && moveDirection <= 1) moveDirection += Input.GetAxis("Horizontal " + playerID) * Time.deltaTime * 10;

                    moveDirection = Mathf.Clamp(moveDirection, -1, 1);

                }

            }

            // スティックの加減をanimetorに渡す
            animator.SetFloat("Speed", Mathf.Abs(moveDirection));

            // タイマーが 0 なら死亡
            if (TimeManager.Instance.CurrentTime <= 0) Dead();

        }

        void Attack()
        {

            // 近距離攻撃
            if (attackType == PlayerAttackType.Normal)
            {

                // アニメーションスタート
                animator.SetTrigger("Attack");

            }
            // 遠距離攻撃
            else if (attackType == PlayerAttackType.Missile && missileCost <= money)
            {

                // アニメーションスタート
                animator.SetTrigger("Attack2");

            }

        }

        public void StartAttack()
        {

            attackTrigger.transform.localPosition = new Vector3(attackOffset.x * offset.forward.x, attackOffset.y, 0);
            attackTrigger.enabled = true;
            attackTrail.enabled = true;

            Invoke("AttackTriggerEnableFalse", attackDuration);

            // 効果音再生
            SoundManager.Instance.PlaySe(swingSE);

        }

        public void AttackTriggerEnableFalse()
        {
            attackTrigger.enabled = false;
            attackTrail.enabled = false;
        }

        public void StartAttack2()
        {

            if (missileCost > money) return;

            // お金の減算
            moneySubject.OnNext(money - missileCost);

            // てまりをインスタント
            Instantiate(TemariMissile, transform.position + Vector3.up + transform.forward * 0.5f, offset.rotation);

        }

        /// <summary>
        /// 直接使用せず、Subscribeに登録して使用する。
        /// </summary>
        /// <param name="value">hpの値</param>
        void SetHp(int value)
        {
            hp = value;
        }

        /// <summary>
        /// 直接使用せず、Subscribeに登録して使用する。
        /// </summary>
        /// <param name="value">moneyの値</param>
        void SetMoney(int value)
        {
            money = value;
        }

        /// <summary>
        /// 直接使用せず、Subscribeに登録して使用する。
        /// </summary>
        /// <param name="value">restの値</param>
        void SetRest(int value)
        {
            rest = value;
        }

        /// <summary>
        /// 直接使用せず、Subscribeに登録して使用する。
        /// </summary>
        /// <param name="value">attackTypeの値</param>
        void SetAttackType(PlayerAttackType value)
        {
            attackType = value;
        }

        private void Dead()
        {

            // SetVelocityZeroAll(rigidbodies);

            offset.parent = null;
            Destroy(gameObject);

            SetIsKinematicAll(false, rigidbodies);

            SoundManager.Instance.PlaySe(deadSE);

        }

        public void Damage(float x)
        {

            attackTrail.enabled = false;
            StartCoroutine(DamageReaction(x));

        }

        public void Goal()
        {

            moveDirection = 0;

            canMove = false;
            damageFlg = false;

            offset.rotation = Quaternion.Euler(0, 180, 0);

            animator.SetTrigger("Goal");

        }

    }

    public enum PlayerAttackType
    {
        Normal,
        Missile
    }
}