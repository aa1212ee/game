using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XInput;
using UnityEngine.UIElements;

public class LARUA : Action_Base
{

    //�����u���b�N����o�����̈ړ����x
    [HideInInspector]
    public float ix = 1;

    //�C���X�y�N�^�[�ύX��
    public float speed;
    public new float[] jump = {20.5f,25.5f}; 

    public Vector2 V2;
    public int l;


    public Status s;

    [HideInInspector] public float hor;
    [HideInInspector] public float ver;
    [HideInInspector] public float sp;
    [HideInInspector] public float me;

    public larua_collider head;
    public larua_collider leg;

    GameObject G;

    string attach;
    [HideInInspector]  public Rigidbody2D attachR2;
    [HideInInspector] public Transform attachT;
    public string G_set
    {
        get{ return G_set; }
        set{ attach = value; }
    }
    //����Y��
   float  y_death = -9999;

    //�U���G�t�F�N�g 0�`1�@������� 2�`3�@�\�[�h 4�_�b�V��
    public List<GameObject> S;
    //���̑��G�t�F�N�g 0��
    public List<GameObject> E;

    //�q�I�u�W�F�N�g
    protected List<GameObject> kodomo;

    //�U�������V���b�g
    public int ss;

    //�_�b�V��
    public float dash1;
    public float dash2; //���i�K
    bool dashing;
    public int dash_mati = 0;
    public float[] slide = { 1f};


    //���̏ꂵ�̂��̃W�����v�A�ő΍�
    int  jju = 0;
    int ssp = 0;

    //�W�����v��
    int jd;

    [HideInInspector]
    public int BE; //�u���b�N�G�������g

    //��Ԉُ�
    [HideInInspector]
    public int jo = 0; //1�₯��

    //�R���[�`��
    Coroutine C_t;

    [Header("��b")]
    public GameObject kaiw;
    public bool mm = false;

    //��ر
    [Header("�o���A")]
    public GameObject bareea;
    GameObject bareeakanri;
    Animator bbba;
    public bool bar;
    float bs, bj;

    //�A�C�e��
    [HideInInspector]
    public int ite;
    public GameObject[] item;

    //���G
    [Header("���G")]
    public  GameObject mutekii;
    public GameObject mutekii_effect;
    bool mf = false;

    [Header("�O���r�e�B")]
    public float gg = 1;

    // Update is called once per frame

    public override void Awake()
    {
        //manager.larua_attack = 2;
        G = gameObject.gameObject.GetComponent<GameObject>();
        GameObject g = GameObject.Find("Main Camera");
        camerajisaku c = g.GetComponent<camerajisaku>();
        y_death = c.fminy - 18;
        BB = GameObject.Find("BGM").GetComponent<BGM_Base>();

    }


