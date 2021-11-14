using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Camera bowCam;
    Rigidbody rb;
    RaycastHit hit;
    CapsuleCollider capsuleCollider;
    public float speed;
    public float jumpPower = 5f;
    public float sensitivity = 1f;
    public float currentCamRotate;
    public bool isGround = false;
    private float camRotateLimit = 90f;
    public float player_Hp = 100;
    public Image HpBar;
    public Text HpText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    
    void Update()
    {
        Move();
        BodyRotate();
        CamRotate();
        Jump();
        Hp();
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveX = transform.right * h;
        Vector3 moveZ = transform.forward * v;
        Vector3 velocity = (moveX + moveZ).normalized * speed;

        rb.MovePosition(transform.position + velocity * Time.deltaTime);

        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = 20f;
        }
        else
        {
            speed = 10f;
        }
    }

    void BodyRotate()
    {
        float X = Input.GetAxis("Mouse X");
        Vector3 rotation = new Vector3(0f, X, 0f) * sensitivity;
        rb.MoveRotation(rb.rotation *  Quaternion.Euler(rotation));
    }

    void CamRotate()
    {
        float Y = Input.GetAxis("Mouse Y");
        float camRotate = Y * sensitivity;
        currentCamRotate -= camRotate;
        currentCamRotate = Mathf.Clamp(currentCamRotate, -camRotateLimit, camRotateLimit);
        Camera.main.transform.localEulerAngles = new Vector3(currentCamRotate, 0f, 0f);
    }

    void Jump()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);

        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.velocity = transform.up * jumpPower;
        }
    }

    void Hp()
    {
        HpBar.fillAmount = player_Hp / 100;
        HpText.text = player_Hp.ToString();

        if(player_Hp >=100)
        {
            player_Hp = 100;
        }
        if(player_Hp <= 0)
        {
            player_Hp = 0;
        }
    }

    public void HpPlus(float a)
    {
        player_Hp += a;
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag == "Enemy")
        {
            EnemyAttack attack = other.gameObject.GetComponent<EnemyAttack>();
            HpPlus(-attack.attackDamage);
        }
    }
}
