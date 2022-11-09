using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Vehicle
    {
        protected string m_VehicleName;
        protected string m_LicenceNumber;
        protected float m_FuelPercentage;
        protected List<Wheel> m_WheelTypes;
        protected EnergyType m_EnergyType;
        protected readonly List<string> r_listOfQuestions;
        //protected int m_amountOfWheels;

        public Vehicle(string i_VehicleName, string i_LicenceNumber, float i_FuelPercentage, List<Wheel> i_WheelTypes,
            EnergyType i_EnergyType)
        {
            this.m_VehicleName = i_VehicleName;
            this.m_LicenceNumber = i_LicenceNumber;
            this.m_FuelPercentage = i_FuelPercentage;
            this.m_WheelTypes = i_WheelTypes;
            this.m_EnergyType = i_EnergyType;
            this.r_listOfQuestions = new List<string>();
            //this.m_amountOfWheels = i_amountOfWheels;
        }

        public List<string> Questions
        {
            get
            {
                return r_listOfQuestions;
            }
        }

        public string LicenceNumber {
            get
            {
                return m_LicenceNumber;
            }
        }

        public string typeOfClass()
        {
            return this.GetType().Name;
        }

        public bool AddAdditionalData(List<object> additionalData)
        {
            bool success = false;
            for (int i = 0; i < additionalData.Count; i++)
            {
                try
                {
                    GetProperties()[i].SetValue(this, additionalData[i], null);
                }
                catch (Exception e)
                {
                    Console.WriteLine("conversion of types failed");
                    success = false;
                    throw;
                }
            }

            return success;
        }

        public virtual PropertyInfo[] GetProperties()
        {
            return null;
        }

        public List<Wheel> Wheels {
            get
            {
                return m_WheelTypes;
            }
        }
        // EnergyType veh;
        // veh = new Fuel();
        // veh.addEnergy();
        // veh.addfuel(2 args);
        //
        public EnergyType VehicleEnergyType {
            get
            {
                return m_EnergyType;
            }
        }

        public string VehicleName {
            get
            {
                return m_VehicleName;
            }
        }

        public enum eVehicleType
        {
            Car=1,
            Motorbike=2,
            Truck=3
        }

    }

}
