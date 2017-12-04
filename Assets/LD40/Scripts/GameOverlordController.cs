using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameOverlordController : MonoBehaviour
{
    const float MIN_CAT_CREATION_INTERVAL = 5;
    const float MAX_CAT_CREATION_INTERVAL = 15;

    public static GameOverlordController instance;
    public GameObject catPrefab;
    public GameObject visitorPrefab;

    public ItemBase SelectedItem { get; private set; }

    private float nextCatTime;

    private int catCounter;
    public Text catCounterText;

    private int prestigeCounter;
    public Text prestigeCounterText;

    private int prevCatsRequiredForParty = 1;
    private int catsRequiredForParty = 1;
    public Text requiredCounterText;
    public Button kittyPartyButton;

    private float partyRemainingTime;

    public GameObject clockPanel;
    public Text clockText;

    public GameObject panelAdoptCat;

    private List<GameObject> visitors = new List<GameObject>();
    private bool isPartyActive = false;
    

    public GameObject mapGameObject;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        nextCatTime = generateNextCatTime();
        requiredCounterText.text = catsRequiredForParty.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!panelAdoptCat.activeSelf && nextCatTime <= Time.time && partyRemainingTime <= 0)
        {
            panelAdoptCat.SetActive(true);
        }
            
        if (partyRemainingTime > 0)
        {
            partyRemainingTime -= Time.deltaTime;
            clockText.text = Mathf.FloorToInt(partyRemainingTime).ToString();
        } else if(isPartyActive)
        {
            isPartyActive = false;
            clockPanel.gameObject.SetActive(false);
            AddPrestige(visitors.Count);
            foreach(var visitor in visitors)
            {
                Destroy(visitor);
            }
        }

        // Get selected item
        var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;

        Collider2D collider;
        if((collider = Physics2D.OverlapCircle(mouse,0.25f,LayerMask.GetMask("Items"))) != null)
        {
            var item = collider.GetComponent<ItemBase>();

            if(item != null)
            {
                SetSelectedItem(item);
            }
            else
            {
                SetSelectedItem(null);
            }
        }
        else
        {
            SetSelectedItem(null);
        }
    }

    void AddPrestige(int amount)
    {
        prestigeCounter += amount;
        prestigeCounterText.text = prestigeCounter.ToString();
    }

    public Vector2 GenerateRandomSpawnPoint()
    {
        var mapCollider2D = mapGameObject.GetComponent<Collider2D>();
        if (mapCollider2D == null)
            mapCollider2D = mapGameObject.GetComponentInChildren<Collider2D>();

        var bounds = mapCollider2D.bounds;
        var vBottomLeft = bounds.center - bounds.extents + new Vector3(1,1,0);
        var vTopRight = bounds.center + bounds.extents - new Vector3(1, 1, 0);


        int spawnTries = 0;
        do
        {
            var pos = new Vector3(Random.Range(vBottomLeft.x, vTopRight.x), Random.Range(vBottomLeft.y, vTopRight.y));

            if (Physics2D.OverlapCircle(pos, 0.75f) == null)
            {
                return pos;
            }
        }
        while (++spawnTries < 250);
        throw new System.Exception("Failed to generate random position");
    }

    private void CreateCat()
    {
        
        try
        {
            Vector2 pos = GenerateRandomSpawnPoint();
            var newCat = Instantiate(catPrefab);
            newCat.transform.position = pos;
            
            // Increase cat counter
            catCounter++;
            catCounterText.text = catCounter.ToString();
            if (catCounter >= catsRequiredForParty)
            {
                kittyPartyButton.gameObject.SetActive(true);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.ToString());
        }
    }

    private void CreateVisitor()
    {
        try
        {
            Vector2 pos = GenerateRandomSpawnPoint();
            var newVisitor = Instantiate(visitorPrefab);
            visitors.Add(newVisitor);
            newVisitor.transform.position = pos;
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.ToString());
        }
    }

    public void DestroyVisitor(GameObject visitor)
    {
        visitors.Remove(visitor);
        Destroy(visitor);
        AddPrestige(-1);
    }

    private float generateNextCatTime()
    {
        return Time.time + Random.Range(
            MIN_CAT_CREATION_INTERVAL,
            MAX_CAT_CREATION_INTERVAL
        );
    }

    public void SetSelectedItem(ItemBase item)
    {
        if(SelectedItem != item)
        {
            if (SelectedItem != null)
                SelectedItem.OnItemDeselected();

            SelectedItem = item;

            if(SelectedItem != null)
                SelectedItem.OnItemSelected();
        }
        
    }


    public void AdoptCat()
    {
        panelAdoptCat.SetActive(false);
        
        CreateCat();

        nextCatTime = generateNextCatTime();
    }

    public void DontAdoptCat()
    {
        panelAdoptCat.SetActive(false);
        nextCatTime = generateNextCatTime();
    }

    public void StartKittyParty()
    {
        for (int i = 0; i < catsRequiredForParty; i++)
        {
            CreateVisitor();
        }

        int temp = catsRequiredForParty;
        catsRequiredForParty = prevCatsRequiredForParty + catsRequiredForParty;
        prevCatsRequiredForParty = temp;
        requiredCounterText.text = catsRequiredForParty.ToString();
        kittyPartyButton.gameObject.SetActive(false);
        clockPanel.gameObject.SetActive(true);

        partyRemainingTime = 30;
        clockText.text = Mathf.FloorToInt(partyRemainingTime).ToString();
        isPartyActive = true;
    }
}
