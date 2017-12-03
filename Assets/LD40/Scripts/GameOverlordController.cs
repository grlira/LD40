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

    public GameObject panelAdoptCat;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (!panelAdoptCat.activeSelf && nextCatTime <= Time.time)
            panelAdoptCat.SetActive(true);

    }

    private void CreateCat()
    {
        // Spawn cat in a randomly position on the house
        var mapCollider2D = mapGameObject.GetComponent<Collider2D>();
        if (mapCollider2D == null)
            mapCollider2D = mapGameObject.GetComponentInChildren<Collider2D>();

        var bounds = mapCollider2D.bounds;
        var vBottomLeft = (mapCollider2D.transform.position + bounds.center) - bounds.extents;
        var vTopRight = (mapCollider2D.transform.position + bounds.center) + bounds.extents;

        bool spawned = false;
        int spawnTries = 0;
        do {
            var pos = new Vector3(Random.Range(vBottomLeft.x, vTopRight.x), Random.Range(vBottomLeft.y, vTopRight.y));
            var ray = new Ray(pos, Vector3.back);

            if(Physics2D.OverlapCircle(pos,0.5f) == null)
            {
                var newCat = Instantiate(catPrefab);
                newCat.transform.position = pos;
                spawned = true;
            }
        }
        while (!spawned && ++spawnTries < 250);

        if(spawnTries == 250)
        {
            Debug.LogError("Failed to find a suitable place for spawning a cat");
            return;
        }

        // Increase cat counter
        catCounter++;
        catCounterText.text = catCounter.ToString();
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
}
