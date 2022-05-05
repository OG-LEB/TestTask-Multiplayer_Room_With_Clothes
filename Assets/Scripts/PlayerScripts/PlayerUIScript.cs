using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerUIScript : MonoBehaviour
{
    [SerializeField] PhotonView _PhotonView;

    [Header("UI Elements")]
    [SerializeField] Text JacketButton;
    [SerializeField] Text PansButton;
    [SerializeField] Text BootsButton;

    private ClothesManager target;

    private void Start()
    {
        if (!_PhotonView.IsMine)
        {
            Destroy(gameObject);
        }
    }
    public void SetTarget(ClothesManager _target) 
    {
        if (_target == null) 
        {
            Debug.LogError("Missing ClothesManager target for PlayerUIScript.SetTarget()");
            return;
        }
        target = _target;
    }

    public void PutOnJacket()
    {
        if (_PhotonView.IsMine)
        {
            target.PutOnJasket();
        }
    }
    public void PutOffJacket()
    {
        if (_PhotonView.IsMine)
        {
            target.PutOffJasket();
        }
    }
    
    public void PuOnPans()
    {
        if (_PhotonView.IsMine)
        {
            target.PutOnPans();
        }
    }
    public void PutOffPans()
    {
        if (_PhotonView.IsMine)
        {
            target.PutOffPans();
        }
    } 

    public void PutOnBoots()
    {
        if (_PhotonView.IsMine)
        {
            target.PutOnBoots();
        }
    }
    public void PutOffBoots() 
    {
        if (_PhotonView.IsMine)
        {
            target.PutOffBoots();
        }
    }
}
