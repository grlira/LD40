﻿using UnityEngine;
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
        var newCat = Instantiate(catPrefab);
        // TODO place cats in random position

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
