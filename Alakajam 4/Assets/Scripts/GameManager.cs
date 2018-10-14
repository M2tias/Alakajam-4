using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField]
    List<GameObject> levelPrefabs = new List<GameObject>();
    [SerializeField]
    private int currentLevelNum = 0;
    private GameObject currentLevel;

    [SerializeField]
    private Angel player;
    [SerializeField]
    private PlayerHealth health;
    [SerializeField]
    private Position playerPosition;

    [SerializeField]
    private BoolFlag levelFinished;
    [SerializeField]
    private Text StartGamePointer;
    [SerializeField]
    private Text ExitPointer;
    [SerializeField]
    private GameObject Menu;
    [SerializeField]
    private BoolFlag inMenu;
    private bool newGameSelected = true;
    [SerializeField]
    private BoolFlag isDead;

    [SerializeField]
    private GameObject StatusPanel;
    [SerializeField]
    private Text LevelFinished;
    [SerializeField]
    private Text GameOver;
    [SerializeField]
    private Text YouAreDead;

    //this is stupid duct tape code
    [SerializeField]
    private List<Image> Hearts; //this shouldn't be here. hearts' parent should handle it somehow
    [SerializeField]
    private Text Level; //should probably use scriptableObject
    [SerializeField]
    private List<Material> levelMaterials;
    [SerializeField]
    private MeshRenderer planeRenderer;


    // Use this for initialization
    void Start () {
        inMenu.Value = true;
        isDead.Value = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (inMenu.Value)
        {
            Menu.SetActive(true);
            float inputY = Input.GetAxis("Vertical");

            //init default values
            health.CurrentHealth = health.MaxHealth;
            player.transform.localPosition = playerPosition.Default;
            Hearts.ForEach(x => x.gameObject.SetActive(true));

            if (currentLevel != null)
            {
                Destroy(currentLevel);
                currentLevelNum = 0;
            }

            if (newGameSelected)
            {
                if (inputY < 0) newGameSelected = false;

                if (Input.GetButton("Fire1"))
                {
                    inMenu.Value = false;
                }

                StartGamePointer.enabled = true;
                ExitPointer.enabled = false;
            }
            else
            {
                if (inputY > 0) newGameSelected = true;

                if (Input.GetButton("Fire1"))
                {
                    Application.Quit();
                }

                StartGamePointer.enabled = false;
                ExitPointer.enabled = true;
            }
        }
        else
        {
            Menu.SetActive(false);
            if (currentLevel == null)
            {
                Debug.Log("First level!");
                levelFinished.Value = false;
                GameObject nextLevelPrefab = levelPrefabs[currentLevelNum];
                GameObject nextLevel = Instantiate(nextLevelPrefab);
                currentLevel = nextLevel;
                nextLevel.SetActive(true);
                Level.text = "Level 1";
                planeRenderer.material = levelMaterials[currentLevelNum];
            }

            if (levelFinished.Value)
            {
                Debug.Log("Next level!");
                currentLevelNum++;
                levelFinished.Value = false;
                GameObject nextLevelPrefab = levelPrefabs[currentLevelNum];
                currentLevel.SetActive(false);
                Destroy(currentLevel);
                GameObject nextLevel = Instantiate(nextLevelPrefab);
                currentLevel = nextLevel;
                nextLevel.SetActive(true);
                Level.text = "Level "+(currentLevelNum+1);
                planeRenderer.material = levelMaterials[currentLevelNum];

                health.CurrentHealth = health.MaxHealth;
                player.transform.localPosition = playerPosition.Default;
                Hearts.ForEach(x => x.gameObject.SetActive(true));
            }

            if(isDead.Value)
            {
                player.gameObject.SetActive(false);
                YouAreDead.gameObject.SetActive(true);
                StatusPanel.gameObject.SetActive(true);
                StartCoroutine("waitAndResurrect");
            }
        }
	}

    IEnumerator waitAndResurrect()
    {
        yield return new WaitForSeconds(2f);

        //not like this...
        levelFinished.Value = false;
        GameObject nextLevelPrefab = levelPrefabs[currentLevelNum];
        currentLevel.SetActive(false);
        Destroy(currentLevel);
        GameObject nextLevel = Instantiate(nextLevelPrefab);
        currentLevel = nextLevel;
        nextLevel.SetActive(true);
        Level.text = "Level " + (currentLevelNum + 1);
        planeRenderer.material = levelMaterials[currentLevelNum];
        player.gameObject.SetActive(true);
        YouAreDead.gameObject.SetActive(false);
        StatusPanel.gameObject.SetActive(false);

        health.CurrentHealth = health.MaxHealth;
        player.transform.localPosition = playerPosition.Default;
        Hearts.ForEach(x => x.gameObject.SetActive(true));
        isDead.Value = false;
        yield return new WaitForSeconds(0);
    }
}
