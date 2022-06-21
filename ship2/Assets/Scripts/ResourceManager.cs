using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ResourceManager : MonoBehaviour
{

	// USING DG-TWEENING LIB HERE

	public static ResourceManager instance;
	public GameObject Ship;
	[Header("Wood References")]

	[SerializeField] Text woodUIText;
	[SerializeField] GameObject animatedWoodPrefab;
	Queue<GameObject> woodsQueue = new Queue<GameObject>();


	[Header("Gold References")]
	//References for coin
	[Header("UI references")]
	public Text coinUIText;
	[SerializeField] GameObject animatedCoinPrefab;
	[SerializeField] Transform target;

	[Space]
	[Header("Available coins : (coins to pool)")]
	[SerializeField] int maxResource;
	Queue<GameObject> coinsQueue = new Queue<GameObject>();


	[Space]
	[Header("Animation settings")]
	[SerializeField] [Range(0.2f, 0.4f)] float minAnimDuration;
	[SerializeField] [Range(0.4f,0.6f)] float maxAnimDuration;

	[SerializeField] Ease easeType;
	[SerializeField] float spread;

	Vector3 targetPosition;

	private int _c = 0;

	public int Coins
	{
		get { return _c; }
		set
		{
			_c = value;
			//update UI text whenever "Coins" variable is changed
			coinUIText.text = Coins.ToString();
		}
	}

	private int _w = 0;

	public int Woods
	{
		get { return _w; }
		set
		{
			_w = value;
			//update UI text whenever "Coins" variable is changed
			woodUIText.text = Woods.ToString();

		}
	}
	
	void Awake()
	{
		instance = this;

		//prepare pool
		PrepareResources();
	}
    private void Update()
    {
		targetPosition = Ship.transform.position;
	}
	
	void PrepareResources()
	{
		// INSTANTIATE THEM IN THE PARENT 
		GameObject coin;
		GameObject wood;
		for (int i = 0; i < maxResource; i++)
		{

			coin = Instantiate(animatedCoinPrefab);
			coin.transform.parent = transform;
			coin.SetActive(false);
			coinsQueue.Enqueue(coin);

			wood = Instantiate(animatedWoodPrefab);
			wood.transform.parent = transform;
			wood.SetActive(false);
			woodsQueue.Enqueue(wood);
		}
	}

	void Animate(Vector3 collectedResourcePosition, int coinAmount,int woodAmount)
	{

		for (int i = 0; i < coinAmount; i++)
		{
			//check if there's coins in the pool
			if (coinsQueue.Count > 0)
			{
				//extract a coin from the pool
				GameObject coin = coinsQueue.Dequeue();
				coin.SetActive(true);


				//move coin to the collected coin pos
				coin.transform.position = collectedResourcePosition + new Vector3(Random.Range(-spread, spread), 0f, 0f);


				//animate coin to target position
				float duration = Random.Range(minAnimDuration, maxAnimDuration);
				coin.transform.LookAt(Camera.main.transform);
				coin.transform.DOMove(targetPosition, duration)
				
				.SetEase(easeType)
				.OnComplete(() => {
					//executes whenever coin reach target position
					coin.SetActive(false);
					coinsQueue.Enqueue(coin);

					Coins++;
				});
			}
		}
		for (int i = 0; i < woodAmount; i++)
		{
			//check if there's coins in the pool
			if (woodsQueue.Count > 0)
			{
				//extract a coin from the pool
				GameObject wood = woodsQueue.Dequeue();
				wood.SetActive(true);


				//move coin to the collected coin pos
				wood.transform.position = collectedResourcePosition + new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), Random.Range(-spread, spread));


				//animate coin to target position
				float duration = Random.Range(minAnimDuration, maxAnimDuration);
				wood.transform.LookAt(Camera.main.transform);
				wood.transform.DOMove(targetPosition, duration)

				.SetEase(easeType)
				.OnComplete(() => {
					//executes whenever coin reach target position
					wood.SetActive(false);
					woodsQueue.Enqueue(wood);

					Woods++;
				});
			}
		}
	}

	public void AddResources(Vector3 collectedResourcePosition, int coinAmount, int woodAmount)
	{
		Animate(collectedResourcePosition, coinAmount, woodAmount);
	}

}
