using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinner : shot_Base
{
    public BoxCollider2D T2;
    public collider_hit ch;
    bool endd = false;
    float time = 0;

    // Start is called before the first frame update
    new void Start()
    {

        R2.velocity = new Vector2(x, y);
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        time += Time.deltaTime;
        if (!endd)
        {
            if (time >= 0.21f)
            {
                if (SR.flipX)
                {
                    R2.velocity = new Vector2(2.9f, y);
                }
                else
                {
                    R2.velocity = new Vector2(-2.9f, y);
                }
            }
            else
            {
                if (SR.flipX)
                {
                    R2.velocity = new Vector2(x, y);
                }
                else
                {
                    R2.velocity = new Vector2(-x, y);
                }
            }

            if(ch.i == 1)
            {

                GameObject g = ch.B;
                if(g.layer == 13 || g.layer == 11)
                {
                    return;
                }

                    en();
            }
        }

        if(time >= 3f && !endd)
        {
            en();
        }
    }

    public override void OnTriggerEnter2D(Collider2D C2)
    {
        if (w == who.larua && !endd)
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
                        en();
                    }
                }
            }
        }

    }
    public override void OnTriggerStay2D(Collider2D C2)
    {
        if (!kantu2 && !endd)
            if (C2.tag == "Block")
            {

                if (C2.gameObject.layer == 11 || C2.gameObject.layer == 13)
                {
                    Debug.Log("aa");
                }
                else
                {
                    Debug.Log(C2.gameObject.layer);
                    efect();
                    en();
                }
            }
    }

    //ÉAÉjÉÅÅ´
    public void en()
    {
        R2.velocity = new Vector2(0, 0);
        T2.enabled = false;
        A.Play("spinner_end");
        endd = true;
    }
    public void end()
    {
        R2.velocity = new Vector2(0, 0);
        Destroy(gameObject);  
    }
}
