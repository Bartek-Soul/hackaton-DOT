using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class pan : MonoBehaviour
{
    public GameObject sun;
    List<GameObject> planets=new List<GameObject>();
    GameObject[] planetsArr = null;
    public int distance = 10;
    private int planetIndex = -1;
    public TMP_Text txt;
    bool guide = true;
    bool blankTXT=false;
    string[] desc =
    {
        "Sun\n" +
        "Type: Yellow Dwarf\n" +
        "Surface Temperature: 5000C\n" +
        "Gravity: About 27.9 times Earth gravity \n" +
        "Length of day:\n" +
        "Length of year: ",
        "Mercury:\r\n\r\nPlanet Type: Terrestrial\r\nTemperature: Extremely hot during the day (800°F or 427°C) and extremely cold at night (-290°F or -179°C).\r\nGravity: About 38% of Earth's gravity.\r\nNumber of Moons: 0 (No natural moons)\r\nAtmosphere: Thin and composed mainly of oxygen, sodium, and hydrogen.\r\nDay Length: Approximately 59 Earth days (due to its slow rotation).\r\nYear Length: Approximately 88 Earth days.",
        "Venus:\r\n\r\nPlanet Type: Terrestrial\r\nTemperature: Extremely hot, with surface temperatures around 900°F (475°C), making it the hottest planet in the solar system.\r\nGravity: About 91% of Earth's gravity.\r\nNumber of Moons: 0 (No natural moons)\r\nAtmosphere: Thick and mainly composed of carbon dioxide, with clouds of sulfuric acid.\r\nDay Length: Approximately 116 Earth days (slow rotation).\r\nYear Length: Approximately 225 Earth days.",
        "Moon\r\n\r\nPlanet Type: Natural Satellite\r\nTemperature: Varies significantly, with daytime temperatures reaching up to 127°C (260°F) and nighttime temperatures dropping to -173°C (-280°F).\r\nGravity: Only about 1/6th of Earth's gravity.\r\nNumber of Moons: 0 (The Moon is Earth's only natural satellite)\r\nAtmosphere: Virtually no atmosphere, consisting mainly of trace elements like helium, neon, and hydrogen.\r\nDay Length: Approximately 29.5 Earth days (tidally locked, so it has a synchronous rotation).",
        "Earth:\r\n\r\nPlanet Type: Terrestrial\r\nTemperature: Varied, with an average surface temperature of about 59°F (15°C).\r\nGravity: 100% of Earth's gravity.\r\nNumber of Moons: 1 (The Moon)\r\nAtmosphere: Rich in oxygen and nitrogen, supporting life.\r\nDay Length: Approximately 24 hours.\r\nYear Length: Approximately 365.25 days.",
        "Mars:\r\n\r\nPlanet Type: Terrestrial\r\nTemperature: Cold, with average surface temperatures around -80°F (-62°C).\r\nGravity: About 38% of Earth's gravity.\r\nNumber of Moons: 2 (Phobos and Deimos)\r\nAtmosphere: Thin and primarily carbon dioxide, with traces of nitrogen and argon.\r\nDay Length: Approximately 24.6 hours.\r\nYear Length: Approximately 687 Earth days.",
        "Jupiter:\r\n\r\nPlanet Type: Gas Giant\r\nTemperature: Extremely cold in the outer layers and much hotter at the core.\r\nGravity: Over 2.5 times the gravity of Earth.\r\nNumber of Moons: At least 79 known moons, including the four largest (Io, Europa, Ganymede, and Callisto).\r\nAtmosphere: Mostly hydrogen and helium, with bands of clouds and the famous Great Red Spot.\r\nDay Length: Approximately 10 hours.\r\nYear Length: Approximately 12 Earth years.",
        "Saturn:\r\n\r\nPlanet Type: Gas Giant\r\nTemperature: Extremely cold in the outer layers.\r\nGravity: About 1.1 times Earth's gravity.\r\nNumber of Moons: Over 80 known moons, including Titan, the second-largest moon in the solar system.\r\nAtmosphere: Mainly hydrogen and helium, with beautiful ring systems.\r\nDay Length: Approximately 10.7 hours.\r\nYear Length: Approximately 29.5 Earth years.",
        "Uranus:\r\n\r\nPlanet Type: Ice Giant\r\nTemperature: Extremely cold throughout.\r\nGravity: About 0.9 times Earth's gravity.\r\nNumber of Moons: At least 27 known moons.\r\nAtmosphere: Mostly hydrogen and helium, with methane giving it a blue-green hue.\r\nDay Length: Approximately 17.2 hours.\r\nYear Length: Approximately 84 Earth years.",
        "Neptune:\r\n\r\nPlanet Type: Ice Giant\r\nTemperature: Extremely cold throughout.\r\nGravity: About 1.1 times Earth's gravity.\r\nNumber of Moons: At least 14 known moons.\r\nAtmosphere: Mostly hydrogen and helium, with methane, resulting in its deep blue color.\r\nDay Length: Approximately 16.1 hours.\r\nYear Length: Approximately 165 Earth years.",
        "Pluto:\r\n\r\nPlanet Type: Dwarf Planet\r\nTemperature: Extremely cold, with surface temperatures averaging around -375°F (-225°C).\r\nGravity: Very weak, about 0.06% of Earth's gravity.\r\nNumber of Moons: 5 known moons, with the largest being Charon.\r\nAtmosphere: Thin and primarily composed of nitrogen, with traces of methane and carbon monoxide.\r\nDay Length: Approximately 153 hours and 29 minutes.\r\nYear Length: Approximately 248 Earth years."

    };
    void Start()
    {
        planets.Add(sun);
        planets.AddRange(GameObject.FindGameObjectsWithTag("planet"));
        planetsArr = Sorting(planets.ToArray());
        foreach (var item in planetsArr)
        {
            print(item.name);
        }
        print(planetsArr.Length);
    }
    GameObject[] Sorting(GameObject[] unsorted)
    {
        GameObject[] arr = unsorted;
        for (int j = 0; j < arr.Length; j++)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (Vector3.Distance(arr[i].transform.position, sun.transform.position) > Vector3.Distance(arr[i + 1].transform.position, sun.transform.position))
                {
                    var tmp = arr[i];
                    arr[i] = arr[i + 1];
                    arr[i + 1] = tmp;
                }
            }
        }
        return arr;
    }
    // Update is called once per frame
    void Update()
    {
        
        if (planetIndex == -1)
        {
            if (guide)
            {
                txt.text = "Press N to cycle throught planets\n" +
                    "Press T to go to top down view\n" +
                    "Press H to hide text\n" +
                    "Press Escape to exit";
            }
            else
                txt.text = "";
            
            transform.position = Vector3.Lerp(this.transform.position, new Vector3(0, 100, 0), Time.deltaTime);
            Quaternion target = Quaternion.LookRotation(sun.transform.position - this.transform.position);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, target, 3 * Time.deltaTime);
        }
        else
        {
            if (!blankTXT)
            {
                txt.text = desc[planetIndex];

            }
            else
            {
                txt.text = "";
            }

            Vector3 dir = -sun.transform.position + planetsArr[planetIndex].transform.position;
            transform.position = Vector3.Lerp(this.transform.position, planetsArr[planetIndex].transform.position - dir.normalized * distance +planetsArr[planetIndex].transform.localScale, Time.deltaTime);
            Quaternion target = Quaternion.LookRotation(planetsArr[planetIndex].transform.position - this.transform.position);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, target, 3 * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            guide = false;
            planetIndex += 1;
            print(planetIndex);
            if (planetIndex >= planetsArr.Length)
            {
                planetIndex = 0;
            }
        }
        if(Input.GetKeyDown(KeyCode.T)) 
        {
            planetIndex = -1;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            blankTXT = !blankTXT;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
