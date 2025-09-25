using UnityEngine;
using UnityEngine.UI;

public class CatchModel : MonoBehaviour
{
    [Header("�������")]
    public float accelerationA ; // A�ļ��ٶ�
    public float accelerationB; // B�ļ��ٶ�
    public float velocityA; // A�ĳ�ʼ�ٶ�
    public float velocityB; // B�ĳ�ʼ�ٶ�
    public float initialSpacing;

    [Header("�ο�����")]
    public GameObject cylinderA; // A��
    public GameObject cylinderB; // B��


    // �������
    private Rigidbody rbA;
    private Rigidbody rbB;

    // ��ʼλ��
    private Vector3 initialPosA;
    private Vector3 initialPosB;
    public Button BtnDataRefresh;
    public Button Puase;
    private DataControl dataControl;
    void Start()
    {
        // �����ʼλ��
        initialPosA = cylinderB.transform.position;
        initialPosB = new Vector3(0, 0.5f, initialSpacing);
        rbA = cylinderA.GetComponent<Rigidbody>();
        rbB = cylinderB.GetComponent<Rigidbody>();
        InitVelocity();
        // ��ʼ������λ�úʹ�С
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
            // ����A�Ĵ�С��λ�ã���ϸ��
            cylinderA.transform.position = initialPosA;

            // ����B�Ĵ�С��λ�ã��ϴ֣�
            cylinderB.transform.position = initialPosB;
        }
    }

    void FixedUpdate()
    {
        // Ӧ���Զ�������
        ApplyCustomAcceration();

        // ������ײʱ�����
        dataControl.SetData(rbA.velocity.z.ToString("F2"), rbB.velocity.z.ToString("F2"), (cylinderB.transform.position.z - cylinderA.transform.position.z).ToString("F2"));
    }

    void ApplyCustomAcceration()
    {
        // ��AӦ���ٶ�
        if (accelerationA != 0) 
        {
            rbA.velocity += Vector3.forward * accelerationA * Time.deltaTime;
        }

        // ��BӦ���ٶ�
        if (accelerationB != 0) 
        {
            rbB.velocity += Vector3.forward * accelerationB * Time.deltaTime;
        }
    }

    // ����ģ��
    public void ResetSimulation()
    {
        // ��������λ�ú��ٶ�
        cylinderA.transform.position = initialPosA;
        cylinderB.transform.position = initialPosB;

        InitVelocity();
    }

    // ��Inspector����ʾ��ײʱ��
    void OnGUI()
    {
        //GUILayout.BeginArea(new Rect(10, 10, 300, 120));
        //GUILayout.Label("A�ٶ�: " + rbA.velocity.z);
        //GUILayout.Label("B�ٶ�: " + rbB.velocity.z);
        //GUILayout.Label("AB���: " + ());
        //if (GUILayout.Button("����ģ��"))
        //{
        //    ResetSimulation();
        //}
        //
        //GUILayout.EndArea();
    }
}