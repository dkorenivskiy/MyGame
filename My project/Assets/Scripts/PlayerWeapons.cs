using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    private Animator _playerAnimator;

    [SerializeField]
    private GameObject PlayerArms;
    [SerializeField]
    private GameObject PlayerBody;
    [SerializeField]
    public GameObject[] PlayerGuns = new GameObject[3];

    [SerializeField]
    private int _currentWeapon;
    private int _maxWeapon = 0;

    private void Start()
    {
        _playerAnimator = this.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        GetPlayerWeapons();
        ScrollingWeapons();

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (_playerAnimator.GetBool("IsArmed") == true)
            {
                _playerAnimator.SetBool("IsArmed", false);
            }
            else
            {
                _playerAnimator.SetBool("IsArmed", true);
            }
        }

        if (_playerAnimator.GetBool("IsArmed") == true)
        {
            for (var i = 0; i < _maxWeapon + 1; i++)
            {
                if (i == _currentWeapon)
                {
                    if (PlayerGuns[i] != null)
                    {
                        PlayerGuns[i].SetActive(true);
                    }
                }
                else
                {
                    if (PlayerGuns[i] != null && PlayerGuns[i].tag != "Fists")
                    {
                        PlayerGuns[i].SetActive(false);
                    }
                }
            }

            TurnAllAnimationsOff();

            if (PlayerGuns[_currentWeapon].CompareTag("Fists"))
            {
                _playerAnimator.SetBool("IsFist", true);
            }

            if (PlayerGuns[_currentWeapon].CompareTag("MeleeWeapon"))
            {
                _playerAnimator.SetBool("IsMelee", true);
            }

            if (PlayerGuns[_currentWeapon].CompareTag("LightWeapon"))
            {
                _playerAnimator.SetBool("IsLight", true);
            }

            if (PlayerGuns[_currentWeapon].CompareTag("HeavyWeaponShotgun") || PlayerGuns[_currentWeapon].CompareTag("HeavyWeaponMachine"))
            {
                _playerAnimator.SetBool("IsHeavy", true);
            }
        }
        else
        {
            TurnAllAnimationsOff();
        }
    }

    private void ScrollingWeapons()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (_currentWeapon == 0 && PlayerGuns[1] == null)
            {
                _currentWeapon++;
            }

            _currentWeapon++;

            if (_currentWeapon > _maxWeapon)
            {
                _currentWeapon = 0;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (_currentWeapon == 2 && PlayerGuns[1] == null)
            {
                _currentWeapon--;
            }

            _currentWeapon--;

            if (_currentWeapon == -1)
            {
                _currentWeapon = _maxWeapon;
            }
        }
    }

    private void GetPlayerWeapons()
    {
        for (var i = 0; i < PlayerBody.transform.childCount; i++)
        {
            if (PlayerBody.transform.GetChild(i).CompareTag("MeleeWeapon"))
            {
                PlayerGuns[0] = PlayerBody.transform.GetChild(i).gameObject;
                continue;
            }

            if (PlayerBody.transform.GetChild(i).CompareTag("LightWeapon"))
            {
                PlayerGuns[1] = PlayerBody.transform.GetChild(i).gameObject;
                
                if (_maxWeapon != 2)
                {
                    _maxWeapon = 1;
                }

                continue;
            }

            if (PlayerBody.transform.GetChild(i).CompareTag("HeavyWeaponShotgun") || PlayerBody.transform.GetChild(i).CompareTag("HeavyWeaponMachine"))
            {
                PlayerGuns[2] = PlayerBody.transform.GetChild(i).gameObject;
                _maxWeapon = 2;
                continue;
            }
        }

        if (PlayerGuns[0] == null)
        {
            PlayerGuns[0] = PlayerArms;
        }

        if (PlayerGuns[_currentWeapon] == null)
        {
            _currentWeapon = 0;
            _maxWeapon = 0;
        }
    }

    private void TurnAllAnimationsOff()
    {
        _playerAnimator.SetBool("IsHeavy", false);
        _playerAnimator.SetBool("IsLight", false);
        _playerAnimator.SetBool("IsMelee", false);
        _playerAnimator.SetBool("IsFist", false);
    }
}