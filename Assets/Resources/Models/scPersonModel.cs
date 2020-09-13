using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scPersonModel
{
    public int sexoPerson; // 0 hombre | 1 mujer
    public int basePerson;
    public int ropaPerson;
    public int ojosPerson;
    public int peloPerson;
    public Sprite spBase, spCara, spPelo, spRopa;
    public bool esObjetivo;

    public scPersonModel(int sexoPerson, int basePerson, int ojosPerson , int ropaPerson, int peloPerson, Sprite spBase, Sprite spCara, Sprite spPelo, Sprite spRopa)
    {
        this.sexoPerson = sexoPerson;
        this.basePerson = basePerson;
        this.ropaPerson = ropaPerson;
        this.ojosPerson = ojosPerson;
        this.peloPerson = peloPerson;
        this.spBase = spBase;
        this.spCara = spCara;
        this.spPelo = spPelo;
        this.spRopa = spRopa;
        this.esObjetivo = false;
    }

    public string GetSexoString()
    {
        return (sexoPerson == 0) ? "Hombre": "Mujer";
    }

    public override bool Equals(object obj)
    {
        return obj is scPersonModel model &&
               sexoPerson == model.sexoPerson &&
               basePerson == model.basePerson &&
               ropaPerson == model.ropaPerson &&
               ojosPerson == model.ojosPerson &&
               peloPerson == model.peloPerson;
    }
}
