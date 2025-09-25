using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataRefresh : MonoBehaviour
{
    public TMP_InputField accelerationA;
    public TMP_InputField accelerationB;
    public TMP_InputField vilocityA;
    public TMP_InputField vilocityB;
    public TMP_InputField initialSpacing;
    private Button confirmBtn;
    private CatchModel catchModel;
    public Button activeBtn;
    // Start is called before the first frame update
    void Start()
    {
        confirmBtn = transform.Find("confirmBtn").GetComponent<Button>();
        confirmBtn.onClick.AddListener(RefreshData);
        catchModel = GameObject.Find("Plane").GetComponent<CatchModel>();
        catchModel.Pause(true);
        activeBtn.onClick.AddListener(() => { gameObject.SetActive(true); catchModel.Pause(true); activeBtn.gameObject.SetActive(false); });
    }

    void RefreshData() 
    {
        if (accelerationA.text != null && accelerationB.text != null && vilocityA.text != null && vilocityB.text != null && initialSpacing.text != null) {
            catchModel.RefreshData(accelerationA.text, accelerationB.text, vilocityA.text, vilocityB.text, initialSpacing.text);
            catchModel.Pause(false);
            gameObject.SetActive(false);
            activeBtn.gameObject.SetActive(true);
        }
    }
}
