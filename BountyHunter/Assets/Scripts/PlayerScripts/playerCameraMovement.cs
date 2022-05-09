using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCameraMovement : MonoBehaviour
{
    [Header("Camera control values")]
    private const float YMin = -50.0f;
    private const float YMax = 50.0f;

    [SerializeField]public Transform lookAt;

    [SerializeField]public Transform Player;

    [SerializeField] public float distance = 5.0f;
    [SerializeField] private float currentX = 0.0f;
    [SerializeField] private float currentY = 0.0f;
    [SerializeField] public float sensivity = 4.0f;

    float aimDir = 50.0f;
    bool isRight = true;

    // Start is called before the first frame update
    void Start()
    {
        gameEvents.current.setPlayer(GameObject.FindGameObjectWithTag("Player"));
        Player = gameEvents.current.playerObject.GetComponent<Transform>();

    }

    // Update is called once per frame
    void LateUpdate()
    {

        currentX += Input.GetAxis("Mouse X") * sensivity * Time.deltaTime;
        currentY += Input.GetAxis("Mouse Y") * sensivity * Time.deltaTime;

        currentY = Mathf.Clamp(currentY, YMin, YMax);

        Vector3 Direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        //add an aiming offset
        transform.position = lookAt.position + rotation * Direction;

        transform.LookAt(lookAt.position);

        //makes player face the same direction as camera on the y axis
        if(Player.GetComponent<playerMovement>().my_ragdoll_state != playerMovement.RagdollState.isRagdoll)
        {
            Player.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        }
        
    }
}
