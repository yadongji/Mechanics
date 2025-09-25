using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatchModel : MonoBehaviour
{
    [Header("�������")]
    public float accelerationA ; // A�ļ��ٶ�
    public float accelerationB; // B�ļ��ٶ�
    public float velocityA; // A�ĳ�ʼ�ٶ�
    public float velocityB; // B�ĳ�ʼ�ٶ�
    public float initialSpacing;//AB��ʼ���

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
    private bool pausing;
    void Start()
    {
        // �����ʼλ��
        initialPosA = cylinderB.transform.position;
        initialPosB = new Vector3(-2, 0.5f, initialSpacing);
        rbA = cylinderA.GetComponent<Rigidbody>();
        rbB = cylinderB.GetComponent<Rigidbody>();
        InitVelocity();
        // ��ʼ������λ�úʹ�С
        InitializeCylinders();
        dataControl = GameObject.Find("Canvas").GetComponent<DataControl>();
        BtnDataRefresh.onClick.AddListener(ResetSimulation);
        Puase.onClick.AddListener(Pause);
    }

    public void Pause(bool pause)
    {
        pausing = pause;
        if (pausing)
        {
            Time.timeScale = 0f;
            Puase.GetComponentInChildren<TextMeshProUGUI>().text = "Pause";
        }
        else
        {
            Time.timeScale = 1f;
            Puase.GetComponentInChildren<TextMeshProUGUI>().text = "Start";
        }
    }

    public void Pause()
    {
        pausing = !pausing;
        if (pausing)
        {
            Time.timeScale = 0f;
            Puase.GetComponentInChildren<TextMeshProUGUI>().text = "Pause";
        }
        else 
        {
            Time.timeScale = 1f;
            Puase.GetComponentInChildren<TextMeshProUGUI>().text = "Start";
        }
    }

    public void RefreshData(string accelerationA,string accelerationB,string velocityA,string velocityB,string initialSpacing) 
    {
        this.accelerationA = float.Parse(accelerationA);
        this.accelerationB = float.Parse(accelerationB);
        this.velocityA = float.Parse(velocityA);
        this.velocityB = float.Parse(velocityB);
        this.initialSpacing = float.Parse(initialSpacing);
        InitVelocity();
        InitializeCylinders();
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
            if (rbA.velocity.z <= 0) 
            {
                rbA.velocity = new Vector3(0, 0, 0);
            }
        }

        // ��BӦ���ٶ�
        if (accelerationB != 0) 
        {
            rbB.velocity += Vector3.forward * accelerationB * Time.deltaTime / 2;
        }
        if (rbA.velocity.z == rbB.velocity.z) 
        {
            Pause();
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
}