    void FixedUpdate()
    {
        if(!bar && !mf)
        {
            bs = 0;
            bj = 0;
        }
        else
        {
            bs = 4;
            bj = 4;
        }

        if(jo == 1) //�Ώ�
        {
            if(R2.velocity.y <= 0)
            {
                attachT = null; //���ɔ�������̂Łc
                attachR2 = null;
                if (!mf)
                {
                    C_t = StartCoroutine(Flash(15, 0.05f));
                }
                    damage_all2(true);
            }
        }

        animator.SetFloat("fall2", R2.gravityScale);
        if(ju== 0 && !attachT)
        {
            R2.gravityScale = gg;
            attachR2 = null;
            BE = 0;
            leg.col = 0;
            animator.SetBool("fall", true);
            ju = 3;
        }

        #region �A�C�e��
        if(ite == 1)
        {
            if(item_icon.item == 1)
            {
                soccer();
            }
            if (item_icon.item == 2)
            {
                muteki();
            }
        }
        #endregion

        if (!Dk && koudou)
        {
            if (manager.larua_attack == 4 && ss == 1)
            {

            }
            else
            {
                move_dash();
            }

            if(ju <= 1 && ver == 0)
            {
                jju = 0;
            }

            //�W�����v
            if (ver == 1 && ju == 0 || ver == 2 && ju == 0 && jju ==0)
            {
                ssee(0);
                R2.gravityScale = gg;
                ju = 1;
                jju = 1;

            }
            else if (ver == 0 && ju == 2 && R2.velocity.y >= 10)
            {
                animator.SetBool("fall", true);
                ju = 3;
                R2.velocity = new Vector2(R2.velocity.x,10) ;

            }

            switch (ju)
            {
                case 0:
                    y = 0;
                    break;
                case 1:
                    animator.SetBool("jump", true);
                    //�e�폜
                    {
                        BE = 0;
                        leg.col = 0;
                        if (attachR2)
                            if (attachR2.gameObject.layer == 13 || attachR2.gameObject.layer == 14)
                            {
                                ix = attachR2.velocity.x * 0.14f;
                            }
                        attachR2 = null;
                        attachT = null;
                    }

                    if (jd == 0)
                    {
                        if (!dashing)//��������
                        {
                            y = jump[0] + bj;
                        }
                        else
                        {
                            y = jump[1] + bj;
                        }
                    }
                    else
                    {
                        y = jump[2] + bj;
                    }

                    ju = 2;
                    base.jump(R2.velocity.x, y);

                    break;
                case 2:
                    //�e�폜
                    {
                        BE = 0;

                        attachR2 = null;
                        attachT = null;
                    }
                    break;
                default:

                    if (dashing)//�_�b�V������
                    {
                        if (R2.velocity.y <= -23)
                        {
                            R2.velocity = new Vector2(R2.velocity.x, -23f);
                        }
                    }
                    else
                    {
                        if (R2.velocity.y <= -23)
                        {
                            R2.velocity = new Vector2(R2.velocity.x, -23);
                        }


                    }
                   
                    break;
            }
            //�����A�j���[�V����
            if (ju == 2 && -1f >= R2.velocity.y || hor >= 1 && ju == 0 && -10 >= R2.velocity.y && R2.gravityScale != 0 || hor == 0 && ju == 0 && R2.gravityScale != 0 && -10 >= R2.velocity.y)
            {
                animator.SetBool("jump", true);
                animator.SetBool("fall", true);
                ju = 3;

            }



            //���@��
            if (head.col == 1 && -1 < R2.velocity.y)
            {
                animator.SetBool("fall", true);
                ju = 2;

                base.jump(x, -1);
            }
            //��
            if(attach == "floor" && leg.col == 1 && ju >= 2 && R2.velocity.y <= 0) //��
            {
                if (attachT)
                {
                    if(transform.position.y >= attachT.transform.position.y)
                    {
                        if (attachR2)
                        {
                            if (attachR2.velocity.y > 0)
                            {
                                R2.gravityScale = 0;
                            }
                        }
                        else
                        {
                            R2.gravityScale = 0;
                        }
                        initialize_jump();

                    }
                }
                else
                {
                    R2.gravityScale = 0;
                    initialize_jump();
                }
            }
            else
            if (leg.col == 1 && ju >= 2 && R2.velocity.y <= 2)
            {
                initialize_jump();
            }


            //�U��
            switch (at)
            {
                case 0:
                    if(sp == 1 || sp == 2 && ssp == 0)
                    {
                        at = 1;
                        
                    }
                    break;
                case 1:
                    if (ss != 0 && ju == 0 && R2.gravityScale == 0) //�V���b�g
                    {
                        if(manager.larua_attack ==1 || manager.larua_attack == 3 || manager.larua_attack == 4)
                        x = 0;
                    }
                    break;
                default: 
                    break;
                
            }

            if(sp == 1 || sp == 2 && ssp == 0)
            {
                ssp = 1;
                attack_start();
            }
            if(sp <= 1 && ssp == 1)
            {

                ssp = 0;
            }




            //�I���ړ�����
            if(ix < 1)
            {
                ix = 1;
            }

            base.move(x * ix , y);

        }
        else if(Dk == true) //��Ԉُ�
        {
            if (jo == 1)�@//�Ώ�
            {
                move_dash();
                base.move(x, y);
            }
        }

        //

        if(transform.position.y <= y_death)//������
        {
            if (manager.game == 0)
            {
                ssee(7);
            }
            stage_manager.HP = 0;
            Death2();
        }
        if (leg.col == 1 && stage_manager.HP <= 0 && R2.velocity.y <= 2)//���n��
        {
            animator.Play("death");
            if (manager.game == 0)
            {
                ssee(24);
            }
            Death2();
        }

        if (attachR2 && ju == 0 && leg.col == 1)
        {

            if (attachR2.velocity.y >= 0) //�u���b�N�֌W�ɂ��Ă���
            {
                if (attach == "floor")
                {
                    R2.velocity = new Vector2(R2.velocity.x + attachR2.velocity.x, attachR2.velocity.y);
                }
                else
                {
                    R2.velocity = new Vector2(R2.velocity.x + attachR2.velocity.x, 0);
                }

                if (attachT && attach == "floor")
                    transform.position = new Vector2(transform.position.x,attachT.localPosition.y + 1.24f);
            }
            else
            {
                R2.velocity = new Vector2(R2.velocity.x + attachR2.velocity.x, attachR2.velocity.y + -1);
                if(attachT && attach == "floor")
                transform.position = new Vector2(transform.position.x, attachT.localPosition.y + 1.24f);
            }
        }
        if(me == 1 && !kaiwa.b && mm && Time.timeScale == 1f)
        {
            Instantiate(kaiw);
            Time.timeScale = 0;
            kaiwa.b = true;
        }

    }

