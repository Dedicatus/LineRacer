using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviourPunCallbacks
{
    [Header("StunDebug")]
    [SerializeField]
    private float stunTimer;
    [SerializeField]
    public float stunTime;
    public bool isStun = false;
    

    public enum PlayerState { IDLING, MOVING, DASHING };
    public enum PlayerOrder { Player1, Player2, Player3, Player4 };
    [SerializeField] private PlayerState state;
    [SerializeField] private PlayerOrder order;

    private PhotonView myPhotonView;

    Rigidbody rigidBody;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float turnSpeed = 250f;

    float cameraRotationY;

    // Start is called before the first frame update
    void Start()
    {
        stunTimer = 0;
        state = PlayerState.IDLING;
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
        myPhotonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (myPhotonView.IsMine)
        {
                inputHandler();
        }

        if (isStun) {
            stunTimer += Time.deltaTime;
            if (stunTimer >= stunTime) {
                isStun = false;
                stunTimer = 0;
            }
        }
  
    }

    private void inputHandler()
    {
        if (GameObject.FindWithTag("GameController").GetComponent<GameController>().getCurState() == GameController.GameState.Playing)
        {
            if (PhotonNetwork.OfflineMode)
            {
                localInputHandler();
            }
        }
    }

    private void localInputHandler()
    {
        //XBOX Controller
        if (Mathf.Abs(Input.GetAxis("Horizontal_L")) > 0.19f || Mathf.Abs(Input.GetAxis("Vertical_L")) > 0.19f)
        {
            float x = Input.GetAxis("Horizontal_L"), y = Input.GetAxis("Vertical_L");
            transform.eulerAngles = new Vector3(0, cameraRotationY, 0);
            float angle = getAngle(x, y), currentAngle = (transform.localEulerAngles.y % 360 + 360) % 360;
            transform.eulerAngles = new Vector3(0, angle + currentAngle, 0);
            //transform.Rotate(Vector3.up,angle- currentAngle);
            //Debug.Log("camera:"+cameraRotationY);
            //Debug.Log("character:"+angle);
            //rigidBody.AddForce(transform.forward * moveSpeed);
            state = PlayerState.MOVING;
            rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            state = PlayerState.IDLING;

            //Keyboard
            if (order == PlayerOrder.Player1)
            {
                if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
                {
                    transform.rotation = Quaternion.Euler(0, 45, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }
                else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
                {
                    transform.rotation = Quaternion.Euler(0, 315, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }
                else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
                {
                    transform.rotation = Quaternion.Euler(0, 135, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }
                else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
                {
                    transform.rotation = Quaternion.Euler(0, 225, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }
                else if (Input.GetKey(KeyCode.W))
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    transform.rotation = Quaternion.Euler(0, 270, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }

                else if (Input.GetKey(KeyCode.D))
                {
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }

            }
            else if (order == PlayerOrder.Player2)
            {
                if (Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.L))
                {
                    transform.rotation = Quaternion.Euler(0, 45, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }
                else if (Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.J))
                {
                    transform.rotation = Quaternion.Euler(0, 315, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }
                else if (Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.L))
                {
                    transform.rotation = Quaternion.Euler(0, 135, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }
                else if (Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.J))
                {
                    transform.rotation = Quaternion.Euler(0, 225, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }
                else if (Input.GetKey(KeyCode.I))
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }
                else if (Input.GetKey(KeyCode.K))
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }
                else if (Input.GetKey(KeyCode.J))
                {
                    transform.rotation = Quaternion.Euler(0, 270, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }

                else if (Input.GetKey(KeyCode.L))
                {
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }
            }
            else if(order == PlayerOrder.Player3)
            {
                if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
                {
                    transform.rotation = Quaternion.Euler(0, 45, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }
                else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.rotation = Quaternion.Euler(0, 315, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }
                else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
                {
                    transform.rotation = Quaternion.Euler(0, 135, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }
                else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.rotation = Quaternion.Euler(0, 225, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }
                else if (Input.GetKey(KeyCode.UpArrow))
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.rotation = Quaternion.Euler(0, 270, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }

                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }
            }
            else if (order == PlayerOrder.Player4)
            {
                if (Input.GetKey(KeyCode.Keypad5) && Input.GetKey(KeyCode.Keypad3))
                {
                    transform.rotation = Quaternion.Euler(0, 45, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }
                else if (Input.GetKey(KeyCode.Keypad5) && Input.GetKey(KeyCode.Keypad1))
                {
                    transform.rotation = Quaternion.Euler(0, 315, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }
                else if (Input.GetKey(KeyCode.Keypad2) && Input.GetKey(KeyCode.Keypad3))
                {
                    transform.rotation = Quaternion.Euler(0, 135, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }
                else if (Input.GetKey(KeyCode.Keypad2) && Input.GetKey(KeyCode.Keypad1))
                {
                    transform.rotation = Quaternion.Euler(0, 225, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }
                else if (Input.GetKey(KeyCode.Keypad5))
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }
                else if (Input.GetKey(KeyCode.Keypad2))
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }
                else if (Input.GetKey(KeyCode.Keypad1))
                {
                    transform.rotation = Quaternion.Euler(0, 270, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }

                else if (Input.GetKey(KeyCode.Keypad3))
                {
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                    state = PlayerState.MOVING;
                }
            }
        }
    }

    float getAngle(float x, float y)
    {
        float theta = Mathf.Atan2(x, y) - Mathf.Atan2(0, 1.0f);
        if (theta > (float)Mathf.PI)
            theta -= (float)Mathf.PI;
        if (theta < -(float)Mathf.PI)
            theta += (float)Mathf.PI;

        theta = (float)(theta * 180.0f / (float)Mathf.PI);
        return theta;

    }

    public void changeCameraY(float y)
    {
        cameraRotationY = y;
    }

    public float getMoveSpeed()
    {
        return moveSpeed;
    }

    public PlayerState getState()
    {
        return state;
    }
    public PlayerOrder getOrder()
    {
        return order;
    }
}