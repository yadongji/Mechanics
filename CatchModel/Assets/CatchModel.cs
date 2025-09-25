using UnityEngine;
using UnityEngine.UI;

public class CatchModel : MonoBehaviour
{
    [Header("物体参数")]
    public float accelerationA ; // A的加速度
    public float accelerationB; // B的加速度
    public float velocityA; // A的初始速度
    public float velocityB; // B的初始速度
    public float initialSpacing;

    [Header("参考物体")]
    public GameObject cylinderA; // A车
    public GameObject cylinderB; // B车


    // 刚体组件
    private Rigidbody rbA;
    private Rigidbody rbB;

    // 初始位置
    private Vector3 initialPosA;
    private Vector3 initialPosB;
    public Button BtnDataRefresh;
    public Button Puase;
    private DataControl dataControl;
    void Start()
    {
        // 保存初始位置
        initialPosA = cylinderB.transform.position;
        initialPosB = new Vector3(0, 0.5f, initialSpacing);
        rbA = cylinderA.GetComponent<Rigidbody>();
        rbB = cylinderB.GetComponent<Rigidbody>();
        InitVelocity();
        // 初始化物体位置和大小
        InitializeCylinders();
        dataControl = GameObject.Find("Canvas").GetComponent<DataControl>();
        BtnDataRefresh.onClick.AddListener(ResetSimulation);
    }

    void InitVelocity() 
    {
        rbA.velocity = new Vector3(0, 0, velocityA);
        rbB.velocity = new Vector3(0, 0, velocityB);
    }

    void InitializeCylinders()
    {
        if (cylinderA && cylinderB)
        {
            // 设置A的大小和位置（较细）
            cylinderA.transform.position = initialPosA;

            // 设置B的大小和位置（较粗）
            cylinderB.transform.position = initialPosB;
        }
    }

    void FixedUpdate()
    {
        // 应用自定义重力
        ApplyCustomAcceration();

        // 更新碰撞时间计算
        dataControl.SetData(rbA.velocity.z.ToString("F2"), rbB.velocity.z.ToString("F2"), (cylinderB.transform.position.z - cylinderA.transform.position.z).ToString("F2"));
    }

    void ApplyCustomAcceration()
    {
        // 对A应用速度
        if (accelerationA != 0) 
        {
            rbA.velocity += Vector3.forward * accelerationA * Time.deltaTime;
        }

        // 对B应用速度
        if (accelerationB != 0) 
        {
            rbB.velocity += Vector3.forward * accelerationB * Time.deltaTime;
        }
    }

    // 重置模拟
    public void ResetSimulation()
    {
        // 重置物体位置和速度
        cylinderA.transform.position = initialPosA;
        cylinderB.transform.position = initialPosB;

        InitVelocity();
    }

    // 在Inspector中显示碰撞时间
    void OnGUI()
    {
        //GUILayout.BeginArea(new Rect(10, 10, 300, 120));
        //GUILayout.Label("A速度: " + rbA.velocity.z);
        //GUILayout.Label("B速度: " + rbB.velocity.z);
        //GUILayout.Label("AB间距: " + ());
        //if (GUILayout.Button("重置模拟"))
        //{
        //    ResetSimulation();
        //}
        //
        //GUILayout.EndArea();
    }
}