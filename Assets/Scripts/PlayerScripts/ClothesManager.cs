using UnityEngine;
using Photon.Pun;

public class ClothesManager : MonoBehaviour, IPunObservable
{
    [SerializeField] PlayerUIScript _PlayerUI;

    [Header("Clothes")]
    [SerializeField] GameObject Jacket;
    [SerializeField] GameObject Pans;
    [SerializeField] GameObject Boots;

    [Header("Body")]
    [SerializeField] GameObject naked_Jacket;
    [SerializeField] GameObject naked_Pans;
    [SerializeField] GameObject naked_Boots;

    bool JacketOn;
    bool PansOn;
    bool BootsOn;

    private void Start()
    {
        _PlayerUI.SetTarget(this);

        PutOnJasket();
        PutOnPans();
        PutOnBoots();
    }
    //public bool GetJacketOnState() 
    //{
    //    return JacketOn;
    //}
    //public bool GetPansOnState()
    //{
    //    return PansOn;
    //}
    //public bool GetBootsOnState()
    //{
    //    return BootsOn;
    //}

    public void PutOnJasket() 
    {
        naked_Jacket.SetActive(false);
        Jacket.SetActive(true);
        JacketOn = true;
    }
    public void PutOffJasket()
    {
        naked_Jacket.SetActive(true);
        Jacket.SetActive(false);
        JacketOn = false;
    }
    public void PutOnPans()
    {
        naked_Pans.SetActive(false);
        Pans.SetActive(true);
        PansOn = true;
    }
    public void PutOffPans()
    {
        naked_Pans.SetActive(true);
        Pans.SetActive(false);
        PansOn = false;
    }
    public void PutOnBoots()
    {
        naked_Boots.SetActive(false);
        Boots.SetActive(true);
        BootsOn = true;
    }
    public void PutOffBoots()
    {
        naked_Boots.SetActive(true);
        Boots.SetActive(false);
        BootsOn = false;
    }
    private void GetClothesInfo()
    {
        if (JacketOn)
        {
            naked_Jacket.SetActive(false);
            Jacket.SetActive(true);
        }
        if (!JacketOn)
        {
            naked_Jacket.SetActive(true);
            Jacket.SetActive(false);
        }
        if (PansOn)
        {
            naked_Pans.SetActive(false);
            Pans.SetActive(true);
        }
        if (!PansOn)
        {
            naked_Pans.SetActive(true);
            Pans.SetActive(false);
        }
        if (BootsOn)
        {
            naked_Boots.SetActive(false);
            Boots.SetActive(true);
        }
        if (!BootsOn)
        {
            naked_Boots.SetActive(true);
            Boots.SetActive(false);
        }
    }

    private void Update()
    {
        GetClothesInfo();
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //Отправляем
            stream.SendNext(JacketOn);
            stream.SendNext(PansOn);
            stream.SendNext(BootsOn);
        }
        else
        {
            //Получаем
            JacketOn = (bool)stream.ReceiveNext();
            PansOn = (bool)stream.ReceiveNext();
            BootsOn = (bool)stream.ReceiveNext();
        }
    }
}
