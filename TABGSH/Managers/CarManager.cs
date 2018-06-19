using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace TABGSH
{
    public static class CarManager
    {
        public static IEnumerable<Car> Cars;

        public static void UpdateCarList()
        {
            Cars = UnityEngine.Object.FindObjectsOfType<Car>();
        }

        public static void SpawnCar(int id)
        {
            Transform transform = PlayerManager.GetLocalPlayer().GetComponentInChildren<Hip>().transform;
            CarDataEntry carDataEntry = CarDatabase.Instance.HMEFDENEPFC(id);
            GameObject car = GameObject.Instantiate<GameObject>(carDataEntry.prefab, transform.position + transform.forward * 4f, transform.rotation);

        }
    }
}
