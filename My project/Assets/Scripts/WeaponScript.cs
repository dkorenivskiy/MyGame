using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public Vector3 PositionInHands;
    public int SlotInWeapons;

    private bool _isPicked;
    private bool _pickUpAllowed;

    private Collider2D _player;
    private Animator _weaponAnimator;

    private void Start()
    {
        if (this.transform.parent == null)
        {
            _isPicked = false;
        }
        else
        {
            _isPicked = true;
        }

        _weaponAnimator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (!_isPicked)
        {
            _weaponAnimator.SetBool("IsPicked", false);
            this.transform.rotation = Quaternion.Euler(0, 0, 0);

            if (Input.GetKeyDown(KeyCode.E) && _pickUpAllowed)
            {
                if (_player.gameObject.GetComponentInParent<PlayerWeapons>().PlayerGuns[SlotInWeapons] == null)
                {
                    this.transform.rotation = _player.transform.GetChild(1).rotation;
                    this.transform.parent = _player.gameObject.transform;
                    this.gameObject.SetActive(false);
                    _isPicked = true;
                }
            }
        }
        else
        {
            _weaponAnimator.SetBool("IsPicked", true);
            this.transform.localPosition = PositionInHands;

            if (Input.GetKeyDown(KeyCode.Q))
            {
                _player.gameObject.GetComponentInParent<PlayerWeapons>().PlayerGuns[SlotInWeapons] = null;
                this.transform.parent = null;
                this.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, 1);
                _isPicked = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _pickUpAllowed = true;
            _player = other;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _pickUpAllowed = false;
        }
    }
}
