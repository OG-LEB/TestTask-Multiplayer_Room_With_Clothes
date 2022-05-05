using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerAnimationManager _AnimationManager;

    [Header("Movement")]
    [SerializeField] float Speed;
    [SerializeField] Vector3 moveDirection;
    [SerializeField] MoveDirection _MoveDirection;
    PhotonView _PhotonView;
    bool key_w, key_s, key_d, key_a = false;
    [Header("Rottaion")]
    [SerializeField] float RotationSpeed;
    Quaternion LastRotation;
    private enum MoveDirection 
    {
        idle,
        Forward,
        Back,
        Left,
        Right,
        ForwardLeft,
        ForwardRight,
        BackLeft,
        BackRight,
    }

    private void Start()
    {
        _PhotonView = GetComponent<PhotonView>();
    }
    private void Update()
    {
        if (_PhotonView.IsMine)
        {

            SetupVectorFromPcInput();
            SetupPlayerRotation();
            //SetupGFXRotation();

            // Перемещение на вектор
            transform.Translate(moveDirection * Time.deltaTime * Speed);
        }
    }
    private void SetupPlayerRotation() 
    {
        if (key_a)
        {
            _MoveDirection = MoveDirection.Left;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, -90, 0), RotationSpeed * Time.deltaTime);
            LastRotation = transform.rotation;
        }
        else if (key_d)
        {
            _MoveDirection = MoveDirection.Right;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 90, 0), RotationSpeed * Time.deltaTime);
            LastRotation = transform.rotation;
        }
        else if (key_w)
        {
            _MoveDirection = MoveDirection.Forward;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), RotationSpeed * Time.deltaTime);
            LastRotation = transform.rotation;
        }
        else if (key_s)
        {
            _MoveDirection = MoveDirection.Back;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 180, 0), RotationSpeed * Time.deltaTime);
            LastRotation = transform.rotation;
        }
        if (key_a && key_w)
        {
            _MoveDirection = MoveDirection.ForwardLeft;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, -45, 0), RotationSpeed * Time.deltaTime);
            LastRotation = transform.rotation;
        }
        if (key_w && key_d)
        {
            _MoveDirection = MoveDirection.ForwardRight;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 45, 0), RotationSpeed * Time.deltaTime);
            LastRotation = transform.rotation;
        }
        if (key_d && key_s)
        {
            _MoveDirection = MoveDirection.BackRight;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 135, 0), RotationSpeed * Time.deltaTime);
            LastRotation = transform.rotation;
        }
        if (key_s && key_a)
        {
            _MoveDirection = MoveDirection.BackLeft;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, -135, 0), RotationSpeed * Time.deltaTime);
            LastRotation = transform.rotation;
        }
        else
        {
            //idle, fixes bug
            transform.rotation = LastRotation;
        }
    }
    private void SetupVectorFromPcInput() 
    {
        // Forward "W"
        if (key_w || key_s || key_a || key_d)
        {
            moveDirection = new Vector3(0, 0, 1);
        }
        else
        {
            moveDirection = new Vector3(0, 0, 0);
            _AnimationManager.StopWalking();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            key_w = true;
            //_MoveDirection = MoveDirection.Forward;
            _AnimationManager.StartWalking();
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            key_w = false;
           //moveDirection = new Vector3(0, 0, 0);
        }
        // Back "S"
        if (Input.GetKeyDown(KeyCode.S))
        {
            key_s = true;
            //_MoveDirection = MoveDirection.Back;
            //moveDirection = new Vector3(0, 0, 1);
            _AnimationManager.StartWalking();
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            key_s = false;
            //moveDirection = new Vector3(0, 0, 0);
            //_AnimationManager.StopWalking();
        }
        // Left "A"
        if (Input.GetKeyDown(KeyCode.A))
        {
            key_a = true;
            //_MoveDirection = MoveDirection.Left;
            //moveDirection = new Vector3(0, 0, 1);
            _AnimationManager.StartWalking();
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            key_a = false;
            //moveDirection = new Vector3(0, 0, 0);
        }
        // Right "D"
        if (Input.GetKeyDown(KeyCode.D))
        {
            key_d = true;
            //_MoveDirection = MoveDirection.Right;
            //moveDirection = new Vector3(0, 0, 1);
            _AnimationManager.StartWalking();
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            key_d = false;
            //moveDirection = new Vector3(0, 0, 0);
        }

        //if (moveDirection.x == 0)
        //{
        //    _AnimationManager.StopWalking();
        //}
    }
    private void SetupGFXRotation() 
    {
        if (moveDirection.z == 1)
        {
            //Forward
            //GFX.rotation = Quaternion.Lerp(GFX.rotation,Quaternion.Euler(0, 0, 0), RotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), RotationSpeed * Time.deltaTime);
            _MoveDirection = MoveDirection.Forward;
            if (moveDirection.x == -1)
            {
                //ForwardLeft
                transform.rotation = Quaternion.Euler(0, -45, 0);
                //GFX.rotation = Quaternion.Lerp(GFX.rotation, Quaternion.Euler(0, -45, 0), RotationSpeed * Time.deltaTime);
                //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, -45, 0), RotationSpeed * Time.deltaTime);
                _MoveDirection = MoveDirection.ForwardLeft;
            }
            else if (moveDirection.x == 1)
            {
                //ForwardRight
                transform.rotation = Quaternion.Euler(0, 45, 0);
                //GFX.rotation = Quaternion.Lerp(GFX.rotation, Quaternion.Euler(0, 45, 0), RotationSpeed * Time.deltaTime);
                //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 45, 0), RotationSpeed * Time.deltaTime);
                _MoveDirection = MoveDirection.ForwardRight;
            }
        }
        else if (moveDirection.z == -1)
        {
            //Back
            //GFX.rotation = Quaternion.Lerp(GFX.rotation, Quaternion.Euler(0, 180, 0), RotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 180, 0), RotationSpeed * Time.deltaTime);
            _MoveDirection = MoveDirection.Back;
            if (moveDirection.x == -1)
            {
                //BackLeft
                //GFX.rotation = Quaternion.Lerp(GFX.rotation, Quaternion.Euler(0, -135, 0), RotationSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, -135, 0);
                //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, -135, 0), RotationSpeed * Time.deltaTime);
                _MoveDirection = MoveDirection.BackLeft;
            }
            else if (moveDirection.x == 1)
            {
                //BackRight
                //GFX.rotation = Quaternion.Lerp(GFX.rotation, Quaternion.Euler(0, 135, 0), RotationSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 135, 0);
                //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 135, 0), RotationSpeed * Time.deltaTime);
                _MoveDirection = MoveDirection.BackRight;
            }
        }
        else if (moveDirection.x == -1)
        {
            //Left
            //GFX.rotation = Quaternion.Lerp(GFX.rotation, Quaternion.Euler(0, -90, 0), RotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, -90, 0);
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, -90, 0), RotationSpeed * Time.deltaTime);
            _MoveDirection = MoveDirection.Left;
            if (moveDirection.z == 1)
            {
                //ForwardLeft
                //GFX.rotation = Quaternion.Lerp(GFX.rotation, Quaternion.Euler(0, -45, 0), RotationSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, -45, 0);
                //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, -45, 0), RotationSpeed * Time.deltaTime);
                _MoveDirection = MoveDirection.ForwardLeft;
            }
            else if (moveDirection.z == -1)
            {
                //
                //GFX.rotation = Quaternion.Lerp(GFX.rotation, Quaternion.Euler(0, -135, 0), RotationSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, -135, 0);
                //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, -135, 0), RotationSpeed * Time.deltaTime);
                _MoveDirection = MoveDirection.BackLeft;
            }
        }
        else if (moveDirection.x == 1)
        {
            //Right
            //GFX.rotation = Quaternion.Lerp(GFX.rotation, Quaternion.Euler(0, 90, 0), RotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 90, 0);
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 90, 0), RotationSpeed * Time.deltaTime);
            _MoveDirection = MoveDirection.Right;
            if (moveDirection.z == 1)
            {
                //ForwardRight
                //GFX.rotation = Quaternion.Lerp(GFX.rotation, Quaternion.Euler(0, 45, 0), RotationSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 45, 0);
                //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 45, 0), RotationSpeed * Time.deltaTime);
                _MoveDirection = MoveDirection.ForwardRight;
            }
            else if (moveDirection.z == -1)
            {
                //BackRight
                //GFX.rotation = Quaternion.Lerp(GFX.rotation, Quaternion.Euler(0, 135, 0), RotationSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 135, 0);
                //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 135, 0), RotationSpeed * Time.deltaTime);
                _MoveDirection = MoveDirection.BackRight;
            }
        }

        
    }
}
