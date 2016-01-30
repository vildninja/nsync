using UnityEngine;
using System.Collections;

namespace DailyRitual
{
    public class Human : MonoBehaviour
    {
        private HFTInput input;

        private int horizontal;
        private int vertical;

        private Vector3 lastAcceleration;
        private float crazyOmeter;

        // Use this for initialization
        private void Start()
        {
            input = GetComponent<HFTInput>();

            input.SpecifyButtonNameToButtonIndex("1", 2);
            input.SpecifyButtonNameToButtonIndex("2", 3);
            input.SpecifyButtonNameToButtonIndex("3", 4);
            input.SpecifyButtonNameToButtonIndex("4", 5);
            input.SpecifyButtonNameToButtonIndex("5", 6);
            input.SpecifyButtonNameToButtonIndex("6", 7);
            input.SpecifyButtonNameToButtonIndex("7", 8);
            input.SpecifyButtonNameToButtonIndex("8", 9);
            input.SpecifyButtonNameToButtonIndex("9", 10);

            var team = FindObjectOfType<TeamManager>().JoinTeam(this);
            GetComponent<HFTGamepad>().Color = team.color;
        }

        // Update is called once per frame
        private void Update()
        {
            crazyOmeter = crazyOmeter*(1 - Time.deltaTime * 10) +
                          Vector3.Distance(lastAcceleration, input.acceleration)*Time.deltaTime * 10;
            transform.rotation = input.gyro.attitude;
        }

        public bool IsPressing(string button)
        {
            return input.GetButton(button);
        }

        public bool IsPartying()
        {
            return crazyOmeter > 1;
        }

        public bool IsAiming(Vector3 direction)
        {
            return Vector3.Distance(transform.up, direction) < 0.6f;
        }
    }
}