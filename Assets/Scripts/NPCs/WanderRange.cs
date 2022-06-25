using UnityEngine;

public class WanderRange : MonoBehaviour {

    public Vector3 WanderDestination { get; private set; }
    
    [SerializeField] private float idleTimeMin;
    [SerializeField] private float idleTimeMax;

    private PolygonCollider2D range;

    private float timeSinceLastWander;
    private float nextWanderTime;

    private void Awake() {
        range = GetComponent<PolygonCollider2D>();
        WanderDestination = transform.position;
        timeSinceLastWander = 0;
        nextWanderTime = RandomWanderTime();
    }

    private void Update() {
        timeSinceLastWander += Time.deltaTime;

        if (timeSinceLastWander >= nextWanderTime) {
            timeSinceLastWander = 0;
            WanderDestination = RandomPointInRange();
        }
    }
    
    private float RandomWanderTime() {
        return Random.Range(idleTimeMin, idleTimeMax);
    }
    
    private Vector2 RandomPointInRange() {
        var min = range.bounds.min;
        var max = range.bounds.max;

        while (true) {
            var point = new Vector2(
                Random.Range(min.x, max.x),
                Random.Range(min.y, max.y)
            );
            
            if (range.OverlapPoint(point)) {
                return point;
            }
        }
    }
}