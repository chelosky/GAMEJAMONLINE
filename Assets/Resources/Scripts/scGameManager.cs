using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scGameManager : MonoBehaviour
{
    private Sprite[] spDeath;
    private Sprite[] spInterrogation;
    private Sprite[] spSexo;
    private Sprite[] spBases;
    private Sprite[] spCaras;
    private Sprite[] spPeloMujer;
    private Sprite[] spPeloHombre;
    private Sprite[] spRopaMujer;
    private Sprite[] spRopaHombre;

    private scSoundManager scSoundManager;

    private GameObject prefabPerson;
    private GameObject goIconsObjetivo;

    private GameObject mapPerson;
    private Vector3 minPosMap, maxPosMap;

    private List<scPersonModel> listadoPersonas = new List<scPersonModel>();

    public static scGameManager instance;

    public scPersonModel scObjetivoNivel;

    private int level = 1;
    private int numberPerson = 5;
    private int intervalPerson = 5;

    public int stateGame = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        stateGame = 0;
        scSoundManager = transform.GetComponent<scSoundManager>();
        GetAllSpritePerson();
        GetMapInfo();
        GetIconsObjetivo();
        GetPrefabs();
    }

    private void GetIconsObjetivo()
    {
        goIconsObjetivo = GameObject.FindGameObjectWithTag("IconosUI");
        Debug.Log(goIconsObjetivo.transform.childCount);
    }

    private void DesactivateMsgUI()
    {
        GameObject goUI = GameObject.FindGameObjectWithTag("UIGAMEINFO");
        foreach (Transform child in goUI.transform)
        {
            switch (child.tag)
            {
                case "LoseUI":
                case "TimeOutUI":
                case "WinnerUI":
                    child.gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }

    private void SetIconObjetivo(string valor, Sprite sprite)
    {
        goIconsObjetivo.transform.Find(valor).GetComponent<Image>().sprite = sprite;
    }

    private void DeleteAllPersonActive()
    {
        GameObject[] listPerson = GameObject.FindGameObjectsWithTag("Person");
        foreach(GameObject person in listPerson)
        {
            Destroy(person);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (stateGame)
        {
            case 0:
                DesactivateMsgUI();
                UpdateLevel();
                GenerateNewLevel();
                stateGame = 1;
                break;
            case 1: //playing
                break;
            case 2: // win
                ShowMessage("win");
                PlayASound("win");
                stateGame = 5;
                break;
            case 3: // lose
                ShowMessage("lose");
                PlayASound("lose");
                stateGame = 6;
                break;
            case 4: // timeout
                ShowMessage("timeout");
                PlayASound("lose");
                stateGame = 6;
                break;
            case 5: //wait win
                if (Input.GetMouseButtonDown(0))
                {
                    // LOAD NEW LEVEL
                    stateGame = 7;
                }
                break;
            case 6: //wait lose
                if (Input.GetMouseButtonDown(0))
                {
                    SceneManager.LoadScene("Menu");
                }
                break;
            case 7: // clean game
                level++;
                numberPerson += 5;
                GameObject.FindGameObjectWithTag("UIGAMEINFO").GetComponent<scTimerManager>().RestartCounter();
                DeleteAllPersonActive();
                stateGame = 0;
                break;
            default:
                break;
        }
    }

    public void GenerateNewLevel()
    {
        List<Sprite> spCarasCopy = new List<Sprite>(spCaras);
        List<Sprite> spPeloHombreCopy = new List<Sprite>(spPeloHombre);
        List<Sprite> spPeloMujerCopy = new List<Sprite>(spPeloMujer);
        List<Sprite> spRopaHombreCopy = new List<Sprite>(spRopaHombre);
        List<Sprite> spRopaMujerCopy = new List<Sprite>(spRopaMujer);
        for (int i=0; i < numberPerson; i++)
        {
            int sexInt = scRandomGenerator.GetRandomLinearCongruential(2);
            int ropaInt = (sexInt == 0) ? scRandomGenerator.GetRandomLinearCongruential(spRopaHombreCopy.Count) : scRandomGenerator.GetRandomLinearCongruential(spRopaMujerCopy.Count);
            Sprite spriteRopa = (sexInt == 0) ? spRopaHombreCopy[ropaInt] : spRopaMujerCopy[ropaInt];
            int peloInt = (sexInt == 0) ? scRandomGenerator.GetRandomLinearCongruential(spPeloHombreCopy.Count) : scRandomGenerator.GetRandomLinearCongruential(spPeloMujerCopy.Count);
            Sprite spritePelo = (sexInt == 0) ? spPeloHombreCopy[peloInt] : spPeloMujerCopy[peloInt];
            int baseInt = scRandomGenerator.GetRandomLinearCongruential(spBases.Length);
            Sprite spriteBase = spBases[baseInt];
            int caraInt = scRandomGenerator.GetRandomLinearCongruential(spCarasCopy.Count);
            Sprite spriteCara = spCarasCopy[caraInt];
            scPersonModel person = new scPersonModel(sexInt,
                                                     baseInt,
                                                     caraInt,
                                                     ropaInt,
                                                     peloInt,
                                                     spriteBase,
                                                     spriteCara,
                                                     spritePelo,
                                                     spriteRopa);
            if(i == 0)
            {
                person.esObjetivo = true;
                SetIconObjetivo("iconSexo", spSexo[sexInt]);
                if (level < 4)
                {
                    SetIconObjetivo("iconCara", spCarasCopy[caraInt]);
                    spCarasCopy.RemoveAt(caraInt);
                    if(sexInt == 0)
                    {
                        SetIconObjetivo("iconPelo", spPeloHombreCopy[peloInt]);
                        spPeloHombreCopy.RemoveAt(peloInt);
                        SetIconObjetivo("iconRopa", spRopaHombreCopy[ropaInt]);
                        spRopaHombreCopy.RemoveAt(ropaInt);
                    }
                    else
                    {
                        SetIconObjetivo("iconPelo", spPeloMujerCopy[peloInt]);
                        spPeloMujerCopy.RemoveAt(peloInt);
                        SetIconObjetivo("iconRopa", spRopaMujerCopy[ropaInt]);
                        spRopaMujerCopy.RemoveAt(ropaInt);
                    }
                }
                else if(level < 7)
                {
                    SetIconObjetivo("iconCara", spCarasCopy[caraInt]);
                    spCarasCopy.RemoveAt(caraInt);
                    if (sexInt == 0)
                    {
                        SetIconObjetivo("iconPelo", spPeloHombreCopy[peloInt]);
                        spPeloHombreCopy.RemoveAt(peloInt);
                    }
                    else
                    {
                        SetIconObjetivo("iconPelo", spPeloMujerCopy[peloInt]);
                        spPeloMujerCopy.RemoveAt(peloInt);
                    }
                    SetIconObjetivo("iconRopa", spInterrogation[0]);
                }
                else
                {
                    if (sexInt == 0)
                    {
                        SetIconObjetivo("iconPelo", spPeloHombreCopy[peloInt]);
                        spPeloHombreCopy.RemoveAt(peloInt);
                    }
                    else
                    {
                        SetIconObjetivo("iconPelo", spPeloMujerCopy[peloInt]);
                        spPeloMujerCopy.RemoveAt(peloInt);
                    }
                    SetIconObjetivo("iconRopa", spInterrogation[0]);
                    SetIconObjetivo("iconCara", spInterrogation[0]);
                }

            }
            scObjetivoNivel = person;
            GameObject goPerson = (GameObject)Instantiate(prefabPerson, ReturnRandomPositionMap(), Quaternion.identity);
            goPerson.GetComponent<scRandomGenerateCharacter>().SetPersonModel(person);

        }
    }

    public void UpdateLevel()
    {
        GameObject goUI = GameObject.FindGameObjectWithTag("UIGAMEINFO");
        goUI.transform.Find("LevelText").GetComponent<Text>().text = "NIVEL " + level;
    }

    public void ShowMessage(string text)
    {
        GameObject goUI = GameObject.FindGameObjectWithTag("UIGAMEINFO");
        foreach (Transform child in goUI.transform)
        {
            switch (text)
            {
                case "win":
                    if (child.tag == "WinnerUI") child.gameObject.SetActive(true);
                    break;
                case "lose":
                    if (child.tag == "LoseUI") child.gameObject.SetActive(true);
                    break;
                case "timeout":
                    if (child.tag == "TimeOutUI") child.gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }


    public void GetPrefabs()
    {
        prefabPerson = (GameObject)Resources.Load("Prefabs/Person", typeof(GameObject));
    }

    public void GetMapInfo()
    {
        mapPerson = GameObject.FindGameObjectWithTag("Map");
        minPosMap = mapPerson.transform.Find("PositionBorderLeft").position;
        maxPosMap = mapPerson.transform.Find("PositionBorderRight").position;
    }

    public Vector3 ReturnRandomPositionMap()
    {
        Vector3 vec3TMP = scRandomGenerator.GenerateARandomVector3((int)minPosMap.x, (int)maxPosMap.y, (int)maxPosMap.x, (int)minPosMap.y);
        return vec3TMP;
    }

    private void GetAllSpritePerson()
    {
        spSexo = Resources.LoadAll<Sprite>("Sprites/spShSignos");
        spBases = Resources.LoadAll<Sprite>("Sprites/spShBases");
        spCaras = Resources.LoadAll<Sprite>("Sprites/spShOjos");
        spPeloHombre = Resources.LoadAll<Sprite>("Sprites/spShPeloHombre");
        spPeloMujer = Resources.LoadAll<Sprite>("Sprites/spShPeloMujer");
        spRopaHombre = Resources.LoadAll<Sprite>("Sprites/spShRopaHombre");
        spRopaMujer = Resources.LoadAll<Sprite>("Sprites/spShRopaMujer");
        spInterrogation = Resources.LoadAll<Sprite>("Sprites/spShInterrogation");
        spDeath = Resources.LoadAll<Sprite>("Sprites/spShDeath");
    }

    public void PlayASound(string name)
    {
        scSoundManager.PlaySound(name);
    }

    public Sprite GetDeathAnimation()
    {
        return spDeath[0];
    }
}
