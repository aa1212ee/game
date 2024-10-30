using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Action_Base : MonoBehaviour
{
    //HP
    public int max_life = 3;
    public int life = 3;
    //アクション
    [HideInInspector] public float x;
    [HideInInspector] public float y;
    [HideInInspector] public bool death = false;
    protected bool jumping = false;
    //[HideInInspector]
    public  int ju; //0ジャンプ可能 1ジャンプ中 2落下中]
    [HideInInspector] public int at; //攻撃
    public bool koudou = true;//行動可能か
    [HideInInspector]
    public bool Dk = false;//ダメージ中

    //エフェクト・無敵処理
    //点滅中
    protected bool f = false;

    //無敵
    [Header("無敵")]
    public bool m = false;

    //踏みつけ有効
    public bool h = true;

    //コンポーネント

    public Rigidbody2D R2;
    public BoxCollider2D B2;
    public BoxCollider2D T2;
    public  Animator animator;
    public SpriteRenderer sprite;
    public Renderer R;
    [HideInInspector]
    public bool r = true;


    //状態異常　耐性
    [Header("気絶無効")]
    public bool stan;
    [Header("振り向き無効")]
    public bool muki = false;
    [HideInInspector]
    public bool sstan;
    [Header("スタンオブジェクト")]
    public GameObject sss;
    GameObject s;
    [Header("敵専用　スコア")]
    public int score = 100;

    //ベルトコンベア
    [HideInInspector]
    public float bel = 0;
    //ラルアゲット W5-1～
    public GameObject larua;
    public SpriteRenderer lsr;

    //敵消滅
    [Header("敵が下まで行くと消滅")]
    public bool enemy_down_shometu = false;

    [HideInInspector]
    public BGM_Base BB;

    public virtual void Awake()
    {
        BB = GameObject.Find("BGM").GetComponent<BGM_Base>();
    }

    protected virtual void Update()
    {
        if(!R.isVisible)
        {
            r = false;
        }
        else
        {
            r = true;
        }
        if(enemy_down_shometu && transform.position.y <= -10)
        {
            Destroy(this.gameObject);
        }
    }

    #region 便利メソッド


    //敵ダメージ
    public virtual void damageE (int d,bool a = false,float m =0.02f, float f = 0.05f,int t = 1) //敵ダメージ
    {
        // a 踏みつけられた
        if (life > 0)
        {
            if(a)
            {
                m = 0.15f;
                t = 3;
                f = 0.05f;
            }
            StartCoroutine(invincible(m));
            StartCoroutine(Flash(t, f));
            damage(d);
            return;
        }
  //      else
   //     {
     //       t = 99999;
    //        E_death();
   //         StartCoroutine(Flash(t, f));
    //        if (!sprite.flipX)
   //         {
   //             move(17, R2.velocity.y);
   //         }
   //         else if (sprite.flipX)
   //         {
    //            move(-17, R2.velocity.y);
    //        }
    //        jump(R2.velocity.x, 14);
   //         death = true;
   //         return;
   //     }
    }
    //振り向かされる
    public virtual void mukii(Vector2 s,Vector2 e)
    {

        if(!muki)
        if(s.x < e.x)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
    }

    //ラルア取得
    public virtual void larua_get()
    {
        larua = GameObject.Find("LARUA");
        lsr = larua.GetComponent<SpriteRenderer>();
        if(!lsr)
        {
            lsr = larua.GetComponentInChildren<SpriteRenderer>();
        }
    }

    //ラルアのほうへ振り向く
    public virtual void larua_muki(bool reverse = false)
    {
        if(!larua)
        {
            Debug.LogWarning("ラルアなし");
        }
        else
        {
            if (!reverse)
            {
                if (larua.transform.position.x > this.transform.position.x)
                {
                    sprite.flipX = true;
                }
                else
                {
                    sprite.flipX = false;
                }
            }
            else
            {
                if (larua.transform.position.x > this.transform.position.x)
                {
                    sprite.flipX = false;
                }
                else
                {
                    sprite.flipX = true;
                }
            }
        }
    }
    //雑魚敵
    public void walkenemys(bool a = false)
    {
        if (!r)
        {
            animator.enabled = false;
            T2.enabled = false;
            R2.gravityScale = 0;
            if (R2.velocity.y >= 0)
            {
                R2.velocity = new Vector2(R2.velocity.x, 0);
            }
        }
        else
        {
            animator.enabled = true;
            T2.enabled = true;
            if (!a)
                R2.gravityScale = 1;
        }
    }

    //効果音
    public void se(int a = 0)
    {
        BB.se(a, sprite);
    }

    #endregion
    #region //移動・ジャンプ
    protected virtual void move(float x,float y)
    {
                R2.velocity = new Vector2(x + bel ,R2.velocity.y);
        bel = 0;
    }
    protected virtual void jump(float x,float y)
    {
        R2.velocity = new Vector2(R2.velocity.x, y);
        bel = 0;
    }

    #endregion

    #region //エフェクト

    //点滅
    public virtual IEnumerator Flash(int a,float b,int aki = 0) //繰り返し、点滅速度
    {
        f = true;
  
        int c = 0;
        while (c  < a )
        {
            sprite.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(b);
            sprite.color = new Color(1, 1, 1, 0f);
            yield return new WaitForSeconds(b);
            c++;
        }
        f = false;

        sprite.color = new Color(1, 1, 1, 1);
        yield return null;
    }
    #endregion

    #region//効果

    //ダメージ
    virtual public void damage(int a)
    {
        life -= a;
    }
    //ヒール
    public void heel(int a)
    {
        life += a;
    }
    //無敵
    public IEnumerator invincible(float a)
    {
        m = true;
        yield return new WaitForSeconds(a);
        m = false;
        yield return null;
    }

    //消滅
    protected void delet()
    {
        Destroy(this.gameObject);

    }
    //雑魚専用 死亡
    protected virtual void E_death(bool girl = false)　
    {
        if (!girl)
        {
            BB.se(13, sprite);
        }
        else
        {
            BB.se(14, sprite);
        }
        if (s != null)
        {
            Destroy(s);
        }
        stage_manager.Hitstop(0.3f, 0.1f);
        stage_manager.SCORE = score;
        if (animator)
        {
            animator.SetBool("death", true);
        }
        R2.bodyType = RigidbodyType2D.Dynamic;
        B2.enabled = false;
        T2.enabled = false;
        StartCoroutine(Flash(9999, 0.05f));
        if (!sprite.flipX)
        {
            move(17, R2.velocity.y);
        }
        else if (sprite.flipX)
        {
            move(-17, R2.velocity.y);
        }
        jump(R2.velocity.x, 14);
        death = true;
        return;
    }
    #endregion

    #region//共通アニメ //ラルア　ボス専用

    protected virtual void damage_all1(int jo = 0) //ダメージモーション　//状態異常 int
    {
        animator.SetBool("damage", true);
        if (jo == 0)
        {
            BB.se(6,sprite);
            animator.Play("damage");
            if (ju == 1)
            {
                ju = 2;
            }
        }
        if(jo == 1)
        {
            animator.Play("fire_damage");

                ju = 3;
        }
        R2.gravityScale = 0;
        R2.velocity = new Vector2(0, 0);
        Dk = true;

    }
    public virtual void damage_all2(bool sky) //ダメージ動ける
    {
        animator.SetBool("damage", false);
        if(!sky)
        {
            R2.bodyType = RigidbodyType2D.Dynamic;
        }
        Dk = false;
    }

    #endregion

    #region 状態異常

    public  void stan_start(int a)
    {
        StartCoroutine(Stan(a));
    }
    public IEnumerator Stan (int time) //気絶
    {
        if (!stan && !sstan && life > 0)
        {
            s = Instantiate(sss, new Vector3(transform.position.x + 0, transform.position.y + 1, transform.position.z), Quaternion.identity);
            s.transform.parent = this.transform;
            animator.Play("stan");
            sstan = true;
            yield return new WaitForSeconds(time);
            if (life > 0)
            {
                animator.Play("move");
                Destroy(s);
                sstan = false;
                yield return null;
            }
        }

    }
    //ベルトコンベア
    

    #endregion

}


