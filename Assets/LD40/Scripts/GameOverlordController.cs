using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverlordController : MonoBehaviour
{
    const float MIN_CAT_CREATION_INTERVAL = 5;
    const float MAX_CAT_CREATION_INTERVAL = 15;

    public static GameOverlordController instance;
    public GameObject catPrefab;

    public ItemBase SelectedItem { get; private set; }

    private float nextCatTime;

    private int catCounter;
    public Text catCounterText;

    private int prevCatsRequiredForParty = 0;
    private int catsRequiredForParty = 1;
    public Text requiredCounterText;
    public Button teaPartyButton;

    public GameObject panelAdoptCat;

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
        if (!panelAdoptCat.activeSelf && nextCatTime <= Time.time)
            panelAdoptCat.SetActive(true);

    }

    private void CreateCat()
    {
        var newCat = Instantiate(catPrefab);
        // TODO place cats in random position

        catCounter++;
        catCounterText.text = catCounter.ToString();
        if (catCounter >= catsRequiredForParty)
        {
            teaPartyButton.gameObject.SetActive(true);
        }
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
        SelectedItem = item;
    }

    public void ClearSelectedItem(ItemBase item=null)
    {
        if (item != null && SelectedItem != item)
            return;

        SelectedItem = null;
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

    public void StartTeaParty()
    {
        int temp = catsRequiredForParty;
        catsRequiredForParty = prevCatsRequiredForParty + catsRequiredForParty;
        prevCatsRequiredForParty = temp;
        requiredCounterText.text = catsRequiredForParty.ToString();
        teaPartyButton.gameObject.SetActive(false);
    }
}
