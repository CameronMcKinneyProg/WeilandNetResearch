using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;
    public string username;

    public Vector3 position;
    public Quaternion rotation;

    private float moveSpeed = 5f / Constants.TICKS_PER_SEC;
    private bool[] inputs;

    public Player(int _id, string _username, Vector3 _spawnPosition)
    {
        id = _id;
        username = _username;
        position = _spawnPosition;
        rotation = Quaternion.identity;

        inputs = new bool[4];
    }

    public void Update()
    {
        Vector2 _inputDirection = Vector2.zero;
        if (inputs[0])
        {
            _inputDirection.y += 1;
        }
        if (inputs[1])
        {
            _inputDirection.y -= 1;
        }
        if (inputs[2])
        {
            _inputDirection.x += 1;
        }
        if (inputs[3])
        {
            _inputDirection.x -= 1;
        }

        Move(_inputDirection);
    }

    private void Move(Vector2 _inputDirection)
    {
        Vector3 _forward = Vector3.Transform(new Vector3(0, 0, 1), rotation);
        Vector3 _right = Vector3.Normalize(Vector3.Cross(_forward, new Vector3(0, 1, 0)));

        Vector3 _moveDirection = _right * _inputDirection.x + _forward * _inputDirection.x;
        position += _moveDirection * moveSpeed;

        ServerSend.PlayerPosition(this);
        ServerSend.PlayerRotation(this);
    }

    public void SetInput(bool[] _inputs, Quaternion _rotation)
    {
        inputs = _inputs;
        rotation = _rotation;
    }
}