    void move_dash()
    {
        //�ړ�
        if (hor == 0)
        {
            animator.SetFloat("Blend", 1.2f);
            if (dash_mati == 1 && ju == 0)
            {
                dash_mati = 2;
            }
            if (dash_mati == 3)
            {
                dash_mati = 0;
            }

            animator.SetBool("dash", false);
            animator.SetBool("run", false);

            if (x > 0 && sprite.flipX) //�u���[�L�␳
            {
                if (BE == 1) //�X
                {
                    if (dash_mati == 4)
                    {
                        CancelInvoke("dashwaitend");
                        dash_mati = 0;
                    }
                        x = x - slide[1];
                }
                else
                {
                    x = x - slide[0];
                }
            }
            else if (x < 0 && !sprite.flipX)
            {
                if (BE == 1) //�X
                {
                    if(dash_mati == 4)
                    {
                        CancelInvoke("dashwaitend");
                        dash_mati = 0;
                    }
                    x = x + slide[1];
                }
                else
                {
                    
                    x = x + slide[0];
                }
            }
            else
            {
                CancelInvoke("dashwaitend");
                dash_mati = 0;
                x = 0;
            }


        }
        else if (hor == 1 || hor == 3)
        {
            if (manager.larua_attack == 1 && ju == 0 && ss != 0 || manager.larua_attack == 3 && ju == 0 && ss != 0 || jo == 1) //�U�����@��������
            {

                sprite.flipX = true;

                if(jo == 1)//�Ώ���p
                {
                    x = speed;
                }
            }
            else
            {

                if (dash_mati == 2 && !sprite.flipX || dash_mati == 3 && !sprite.flipX)
                {
                    dash_mati = 0;
                }
                else if(dash_mati == 2 || dash_mati == 3) //�_�b�V��
                {
                    dashing = true;
                    if (dash_mati == 2) //���艌
                    {
                        dash_smoke();
                    }
                    if (manager.larua_attack == 2 && ss != 0)//��
                    {
                       
                    }
                    else
                    {
                        sprite.flipX = true;
                    }
                    

                    CancelInvoke("dashwaitend");
                    animator.SetBool("dash", true);
                    dash_mati = 3;

                    //����
                    if (x < dash1 + bs)
                    {
                        if (BE == 0)
                        {
                            
                            
                            if (x < speed)
                            {
                                x = speed;
                            }
                            x = x + 0.5f;
                            if (x >= dash1 + bs)
                            {
                                x = dash1 + bs;
                            }
                        }
                        if(BE == 1)
                        {
                          
                            x = x + 0.3f;
                            if (x >= dash1 + bs)
                            {
                                x = dash1 + bs;
                            }
                        }
                        
                    }
                    else
                    {
                        x = dash1 + bs;
                    }
                  //  else if (x < dash2)
                  //  {
                  //      if (x >= 10.5f)
                  //          animator.SetFloat("Blend", 1.5f);
                  //      x = x + 0.02f;
                  //  }
                  //  else 
                  //  {
                  //      if (x >= 10.5f)
                  //          animator.SetFloat("Blend", 1.5f);
                  //      x = dash2;
                  //  }
                }
                else //����
                {
                    animator.SetBool("dash", false);
                    animator.SetBool("run", true);
                    if (hor == 1)
                    {

                        if(dash_mati != 4)
                            dash_mati = 1;
                        if(dash_mati == 1)
                        {
                            Invoke("dashwaitend", 0.25f);
                        }
                    }
                    else
                    {
                        
                        if (dash_mati != 4)
                        {

                            Invoke("dashwaitend", 0.25f);

                        }
                    }

                    if (manager.larua_attack == 2 && ss != 0 || manager.larua_attack == 4 && ss == 1)//��
                    {

                    }
                    else
                    {
                        sprite.flipX = true;
                    }
                    
                    if (BE == 0)
                    {
                        x = speed + bs;
                    }
                    else if (BE == 1)
                    {
                        x = x + 0.3f;
                        if (x >= speed + bs)
                        {
                            x = speed + bs;
                        }
                    }

                }
            }
        }
        else if (hor == 2 || hor == 4)
        {
            if (manager.larua_attack == 1 && ju == 0 && ss != 0 || manager.larua_attack == 3 && ju == 0 && ss != 0 || jo == 1) //�U�����@��������
            {
                sprite.flipX = false;

                if (jo == 1)//�Ώ���p
                {
                    
                    x = -speed;
                }
            }
            else
            {
                if(dash_mati == 2 && sprite.flipX || dash_mati == 3 && sprite.flipX)
                {
                    dash_mati = 0;
                }
                else if (dash_mati == 2 || dash_mati == 3) //�_�b�V��
                {
                    dashing = true;
                    if (dash_mati == 2) //���艌
                    {
                        dash_smoke();
                    }

                    if (manager.larua_attack == 2 && ss != 0 || manager.larua_attack == 4 && ss == 1) //��
                    {

                    }
                    else
                    {

                        sprite.flipX = false;
                    }
                    CancelInvoke("dashwaitend");
                    animator.SetBool("dash", true);
                    dash_mati = 3;

                    //����
                    
                    if (x > -dash1 - bs)
                    {
                        if (BE == 0)
                        {
                            
                            if (x > -speed)
                            {
                                x = -speed;
                            }
                            x = x - 0.5f;
                            if (x <= -dash1 -bs)
                            {
                                x = -dash1 -bs;
                            }
                        }
                        else if(BE == 1)
                        {
                           
                            x = x - 0.3f;
                            if (x <= -dash1 - bs)
                            {
                                x = -dash1 -bs;
                            }
                        }
                        
                    }
                    else
                    {
                        x = -dash1 -bs;
                    }
                  //  else if (x > -dash2)
                   // {
                  //      if(x <= -10.5f)
                  //      animator.SetFloat("Blend", 1.5f);
                  //      x = x - 0.02f;
                  //  }
                  //  else
                  //  {
                  //      if (x <= -10.5f)
                  //          animator.SetFloat("Blend", 1.5f);
                  //      x = -dash2;
                  //  }
                }
                else //����
                {
                    animator.SetBool("dash", false);
                    animator.SetBool("run", true);
                    if (hor == 2)
                    {
                        if (dash_mati != 4)
                            dash_mati = 1;
                        if (dash_mati == 1)
                        {
                            Invoke("dashwaitend", 0.25f);
                        }
                    }
                    else
                    {
                        if(dash_mati != 4)
                        {
                            Invoke("dashwaitend", 0.25f);
                        }
                    }

                    if (manager.larua_attack == 2 && ss != 0)//��
                    {

                    }
                    else
                    {
                        sprite.flipX = false;
                    }

                    if (BE == 0)
                    {
                        x = -speed - bs;
                    }
                    else if (BE == 1)
                    {
                        x = x - 0.3f;
                        if(x <= -speed - bs)
                        {
                            x = -speed - bs;
                        }
                    }
                }
            }
        }

    } //�ړ�

