using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class video360aux : MonoBehaviour
{

    [SerializeField] private GeneralMenu _gM;
    [SerializeField] private ActionButton _aB;

    public void deployMenuAux()
    {
        this._gM.deployMenu();
    }

    public void activateButton()
    {
        this._aB.activeMenu();
    }


    public void deactivateButton()
    {
        this._aB.deployMenu();
    }
}
