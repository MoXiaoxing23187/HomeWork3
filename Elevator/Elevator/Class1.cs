using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator
{
    public class TestElevator 
    {

        public string UP_MODE = "1";
        public string DOWN_MODE = "-1";
        public string WAIT_MODE = "0";
        public int MAX_SIZE = 10;
        public int MAX_WEIGHT = 800;

        public int customerNum { get; set; }
        public int weight { get; set; }
        public string mode { get; set; }
        public string waitFloor { get; set; }
        public string targetFloor { get; set; }

        public string Working(List<int> list, TestElevator elevator,List<int> storeList)
        {
            if (int.Parse(elevator.waitFloor) == list[0])
            {

                elevator.mode = elevator.WAIT_MODE;
            }

            if (elevator.mode==elevator.WAIT_MODE)                    
            {
                if (int.Parse(elevator.waitFloor)==list[0])
                {
                     list.Remove(list[0]);
                }
                if (list.Count!=0 && int.Parse(elevator.waitFloor) < list[0])
                {
                    elevator.mode = UP_MODE;
                    foreach (var item in storeList)
                    {
                        list.Add(item);
                    }
                    storeList.Clear();
                }
                else if (list.Count != 0 && int.Parse(elevator.waitFloor) > list[0])
                {
                   elevator.mode = DOWN_MODE;
                    foreach (var item in storeList)
                    {
                        list.Add(item);
                    }
                    storeList.Clear();
                }
            }
            if (elevator.mode==UP_MODE)
            {
                list.Sort();
                for (int i = 0; i < list.Count; i++)
                {
                    if (int.Parse(elevator.waitFloor) <= list[0])
                    {
                        break;
                    }
                    int temp = list[0];
                    list.Remove(list[0]);
                    list.Add(temp);
                }
               
                
            }
            else if(elevator.mode==DOWN_MODE)
            {
                list.Sort();
                list.Reverse();
                for (int i = 0; i < list.Count; i++)
                {
                    if (int.Parse(elevator.waitFloor) >= list[0])
                    {
                        break;
                    }
                    int temp = list[0];
                    list.Remove(list[0]);
                    list.Add(temp);
                }

            }

            if (list.Count==0)
            {
                return elevator.waitFloor;
            }
            else
            {
                return list[0].ToString();
            }
        }
    }

    public class FirstElevator : TestElevator
    {

        
    }

    public class SecondElevator : TestElevator
    {
        public SecondElevator()
        {
            this.MAX_WEIGHT = 800;
            this.MAX_SIZE = 10;
        }
    }

    public class ThirdElevator : TestElevator
    {
        public ThirdElevator()
        {
            this.MAX_WEIGHT = 800;
            this.MAX_SIZE = 10;
        }
    }

    public class FouthElevator : TestElevator
    {
        public FouthElevator()
        {
            this.MAX_WEIGHT = 2000;
            this.MAX_SIZE = 20;
        }
    }
}