    void dashwaitend()
    {
        if (dash_mati ==  0 || dash_mati == 4)
        {
            CancelInvoke("dashwaitend");
        }
        dashing = false;
        dash_mati = 4;
    }

    #region �U�����

    public void attack_start()
    {
        switch (manager.larua_attack)
        {
            default://�V���b�g
                if (ss == 0)
                {
                    shot_01();
                    ss = 1;
                }
                if (ss == 2)
                {
                    shot_02();
                    ss = 3;
                }
                break;
            case 2://�\�[�h
                if (ss == 0)
                {
                    sowr();
                }
                break;
            case 3://�n���h�X�s�i�[
                if (ss == 0)
                {
                    spinner();
                    ss = 1;
                }
                break;
            case 4://�A�b�N�X
                if (ss == 0)
                {
                    axe();
                    ss = 1;
                }
                break;



        }
    }

    //�_�b�V��
    public void dash_smoke()
    {
        ssee(5);
        if (sprite.flipX)
        {
            GameObject a = Instantiate(S[4], new Vector3(transform.position.x - 1f, transform.position.y - 0.8f, transform.position.z), Quaternion.identity);
            SpriteRenderer s = a.GetComponent<SpriteRenderer>();
            a.transform.parent = null;
            s.flipX = sprite.flipX;
        }
        else
        {
            GameObject a = Instantiate(S[4], new Vector3(transform.position.x + 1f, transform.position.y - 0.8f, transform.position.z), Quaternion.identity);
            SpriteRenderer s = a.GetComponent<SpriteRenderer>();
            a.transform.parent = null;
            s.flipX = sprite.flipX;
        }
    }

