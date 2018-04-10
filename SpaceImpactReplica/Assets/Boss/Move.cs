using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    private Vector3 _startPosition;
    private Vector3 pos;
    public float MoveSpeed=5f;
    float min = 0f;
    float max = 1f;

    void Start() {
        Vector3 tweak = new Vector3(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
        _startPosition = transform.position + tweak;
    }

    void Update() {
        //_newPosition = transform.position;
        //_newPosition.x += Mathf.Sin(Time.time) * Time.deltaTime;
        //transform.position = _newPosition;
        pos += transform.up * Time.deltaTime * MoveSpeed;
        transform.position = pos + _startPosition + new Vector3(0f, Mathf.Sin(Time.time*2), 0f);
    }
}
