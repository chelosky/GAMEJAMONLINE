using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scGameManager : MonoBehaviour
{
    private Sprite[] spBases;
    private Sprite[] spCaras;
    private Sprite[] spPeloMujer;
    private Sprite[] spPeloHombre;
    private Sprite[] spRopaMujer;
    private Sprite[] spRopaHombre;

    private GameObject prefabPerson;

    private GameObject mapPerson;
    private Vector3 minPosMap, maxPosMap;

    private List<scPersonModel> listadoPersonas = new List<scPersonModel>();

    public static scGameManager instance;

    private int level = 1;
    private int numberPerson = 5;
    private int intervalPerson = 5;

    private int stateGame = 0; // 0 --> iniciando | 1 --> jugando | 2 --> lose | 3 --> win

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
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
        GetAllSpritePerson();
        GetMapInfo();
        GetPrefabs();
    }

    // Update is called once per frame
    void Update()
    {
        switch (stateGame)
        {
            case 0:
                GenerateNewLevel();
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
    }

    public void GenerateNewLevel()
    {
        for(int i=0; i < numberPerson; i++)
        {
            int sexInt = scRandomGenerator.GetRandomLinearCongruential(2);
            int ropaInt = (sexInt == 0) ? scRandomGenerator.GetRandomLinearCongruential(spRopaHombre.Length) : scRandomGenerator.GetRandomLinearCongruential(spRopaMujer.Length);
            Sprite spriteRopa = (sexInt == 0) ? spRopaHombre[ropaInt] : spRopaMujer[ropaInt];
            int peloInt = (sexInt == 0) ? scRandomGenerator.GetRandomLinearCongruential(spPeloHombre.Length) : scRandomGenerator.GetRandomLinearCongruential(spPeloMujer.Length);
            Sprite spritePelo = (sexInt == 0) ? spPeloHombre[peloInt] : spPeloMujer[peloInt];
            int baseInt = scRandomGenerator.GetRandomLinearCongruential(spBases.Length);
            Sprite spriteBase = spBases[baseInt];
            int caraInt = scRandomGenerator.GetRandomLinearCongruential(spCaras.Length);
            Sprite spriteCara = spCaras[caraInt];
            scPersonModel person = new scPersonModel(sexInt,
                                                     baseInt,
                                                     caraInt,
                                                     ropaInt,
                                                     peloInt,
                                                     spriteBase,
                                                     spriteCara,
                                                     spritePelo,
                                                     spriteRopa);
            GameObject goPerson = (GameObject)Instantiate(prefabPerson, ReturnRandomPositionMap(), Quaternion.identity);
            goPerson.GetComponent<scRandomGenerateCharacter>().SetPersonModel(person);

        }
        stateGame = 1;
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
        spBases = Resources.LoadAll<Sprite>("Sprites/spShBases");
        spCaras = Resources.LoadAll<Sprite>("Sprites/spShOjos");
        spPeloHombre = Resources.LoadAll<Sprite>("Sprites/spShPeloHombre");
        spPeloMujer = Resources.LoadAll<Sprite>("Sprites/spShPeloMujer");
        spRopaHombre = Resources.LoadAll<Sprite>("Sprites/spShRopaHombre");
        spRopaMujer = Resources.LoadAll<Sprite>("Sprites/spShRopaMujer");
    }
}
