using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class shot_Base : MonoBehaviour
{
    //カスタマイズ自由

    public enum elemental //属性
    {
        normal,
        stan,
    }
    public enum who //敵　主人公　無関係
    { 
        larua,
        enemy,
        boss,
        mukankei
    }
    [Header("属性")]
    public elemental e;
    [Header("誰?")]
    public who w;

    [SpaceAttribute(5)]
    [Header("コンポーネント")]
    public Rigidbody2D R2;
    public SpriteRenderer SR;
    public Animator A;

    [SpaceAttribute(5)]
    [Header("スタイル")]

    [TooltipAttribute("横に進む")]
    public bool straight_line;
    [TooltipAttribute("縦に進む")]
    public bool vertical;
    [TooltipAttribute("円移動")]
    public bool circle;
    [TooltipAttribute("ジグザグ")]
    public bool zigzag;
    [TooltipAttribute("画面外消滅")]
    public bool shometu;

    [SpaceAttribute(5)]
    [Header("性能")]
    public  int damage;//ダメージ
    public bool kantu;//貫通　キャラ
    public bool kantu2;//貫通　壁
    public bool kantu3;//貫通　ショット
    [Header("ふりむく")]
    public bool huri = false;

    [SpaceAttribute(5)]
    [Header("縦横の数値")]

    public float x; //横
    public float y; //縦

    public GameObject[] GG = new GameObject[2]; //エフェクト
    public Renderer R;

    [Header("孤立で向き取得")]
    public bool yes = false;

    [Header("ショックウェーブ")]
    public bool shock = false;
    public float rf;
    public float iti;

    BGM_Base b;

    private void Awake()
    {
        b = GameObject.Find("BGM").GetComponent<BGM_Base>();
        if(!R)
        R = GetComponentInChildren<SpriteRenderer>();
    if(yes)
        {
            SR.flipX = GetComponentInParent<SpriteRenderer>().flipX;
            transform.parent = null;
        }

    
    }
    void OnDrawGizmos()
    {
        // Draw a yellow cube at the transform position
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(new Vector2(transform.localPosition.x + iti, transform.localPosition.y), Vector3.down * rf);
    }
    public void Start()
    {
        if (shock)
        {
            var aa = 1 << 3;
            var bb = 1 << 13;
            var cc = aa + bb;
            RaycastHit2D r2 = Physics2D.Raycast(new Vector2(transform.localPosition.x + iti, transform.localPosition.y), Vector2.down * rf, rf, cc); ;
            if(r2.collider == null)
            {
                Destroy(gameObject);
            }
        }
    }

    public virtual void FixedUpdate()
    {

        if(straight_line | vertical)
        {
            if (SR.flipX)
            {
                xy(x, y);
            }
            else
            {
                xy(-x, y);
            }
        }
        if(!R.isVisible && shometu)
        {
            Destroy(gameObject);
        }

    }

    public void xy(float xx,float yy)
    {
        if(!straight_line)
        {
            xx = R2.velocity.x;
        }
        if (!vertical)
        {
            yy = R2.velocity.y;
        }
        R2.velocity = new Vector2(xx, yy);
    }
    public virtual void OnTriggerEnter2D(Collider2D C2)
    {
        if (w == who.larua)
        {
            if (C2.tag == "Enemy")
            {
                GameObject G = C2.gameObject;
                Action_Base E = G.GetComponent<Action_Base>();

                if (E && E.life > 0)
                {
                    if (!E.m)
                    {
                        efect();
                        damages(E);
                    }
                    else
                    {
                        efect(1);

                    }
                    if (!kantu)
                    {
                        Destroy(this.gameObject);
                    }
                }
            }
        }

    }
    public  virtual void OnTriggerStay2D(Collider2D C2)
    {
        if(!kantu2)
            if (C2.tag == "Block")
            {

                if (C2.gameObject.layer == 11 && R2.velocity.y >= 0 || C2.gameObject.layer == 13 && R2.velocity.y >= 0)
                {
                }
                else
                {
                    efect();
                    Destroy(this.gameObject);
                }
            }
    }

    public void damages(Action_Base E)
    {
        E.damageE(damage);
        if(huri)
        E.mukii(transform.position,E.transform.position);
        if (E.life <= damage)
            E.mukii(transform.position, E.transform.position);
        if (e == elemental.stan)
        {

            E.stan_start(3);
        }
    }

    //エフェクト
    public void efect(int a = 0)
    {
        if (a == 0)
        {
            b.se(24);
            Instantiate(GG[0], transform.position, transform.rotation);
        }
        else
        {
            b.se(25);
            if (GG[1])
            {
                Instantiate(GG[1], transform.position, transform.rotation);
            }
        }
    }
 }