using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 0.5f;
        float distanceTravelled;
        float currTime;
        public GameObject fuelInput;

        void Start() {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
        }

        void Update()
        {
            if (pathCreator != null)
            {
                // 연료없으면 멈춰
                if (GameManager.instance.fuelCnt == 0)
                {
                    print("연료가 없다" + GameManager.instance.fuelCnt);
                    return;
                }
                // 시간 흘러
                currTime += Time.deltaTime;

                // 연료는 10초 하나씩 소모됨
                if (currTime >= 15f)
                {
                    print("연료 하나 소모됨");
                    currTime = 0;
                    GameManager.instance.fuelCnt--;
                }

                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            }
            
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}