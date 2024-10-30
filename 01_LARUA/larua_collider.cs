using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class larua_collider : MonoBehaviour
{
    //ray flip
    float rf = 0.458f;

    [HideInInspector]
    public int col = 0; //処理
    [Header("足はオンにしろ")]
    public bool leg = false;
    [Header("当たり判定")]
    public bool co = false;

    public LARUA larua;
    public GameObject G;

    public GameObject[] GG;//エフェクト 1アイコン

    [HideInInspector]
    public message m;

    public void FixedUpdate()
    {
        Debug.DrawRay(new Vector2(larua.transform.localPosition.x + rf,larua.transform.localPosition.y + -0.5f), Vector2.down * 1, Color.blue);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        //ブロック
            if (collision.tag == "Block" && !co)
            {

            if (leg && larua.ju == 0)
            {
                block_element b = collision.GetComponent<block_element>();
                if(b)
                {
                    switch(b.b)
                    {
                        case 0:
                            larua.BE = 0;
                            break;

                        case (block_element.a)1:
                            larua.BE = 1;
                            break;
                    }
                }

                gb0();
                    Transform T = collision.gameObject.GetComponent<Transform>();
                    if (T)
                        larua.attachT = T;

                       
            }
                if (collision.gameObject.layer == 7)
            {
                return;
            }

            larua.G_set = LayerMask.LayerToName(collision.gameObject.layer);
            if (collision.gameObject.layer != 11 && collision.gameObject.layer != 14)
            {

                if ( collision.gameObject.layer != 13)//ブロック
                {
                    
                    col = 1;
                }
                else
                {
                    if (leg && larua.R2.velocity.y <= 1 && larua.ju >= 2) //床
                    {
                        Transform T = collision.gameObject.GetComponent<Transform>();
                        if (T)
                            larua.attachT = T;
                        col = 1;
                        gb0();
                        larua.initialize_jump();
                    }
                }



            }
            else //床
            {
                if (leg && col == 0 )
                {
                    Transform T = collision.gameObject.GetComponent<Transform>();
                    Rigidbody2D R = collision.gameObject.GetComponent<Rigidbody2D>();
                    if (larua.ju >= 3)
                    {
                        if (collision.gameObject.layer == 11)//動く床専用」
                        {
                            col = 1;

                            if (T)
                                larua.attachT = T;
                            if (R)
                                larua.attachR2 = R;

                            //本体のほうで用意済み
                        }

                        if (collision.gameObject.layer == 14)//動くブロック専用」
                        {
                            col = 1;
                            gb0();
                            if (T)
                                larua.attachT = T;
                            if (R)
                                larua.attachR2 = R;
                            larua.initialize_jump();
                        }

                        if (collision.gameObject.layer == 13&& larua.transform.position.y - 1f >= T.transform.position.y) //床
                        {
                            col = 1;
                            if (R.velocity.y >= 0)
                            {
                                if (T)
                                    Debug.Log(T);
                                larua.attachT = T;
                                larua.initialize_jump();
                            }
                            else if (larua.R2.velocity.y <= 3)
                            {
                                if (T)
                                    Debug.Log(T);
                                larua.attachT = T;
                                larua.initialize_jump();
                            }
                        }
                    }
                    else if(larua.ju == 2)
                    {
                        if (collision.gameObject.layer == 11)//動く床専用」
                        {
                            //col = 1;
                            //Debug.Log("aa");
                            //if (T)
                            //    larua.attachT = T;
                            //if (R)
                            //    larua.attachR2 = R;
                            //if (R.velocity.y < 0)
                            {

                            }

                            //本体のほうで用意済み
                        }

                        if (collision.gameObject.layer == 14)//動くブロック専用」
                        {

                            if (R)
                                larua.attachR2 = R;
                            if (R.velocity.y >= 0)
                            {
                                col = 1;
                                gb0();
                                if (T)
                                    larua.attachT = T;
                                larua.initialize_jump();
                            }

                        }

                    }
                    else if(larua.ju == 1)
                    {
                        //　床
                        if (collision.gameObject.layer == 13 && larua.transform.position.y - 1f >= T.transform.position.y)
                        {
                            float f = larua.transform.position.y - 1f;
                            Debug.Log("zzz" + f);
                            Debug.Log("bbb" + T.transform.position.y);
                            Debug.Log(larua.transform.position.y - 1f >= T.transform.position.y);
                        }


                        if (collision.gameObject.layer == 13 && larua.transform.position.y - 1f >= T.transform.position.y) //床
                        {
                            Debug.Log("a");
                            col = 1;
                            if (R.velocity.y >= 0)
                            {
                                larua.initialize_jump();
                            }
                            else if (larua.R2.velocity.y <= 3)
                            {
                                larua.initialize_jump();
                            }
                        }
                    }

                }

                if(larua.ju == 0 )
                {
                    Transform T = collision.gameObject.GetComponent<Transform>();
                    Rigidbody2D R = collision.gameObject.GetComponent<Rigidbody2D>();
                    if (leg)
                    {
                        if (collision.gameObject.layer != 11 && collision.gameObject.layer != 14)//動くブロック専用」
                        {

                        }
                        else
                        {
                            if (R)
                            {
                                if (R)
                                    col = 1;
                                    gb0();
                                    if (T)
                                        larua.attachT = T;
                                larua.attachR2 = R;
                                if (R.velocity.y >= 0) //こうしないとジャンプするから
                                {
                                    if (collision.gameObject.layer != 14)
                                        larua.R2.velocity = new Vector2(larua.R2.velocity.x, 0);
                                }
                                if (R.velocity.x == 0 && larua.dash_mati == 0) //こうしないとジャンプするから
                                {
                                    larua.R2.velocity = new Vector2(0, larua.R2.velocity.y);
                                }
                                larua.y = 0;
                            }
                        }

                        //何もない
                        if(collision == null)
                        {
                            Debug.Log("ee");
                            larua.R2.gravityScale = larua.gg;
                            larua.attachT = null;
                            larua.attachR2 = null;
                            col = 0;

                        }
                    }
                }
            }
            }


        if (larua.koudou)
        {
            if (co)
            {
                //ダメージ
                if (collision.tag == "Enemy")
                {

                    GameObject G = collision.gameObject;
                    Enemy_attack E = G.GetComponent<Enemy_attack>();


                    if (!E || !G)
                    {

                        return;
                    }

                    if (E.attribute == Enemy_attack.a.death)
                    {
                        efect(0);
                        larua.bar = false;
                        larua.barrieroff();
                        larua.Larua_damage(E.attack);
                        return;
                    }

                    if (larua.m)
                    {

                        return;
                    }
                    else
                    {
                        efect(0);
                        larua.Larua_damage(E.attack);
                    }

                }
                if (collision.tag == "attackE" && !larua.m)
                {

                    GameObject G = collision.gameObject;
                    shot_Base E = G.GetComponent<shot_Base>();
                    if (!E || !G || E.w != shot_Base.who.enemy)
                    {
                        return;
                    }
                    else
                    {
                        efect(0);
                        larua.Larua_damage(E.damage);
                    }
                }

            }
            //メッセージ
            if (co && larua.koudou)
            {
                if (collision.gameObject.layer == 15)
                {
                    GG[1].SetActive(true);
                    m = collision.gameObject.GetComponent<message>();
                    m.swi = true;
                    larua.mm = true;
                }
            }

        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        //踏みつけ
        if (larua.koudou)
            if (leg && !co)
        {

            GameObject G = collision.gameObject;
            Action_Base E = G.GetComponent<Action_Base>();
            if (collision.tag == "Enemy" && E && larua.ju >= 2)
            {
                    
                    if (!E.m && E.h && !E.death)
                    {
                        efect(-1);

                        larua.Step();
                        E.damageE(3,true);
                    }
            }
            else if (collision.tag == "Enemy"  && larua.ju == 3)
                {
                    jumpdai j = G.GetComponent<jumpdai>();
                    if(j)
                    {
                        larua.Step2();
                        j.AA();
                    }
                }
        }
        if(!leg && !co)
        {
            col = 0;
        }





    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //ブロックから離れた
        if (larua.koudou && leg)
                if (collision.tag == "Block" && !co )
                {
                var a = 1 << 14;
                RaycastHit2D r2 = Physics2D.Raycast(new Vector2(larua.transform.localPosition.x + rf, larua.transform.localPosition.y + -0.5f), Vector2.down * 1, 5, a);
                if (r2.collider == null)
                {
                    r2 = Physics2D.Raycast(new Vector2(larua.transform.localPosition.x + -rf, larua.transform.localPosition.y + -0.5f), Vector2.down * 1, 5, a);
                    if (r2.collider == null)
                    {
                        r2 = Physics2D.Raycast(new Vector2(larua.transform.localPosition.x, larua.transform.localPosition.y + -0.5f), Vector2.down * 1, 5, a);
                    }
                }
                if (r2.collider == null)
                {
                    a = 1 << 11;
                    r2 = Physics2D.Raycast(new Vector2(larua.transform.localPosition.x + rf, larua.transform.localPosition.y + -0.5f), Vector2.down * 1, 5, a);
                    if (r2.collider == null)
                    {
                        r2 = Physics2D.Raycast(new Vector2(larua.transform.localPosition.x + -rf, larua.transform.localPosition.y + -0.5f), Vector2.down * 1, 5, a);
                        if (r2.collider == null)
                        {
                            r2 = Physics2D.Raycast(new Vector2(larua.transform.localPosition.x, larua.transform.localPosition.y + -0.5f), Vector2.down * 1, 5, a);
                        }
                    }
                }

                if (collision.gameObject.layer == 14 || collision.gameObject.layer == 11)
                {
                    if (larua.ju == 0)
                    {
                        if (r2.collider == null)
                        {
                            larua.BE = 0;
                            col = 0;
                            if (larua.attachR2)
                                if (larua.attachR2.gameObject.layer == 11 || larua.attachR2.gameObject.layer == 14)
                                {
                                    larua.ix = larua.attachR2.velocity.x * 0.14f;
                                }
                            larua.attachR2 = null;
                            larua.attachT = null;
                            StartCoroutine(s());
                            G.transform.parent = null;
                        }
                        else
                        if (r2.rigidbody.gameObject.layer != 14 && r2.rigidbody.gameObject.layer != 11)
                        {
                            larua.BE = 0;
                            col = 0;
                            larua.attachR2 = null;
                            larua.attachT = null;
                            StartCoroutine(s());
                            G.transform.parent = null;
                        }
                        else
                        {
                            Debug.Log(r2.collider);
                        }
                    }
                    if(larua.ju == 2 && larua.R2.gravityScale == 1)
                    {
                        col = 0;
                    }

                  }
                else
                {
                    larua.BE = 0;
                    col = 0;
                    larua.attachR2 = null;
                    larua.attachT = null;
                    StartCoroutine(s());
                    G.transform.parent = null;
                }
                
            }
        if (co && larua.koudou)
        {
            {
                GG[1].SetActive(false);
                if (m)
                {
                    m.swi = false;
                    m = null;
                    larua.mm = false;
                }
            }
        }
    }
    IEnumerator s()
    {
        yield return new WaitForSeconds(0.02f);
        if (!larua.attachT && !larua.attachR2)
        {
            larua.R2.gravityScale = larua.gg;
            if (larua.R2.velocity.y >= 0)
            {
                larua.animator.SetBool("jump", true);
            }
        }
        yield return null;
    }

    void efect(float a)//ダメージ
    {
        Instantiate(GG[0],new Vector3( transform.position.x,transform.position.y + a,transform.position.z), transform.rotation);
    }

    void gb0()
    {
        if(larua.koudou)
        {
            larua.R2.gravityScale = 0;
        }
    }
}
