using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private GameObject Legs;

    private Animator _playerAnimator;
    private float _moveSpeed = 7f;

    private void Start()
    {
        _playerAnimator = this.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        MovementControl();

        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            _playerAnimator.SetBool("IsRunning", false);
            Legs.GetComponent<Animator>().SetBool("IsRunning", false);
        }
        else
        {
            _playerAnimator.SetBool("IsRunning", true);
            Legs.GetComponent<Animator>().SetBool("IsRunning", true);
        }
    }

    private void MovementControl()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector2.up * _moveSpeed * Time.deltaTime);
            Legs.transform.rotation = Quaternion.Euler(0, 0, 180);
        }

        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(Vector2.down * _moveSpeed * Time.deltaTime);
            Legs.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(Vector2.right * _moveSpeed * Time.deltaTime);
            Legs.transform.rotation = Quaternion.Euler(0, 0, 90);
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(Vector2.left * _moveSpeed * Time.deltaTime);
            Legs.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }
}
