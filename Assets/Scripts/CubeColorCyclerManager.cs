using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
namespace Sayan.CubeColorCycler
{
    public class CubeColorCyclerManager : MonoBehaviour
    {
        [System.Serializable]
        public class CubeInfo
        {
            public GameObject cube;
            public TMP_Text label;
            [HideInInspector] public Renderer renderer;
        }

        [SerializeField] private List<CubeInfo> cubes = new List<CubeInfo>();
        [SerializeField] private TMP_Text timeText;
        private readonly List<Color> colorCycle = new List<Color> { Color.red, Color.green, Color.blue };
        private float cycleInterval = 3f;
        private float initialDelay = 2f;
        private float timer = 0;
        /// <summary>
        /// Initially Store Cube information
        /// </summary>

        void Start()
        {
            for (int i = 0; i < cubes.Count; i++)
            {
                cubes[i].renderer = cubes[i].cube.GetComponent<Renderer>();
                cubes[i].renderer.material.color = colorCycle[i];
                UpdateLabel(i);
            }
            StartCoroutine(ColorCycleRoutine());
        }
        /// <summary>
        /// Coroutine that updates a timer text and rotates colors at fixed intervals.
        /// Starts with an initial delay.
        /// </summary>
        /// <returns></returns>
        IEnumerator ColorCycleRoutine()
        {
            timeText.text = "" + timer + "s";
            timer += initialDelay;
            yield return new WaitForSeconds(initialDelay); // Initial delay
            timeText.text = "" + timer + "s";
            while (true)
            {
                timer += cycleInterval;
                yield return new WaitForSeconds(cycleInterval); // Repeat every 3s
                timeText.text = "" + timer + "s";
                RotateColors();
            }
        }
        /// <summary>
        /// Colors will be in in cycle mode and used their name also
        /// </summary>
        void RotateColors()
        {
            Color lastColor = colorCycle[colorCycle.Count - 1];
            for (int i = colorCycle.Count - 1; i > 0; i--)
            {
                colorCycle[i] = colorCycle[i - 1];
            }
            colorCycle[0] = lastColor;
            for (int i = 0; i < cubes.Count; i++)
            {
                cubes[i].renderer.material.color = colorCycle[i];
                UpdateLabel(i);
            }
        }
        /// <summary>
        /// Assign color and their respective name
        /// </summary>
        /// <param name="index"></param>
        void UpdateLabel(int index)
        {
            if (cubes[index].label != null)
            {
                Color c = cubes[index].renderer.material.color;
                cubes[index].label.text = ColorToName(c);
            }
        }
        /// <summary>
        /// Fetch color name according color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        string ColorToName(Color color)
        {
            if (color == Color.red) return "Red";
            if (color == Color.green) return "Green";
            if (color == Color.blue) return "Blue";
            return "Unknown";
        }
    }
}
