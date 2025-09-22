using System.Collections;
using UnityEngine;

public class FallingModel : MonoBehaviour
{
    [Header("物体参数")]
    public float h = 2.0f; // 圆柱体长度
    public float gravityA = 9.8f; // A的重力加速度
    public float gravityB = 9.8f; // B的重力加速度
    public float cylinderARadius = 0.2f; // A的半径
    public float cylinderBRadius = 0.5f; // B的半径

    [Header("参考物体")]
    public GameObject cylinderA; // 细圆柱体A
    public GameObject cylinderB; // 粗圆柱体B

    // 物理状态
    private bool isFallingA = true;
    private bool isFallingB = false;
    private bool collisionOccured = false;
    private float collisionStartTime = 0f;
    private float collisionDuration = 0f;

    // 刚体组件
    private Rigidbody rbA;
    private Rigidbody rbB;

    // 初始位置
    private Vector3 initialPosA;
    private Vector3 initialPosB;
    void Start()
    {
        // 保存初始位置
        initialPosB = cylinderB.transform.position;
        initialPosA = new Vector3(0, initialPosB.y + 2*h, 0);

        // 初始化物体位置和大小
        InitializeCylinders();

        // 获取或添加刚体组件
        SetupRigidbodies();
    }

    void InitializeCylinders()
    {
        if (cylinderA && cylinderB)
        {
            // 设置A的大小和位置（较细）
            cylinderA.transform.localScale = new Vector3(cylinderARadius * 2, h / 2, cylinderARadius * 2);
            cylinderA.transform.position = initialPosA;

            // 设置B的大小和位置（较粗）
            cylinderB.transform.localScale = new Vector3(cylinderBRadius * 2, h / 2, cylinderBRadius * 2);
            cylinderB.transform.position = initialPosB;
        }
    }

    void SetupRigidbodies()
    {
        // 为A添加刚体并设置初始状态
        rbA = cylinderA.GetComponent<Rigidbody>();
        if (rbA == null)
            rbA = cylinderA.AddComponent<Rigidbody>();

        rbA.useGravity = false; // 禁用Unity默认重力，使用自定义重力
        rbA.collisionDetectionMode = CollisionDetectionMode.Continuous; // 连续碰撞检测，防止穿透

        // 为B添加刚体并设置初始状态
        rbB = cylinderB.GetComponent<Rigidbody>();
        if (rbB == null)
            rbB = cylinderB.AddComponent<Rigidbody>();

        rbB.useGravity = false; // 禁用Unity默认重力
        rbB.isKinematic = true; // 初始时B为运动学刚体，不受物理影响
    }

    void Update()
    {
        // 应用自定义重力
        ApplyCustomGravity();

        // 更新碰撞时间计算
    }

    void ApplyCustomGravity()
    {
        // 对A应用重力
        if (isFallingA && rbA)
        {
            rbA.velocity += Vector3.down * gravityA * Time.deltaTime;
        }

        // 对B应用重力（仅在碰撞后）
        if (isFallingB && rbB)
        {
            rbB.velocity += Vector3.down * gravityB * Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        // 检测A与B的碰撞
        if ((collision.gameObject == cylinderA && gameObject == cylinderB))
        {
            if (!collisionOccured)
            {
                collisionOccured = true;
                isFallingB = true; // B开始自由落体
                rbB.isKinematic = false; // 启用B的物理模拟
                collisionStartTime = Time.time;

                Debug.Log("碰撞发生！B开始自由落体");
            }
        }
    }

    void OnTriggerExit(Collider collision)
    {
        // 检测A离开B的碰撞
        if ((collision.gameObject == cylinderA && gameObject == cylinderB))
        {
            if (collisionOccured)
            {
                collisionDuration = Time.time - collisionStartTime;
                Debug.Log("A穿过B的时间(位置检测): " + collisionDuration + "秒");
            }
        }
    }

    // 重置模拟
    public void ResetSimulation()
    {
        // 重置物理状态
        isFallingA = true;
        isFallingB = false;
        collisionOccured = false;
        collisionStartTime = 0f;
        collisionDuration = 0f;

        // 重置物体位置和速度
        cylinderA.transform.position = initialPosA;
        cylinderB.transform.position = initialPosB;

        if (rbA) rbA.velocity = Vector3.zero;
        if (rbB)
        {
            rbB.velocity = Vector3.zero;
            rbB.isKinematic = true;
        }

        Debug.Log("模拟已重置");
    }

    // 在Inspector中显示碰撞时间
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 120));
        GUILayout.Label("A穿过B的时间: " + (collisionDuration > 0 ? collisionDuration.ToString("F3") + "秒" : "尚未计算"));
        GUILayout.Label("A位置: " + cylinderA.transform.position.y.ToString("F2"));
        GUILayout.Label("B位置: " + cylinderB.transform.position.y.ToString("F2"));

        if (GUILayout.Button("重置模拟"))
        {
            ResetSimulation();
        }

        GUILayout.EndArea();
    }
}