using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class scRandomGenerateCharacter : MonoBehaviour
{
    private scPersonModel person;

    void Start()
    {
        //GetAllSpritePerson();
        //GenerateARandomPerson();
    }

    public void SetPersonModel(scPersonModel personM) {
        person = personM;
        SetAllChildComponents();
    }

    void SetChildSprite(Transform child, Sprite sprite)
    {
        child.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    void SetAllChildComponents()
    {
        foreach (Transform child in transform)
        {
            switch (child.name)
            {
                case "Base":
                    SetChildSprite(child, person.spBase);
                    break;
                case "Ropa":
                    SetChildSprite(child,person.spRopa);
                    break;
                case "Pelo":
                    SetChildSprite(child, person.spPelo);
                    break;
                case "Cara":
                    SetChildSprite(child, person.spCara);
                    break;
                default:
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }



}