    #region �A�C�e��
    protected void soccer()
    {
        if (sprite.flipX)
        {
            SpriteRenderer a = Instantiate(item[0], new Vector3(transform.position.x + 1f, transform.position.y+ -0.2f, transform.position.z), Quaternion.identity).GetComponentInChildren<SpriteRenderer>();
            Instantiate(item[1], new Vector3(transform.position.x + 0.5f, transform.position.y + -0.5f, transform.position.z), Quaternion.identity).GetComponentInChildren<SpriteRenderer>();
            a.flipX = sprite.flipX;
        }
        else
        {
            SpriteRenderer a = Instantiate(item[0], new Vector3(transform.position.x + -1f, transform.position.y + -0.2f, transform.position.z), Quaternion.identity).GetComponentInChildren<SpriteRenderer>();
            Instantiate(item[1], new Vector3(transform.position.x + -0.5f, transform.position.y + -0.5f, transform.position.z), Quaternion.identity).GetComponentInChildren<SpriteRenderer>();

            a.flipX = sprite.flipX;
        }
        item_icon.item = 0;
    }
    protected void muteki()
    {
        m = true;
        mf = true;
        mutekii.SetActive(true);
        if (C_t != null)
        {
            StopCoroutine(C_t);
        }
        C_t = StartCoroutine(Flash(80, 0.05f));
        GameObject a = Instantiate(mutekii_effect, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Quaternion.identity);
        a.transform.parent = this.transform;
        item_icon.item = 0;
    }
    #endregion

