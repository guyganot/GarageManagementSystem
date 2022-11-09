using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private eDoorColors m_eDoorColor;
        //public int m_EngineCapacity;
        private int m_AmountOfDoors;

        public Car(string i_VehicleName, string i_LicenceNumber, float i_FuelPercentage, List<Wheel> i_WheelTypes,
            EnergyType i_EnergyType) : this(i_VehicleName,
            i_LicenceNumber, i_FuelPercentage, i_WheelTypes, i_EnergyType, eDoorColors.Blue, 4)
        {

        }

        public Car(string i_VehicleName, string i_LicenceNumber, float i_FuelPercentage, List<Wheel> i_WheelTypes, EnergyType i_EnergyType, eDoorColors i_eDoorColor, int i_amountOfDoors) : base(i_VehicleName, i_LicenceNumber, i_FuelPercentage, i_WheelTypes, i_EnergyType)
        {
            this.m_eDoorColor = i_eDoorColor;
            this.m_AmountOfDoors = i_amountOfDoors;;
            //this.m_EngineCapacity = i_EngineCapacity;
            this.r_listOfQuestions.Add("Enter the car color:");
            this.r_listOfQuestions.Add("Enter amount of doors:");
        }


        public eDoorColors eDoorColor
        {
            get
            {
                return m_eDoorColor;
            }
            set
            {
                m_eDoorColor = value;
            }
        }
        public int AmountOfDoors
        {
            get
            {
                return m_AmountOfDoors;
            }
            set
            {
                m_AmountOfDoors = value;
            }
        }

        public override PropertyInfo[] GetProperties()
        {
            return typeof(Car).GetProperties();
        }
    }

    public enum eDoorColors
    {
        Red = 1,
        White = 2,
        Green = 3,
        Blue = 4
    }
}
