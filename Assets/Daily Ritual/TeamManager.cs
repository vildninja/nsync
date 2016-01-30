using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

namespace DailyRitual
{
    [System.Serializable]
    public class Team
    {
        public readonly List<Human> members = new List<Human>();
        public Color color;
        public Text task;
        public Text scoreText;
        public Image scoreBar;
        public float score;
    }

    public class TeamManager : MonoBehaviour
    {
        public const int Sleep = 0;
        public const int Coffe = 1;
        public const int Work = 2;
        public const int Party = 3;

        public int roundTime = 15;
        private int roundCount;
        public Team[] teams;

        private string currentWorkTask = "";

        // Use this for initialization
        private IEnumerator Start()
        {
            while (true)
            {
                roundCount++;
                for (int i = 0; i < teams.Length; i++)
                {
                    var team = teams[i];

                    // clean up disconnected players
                    for (int j = 0; j < team.members.Count; j++)
                    {
                        if (team.members[j] == null)
                        {
                            team.members.RemoveAt(j);
                            j--;
                        }
                    }

                    switch ((roundCount + i) % 4)
                    {
                        case Sleep:
                            team.task.text = "Sleep";
                            break;
                        case Coffe:
                            team.task.text = "Drink Coffe";
                            break;
                        case Work:
                            team.task.text = "Work! Press: " + currentWorkTask;
                            break;
                        case Party:
                            team.task.text = "PARTY HARD!";
                            break;
                    }
                }

                for (int rt = 0; rt < roundTime * 3; rt++)
                {
                    yield return new WaitForSeconds(0.333f);

                    for (int i = 0; i < teams.Length; i++)
                    {
                        var team = teams[(roundCount + i) % teams.Length];

                        if (team.members.Count == 0)
                        {
                            continue;
                        }

                        int participating = 0;

                        switch ((roundCount + i) % 4)
                        {
                            case Sleep:
                                participating = team.members.Count(m => !m.IsPartying() &&
                                    !m.IsAiming(Vector3.down) && !m.IsAiming(Vector3.up));
                                break;
                            case Coffe:
                                participating = team.members.Count(m => m.IsAiming(Vector3.down));
                                break;
                            case Work:

                                participating = team.members.Count(m => m.IsPressing(currentWorkTask));

                                if (rt%9 == 0)
                                {
                                    currentWorkTask = Random.Range(0, 10).ToString();
                                    team.task.text = "Work! Press: " + currentWorkTask;
                                }
                                break;
                            case Party:
                                participating = team.members.Count(m => m.IsPartying());
                                break;
                        }

                        team.score += participating/(float) team.members.Count;

                        team.scoreText.text = "Score: " + Mathf.FloorToInt(team.score);
                    }
                }
            }
        }

        public Team JoinTeam(Human human)
        {
            Team team = teams[0];
            for (int i = 0; i < teams.Length; i++)
            {
                if (teams[i].members.Count < team.members.Count)
                {
                    team = teams[i];
                }
            }

            team.members.Add(human);
            return team;
        }
    }
}