    #region �o�X�^�[
    protected void shot_01()
    {
        ssee(1);
        if (leg.col == 1 && R2.gravityScale == 0)
        {
            animator.Play("larua_shot01");
        }
        else
        {

            animator.Play("larua_shotS01");
        }
    }
    protected void shot_02()
    {
        BB.se(1);
        if (leg.col == 1 && R2.gravityScale == 0)
        {
            animator.Play("larua_shot02");
        }
        else
        {
            animator.Play("larua_shotS02");
        }
    }
    public void shota_01()
    {
        BB.se(1);
        if (sprite.flipX)
        {
            SpriteRenderer a = Instantiate(S[0], new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), Quaternion.identity).GetComponentInChildren<SpriteRenderer>();
            a.flipX = sprite.flipX;
        }
        else
        {
            SpriteRenderer a = Instantiate(S[0], new Vector3(transform.position.x + -0.5f, transform.position.y, transform.position.z), Quaternion.identity).GetComponentInChildren<SpriteRenderer>();
            a.flipX = sprite.flipX;
        }
        ss = 2;
    }
    public void shota_02()
    {
        BB.se(1);
        if (sprite.flipX)
        {
            SpriteRenderer a = Instantiate(S[1], new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), Quaternion.identity).GetComponentInChildren<SpriteRenderer>();
            a.flipX = sprite.flipX;
        }
        else
        {
            SpriteRenderer a = Instantiate(S[1], new Vector3(transform.position.x + -0.5f, transform.position.y, transform.position.z), Quaternion.identity).GetComponentInChildren<SpriteRenderer>();
            a.flipX = sprite.flipX;
        }
    }
    #endregion

    #region ��
    public void sowr()
    {
        ssee(2);
        if (ju == 0)
        {
            animator.Play("LARUA_sowrd");
        }
        else
        {
            animator.Play("LARUA_sowrdA");
        }

    }

    public void sow_01()
    {
        if (sprite.flipX)
        {
            GameObject a = Instantiate(S[2], new Vector3(transform.position.x + 1.4f, transform.position.y+0.2f, transform.position.z), Quaternion.identity);
            SpriteRenderer s = a.GetComponent<SpriteRenderer>();
            a.transform.parent = this.gameObject.transform;
            s.flipX = sprite.flipX;
        }
        else
        {
            GameObject a = Instantiate(S[2], new Vector3(transform.position.x + -1.4f, transform.position.y + 0.2f, transform.position.z), Quaternion.identity);
            SpriteRenderer s = a.GetComponent<SpriteRenderer>();
            a.transform.parent = this.gameObject.transform;
            s.flipX = sprite.flipX;
        }
    }
    public void sow_02()
    {
        if (sprite.flipX)
        {
            GameObject a = Instantiate(S[3], new Vector3(transform.position.x + -1.4f, transform.position.y + 0.2f, transform.position.z), Quaternion.identity);
            SpriteRenderer s = a.GetComponent<SpriteRenderer>();
            a.transform.parent = this.gameObject.transform;
            s.flipX = !sprite.flipX;
        }
        else
        {
            GameObject a = Instantiate(S[3], new Vector3(transform.position.x + 1.4f, transform.position.y + 0.2f, transform.position.z), Quaternion.identity);
            SpriteRenderer s = a.GetComponent<SpriteRenderer>();
            a.transform.parent = this.gameObject.transform;
            s.flipX = !sprite.flipX;
        }
    }
    #endregion

    #region �n���h�X�s�i�[
    public void spinner()
    {
        ssee(1);
        if (ju == 0)
        {
            animator.Play("larua_spinner01");
        }
        else
        {
            animator.Play("larua_spinner02");
        }


    }
    public void spnn()
    {
        if (sprite.flipX)
        {
            GameObject a = Instantiate(S[5], new Vector3(transform.position.x + 2f, transform.position.y + 0f, transform.position.z), Quaternion.identity);
            SpriteRenderer s = a.GetComponent<SpriteRenderer>();
            s.flipX = sprite.flipX;
        }
        else
        {
            GameObject a = Instantiate(S[5], new Vector3(transform.position.x + -2f, transform.position.y + 0f, transform.position.z), Quaternion.identity);
            SpriteRenderer s = a.GetComponent<SpriteRenderer>();
            s.flipX = sprite.flipX;
        }
    }
    #endregion

    #region �A�b�N�X
    public void axe()
    {
        ssee(2);
        animator.Play("LARUA_axe");
    }
    public void axee()
    {
        animator.SetBool("jump", true);
        animator.SetBool("fall", true);
        R2.gravityScale = gg;
        leg.col = 0;
        if (sp == 2)
        {
            y = 22;
            R2.velocity = new Vector2(R2.velocity.x, y);
        }
        else
        {
            if (sprite.flipX)
            {
                animator.SetBool("dash", true);
                dash_smoke();
                dash_mati = 3;
                y = 12;
                R2.velocity = new Vector2(R2.velocity.x, y);
                x = 19;
            }
            else
            {
                animator.SetBool("dash", true);
                dash_smoke();
                dash_mati = 3;
                y = 12;
                R2.velocity = new Vector2(R2.velocity.x, y);
                x = -19;
            }

         }
        ju = 3;
        if (sprite.flipX)
        {
            GameObject a = Instantiate(S[6], new Vector3(transform.position.x + 2f, transform.position.y + 0f, transform.position.z), Quaternion.identity);
            SpriteRenderer s = a.GetComponent<SpriteRenderer>();
            a.transform.parent = this.gameObject.transform;
            s.flipX = sprite.flipX;
        }
        else
        {
            GameObject a = Instantiate(S[6], new Vector3(transform.position.x + -2f, transform.position.y + 0f, transform.position.z), Quaternion.identity);
            SpriteRenderer s = a.GetComponent<SpriteRenderer>();
            a.transform.parent = this.gameObject.transform;
            s.flipX = sprite.flipX;
        }
    }
    #endregion

    public void attack_end() //�U���I��
    {
        if(ju == 0)
        {
            animator.Play("larua");
        }
        else
        {
            animator.Play("larua_jump");
        }
        at = 0;
        if (manager.larua_attack == 4 && ju != 0 && ss != 0)
        {
            ss = 2;
        }
        else
        {
            ss = 0;
        }
    }

    #endregion

    public void initialize_jump() //���n����
    {

        ix = 1;
        y = 0;
        jd = 0;
        animator.SetBool("jump", false);
        animator.SetBool("fall", false);
        if(ss != 0)
        {
            if (manager.larua_attack == 4)
            {
                ss = 0;
            }

                attack_end();

        }
        head.col = 0;
        ju = 0;
    }
    public void Death() //���S
    {
        
        ssee(7);
        BB.pause();
        stage_manager.Hitstop(0.20f,0.5f);
        ju = 3;
        R2.gravityScale = gg;
        animator.SetBool("dash", false);
        animator.SetBool("damage", true);
        animator.Play("larua_damage2");
        R2.bodyType = RigidbodyType2D.Dynamic;
        if (!sprite.flipX)
        {
            R2.velocity = new Vector2(18, 13);
        }
        else
        {
            R2.velocity = new Vector2(-18, 13);
        }

        leg.col = 0;
        koudou = false;
    }
    public void Death2()//�Q�[���I�[�o�[
    {
        if (manager.game == 0)
        {
            jo = 0;
            animator.SetBool("dash", false);
            animator.SetBool("damage", true);
            animator.Play("death");
            R2.bodyType = RigidbodyType2D.Dynamic;
            R2.velocity = new Vector2(0, 0);
            koudou = false;
            BB.gameover();
            manager.Game(2);
        }
    }
    public void Step() //���݂�
    {
        ssee(24);
        jd = 0;
        ss = 0;
        ju = 1;
        animator.SetBool("fall", false);
        animator.SetBool("jump", true);
    }
    public void Step2() //���݂��@�W�����v��
    {
        ssee(0);
        jd = 1;
        ss = 0;
        ju = 1;
        Debug.Log(ju);
        animator.SetBool("fall", false);
        animator.SetBool("jump", true);
    }



    public void Larua_damage(int a ,int jjj = 0)
    {
        stage_manager.Hitstop(0.20f, 0.05f);
        if (jjj != 1 && bar)
        {
            m = true;
            C_t = StartCoroutine(Flash(15, 0.05f));
            barrieroff();
        }
        else
        {
            animator.SetBool("dash", false);
            dash_mati = 0;
            ss = 0;
            m = true;
            if (!mf)
            {
                damage(a);
            }
            jo = jjj;



            if (stage_manager.HP > 0)
            {
                if (jjj == 0)
                {
                    C_t = StartCoroutine(Flash(15, 0.05f));
                    if (ju == 1)
                    {

                        ju = 2;
                    }
                }
                damage_all1(jo);
                if (jjj == 1)
                {
                    bar = false;
                    if (!mf)
                    {
                        sprite.color = new Color(1, 1, 1, 1);
                        if (C_t != null)
                            StopCoroutine(C_t);
                        f = false;
                    }
                    barrieroff();
                    m = true;
                    ju = 3;
                    animator.SetBool("jump", true);
                    jump(0, 28);
                    R2.gravityScale = gg;
                }


            }
            else //���S
            {
                switch (jo)
                {
                    case 1:
                        sprite.enabled = false;
                        Death2();
                        break;
                    default:
                        Death();
                        break;
                }

            }
        }

    }    //�_���[�W

    override public void damage(int a)
    {
        stage_manager.HP -= a;
    }

    public override void damage_all2(bool sky) //�_���[�W������
    {
        animator.SetBool("damage", false);
        jo = 0;
        ss = 0;
        at = 0;
        if (!sky)
        {
            R2.gravityScale = gg;
            R2.bodyType = RigidbodyType2D.Dynamic;
        }
        else
        {
            R2.gravityScale = gg;
        }
        Dk = false;
    }

    //���G
    public override IEnumerator Flash(int a, float b,int Barrier = 0) //�J��Ԃ��A�_�ő��x
    {
        f = true;
        m = true;
        int c = 0;
        while (c < a)
        {
            sprite.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(b);
            sprite.color = new Color(1, 1, 1, 0f);
            yield return new WaitForSeconds(b);
            c++;
        }
        mutekii.SetActive(false);

        if(Barrier == 1 && mf)
        {
            yield return null;
        }
        f = false;
        m = false;
        mf = false;
        sprite.color = new Color(1, 1, 1, 1);
        yield return null;
    }

    #region �G�t�F�N�g
    public void smork()
    {
        GameObject a = Instantiate(E[0], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        SpriteRenderer s = a.GetComponent<SpriteRenderer>();
    }
    #endregion

    #region�@�o���A

    public void barrieron()
    {
        m = true;
        if (C_t != null)
        {
            StopCoroutine(C_t);
        }
        C_t = StartCoroutine(Flash(15, 0.05f,1));
        bareeakanri = Instantiate(bareea,transform.position,transform.rotation);
        bareeakanri.transform.parent = this.gameObject.transform;
        bar = true;
        bbba = bareeakanri.GetComponent<Animator>();
    }
    public void barrieroff()
    {
        bar = false;
        if (bbba)
        {
            bbba.Play("end");
        }
    }

    #endregion

    #region ���ʌ��ʉ�����

    public void ssee(int i)
    {
        BB.se(i);
    }
    #endregion
}

