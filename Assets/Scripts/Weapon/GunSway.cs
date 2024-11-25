using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSway : MonoBehaviour
{
    public float intensity;
    public float smooth;

    private Quaternion targetRotation, originRotation, adjustmentX, adjustmentY;

    public bool isMine;


    // Start is called before the first frame update
    void Start()
    {
        originRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Pause.paused) return;
        UpdateSway();
    }

    private void UpdateSway()
    {
        //controls use new input system
        float mouseInputY = Input.GetAxis("Mouse Y");
        float mouseInputX = Input.GetAxis("Mouse X");

        if (!isMine)
        {
            mouseInputY = 0;
            mouseInputX = 0;
        }
        //calculate adjustments
        adjustmentX = Quaternion.AngleAxis(-intensity * mouseInputX, Vector3.up);
        adjustmentY = Quaternion.AngleAxis(intensity * mouseInputY, Vector3.right);
        targetRotation = adjustmentX * adjustmentY * originRotation;

        //rotate towards target
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * smooth);


    }
}
