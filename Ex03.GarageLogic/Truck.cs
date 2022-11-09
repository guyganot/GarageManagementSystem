using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_IsDrivingCooledContents;
        private float m_TrunkSize;
        //private int m_AmountOfWheels = 16;

        public Truck(string i_VehicleName, string i_LicenceNumber, float i_FuelPercentage, List<Wheel> i_WheelTypes,
            EnergyType i_EnergyType) : this(i_VehicleName,
            i_LicenceNumber, i_FuelPercentage, i_WheelTypes, i_EnergyType, i_IsDrivingCooledContents:false, i_TrunkSize: 20)
        {

        }

        public Truck(string i_VehicleName, string i_LicenceNumber, float i_FuelPercentage, List<Wheel> i_WheelTypes,
            EnergyType i_EnergyType, bool i_IsDrivingCooledContents, float i_TrunkSize) : base(i_VehicleName, i_LicenceNumber, i_FuelPercentage, i_WheelTypes, i_EnergyType)
        {
            this.m_IsDrivingCooledContents = i_IsDrivingCooledContents;
            this.m_TrunkSize = i_TrunkSize;
            this.r_listOfQuestions.Add("Is the truck carrying cooled content?");
            this.r_listOfQuestions.Add("Enter size of trunk: ");
        }

        public bool IsDrivingCooledContents
        {
            get
            {
                return m_IsDrivingCooledContents;
            }
            set
            {
                m_IsDrivingCooledContents = value;
            }
        }

        public override PropertyInfo[] GetProperties()
        {
            return typeof(Truck).GetProperties(); ;
        }

        public float TrunkSize
        {
            get
            {
                return m_TrunkSize;
            }
            set
            {
                m_TrunkSize = value;
            }
        }
    }
}
