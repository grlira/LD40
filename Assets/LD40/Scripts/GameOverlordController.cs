using UnityEngine;
using System.Collections;

public class GameOverlordController : MonoBehaviour
{
    const float MIN_CAT_CREATION_INTERVAL = 5;
    const float MAX_CAT_CREATION_INTERVAL = 15;

    public static GameOverlordController instance;
    public GameObject catPrefab;

    public ItemBase SelectedItem { get; private set; }

    private float nextCatTime;

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
        if (nextCatTime <= Time.time)
        {
            var newCat = Instantiate(catPrefab);
            // TODO place cats in random position

            nextCatTime = generateNextCatTime();
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
}
