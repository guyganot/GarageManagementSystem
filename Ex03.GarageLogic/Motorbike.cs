using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Motorbike : Vehicle
    {
        protected eLicenseType m_LicenseType;
        protected float m_EngineSize;
        //private int m_AmountOfDoors = 2;

        public Motorbike(string i_VehicleName, string i_LicenceNumber, float i_FuelPercentage, List<Wheel> i_WheelTypes,
            EnergyType i_EnergyType) : this(i_VehicleName,
            i_LicenceNumber, i_FuelPercentage, i_WheelTypes, i_EnergyType, eLicenseType.A, 220)
        {

        }

        public Motorbike(string i_VehicleName, string i_LicenceNumber, float i_FuelPercentage, List<Wheel> i_WheelTypes, EnergyType i_EnergyType, eLicenseType i_eLicenseType, float i_EngineSize) : base(i_VehicleName, i_LicenceNumber, i_FuelPercentage, i_WheelTypes, i_EnergyType)
        {
            this.m_LicenseType = i_eLicenseType;
            this.m_EngineSize = i_EngineSize;
            this.r_listOfQuestions.Add("Enter license type:");
            this.r_listOfQuestions.Add("Enter size of engine: :");
        }

        public eLicenseType LicenseType {
            get
            {
                return m_LicenseType;
            }
            set
            {
                m_LicenseType = value;
            }
        }

        public override PropertyInfo[] GetProperties()
        {
            return typeof(Motorbike).GetProperties(); ;
        }

        public float EngineSize
        {
            get
            {
                return m_EngineSize;
            }
            set
            {
                m_EngineSize = value;
            }
        }

    }

    public enum eLicenseType
    {
        A = 1,
        A1 = 2,
        B1 = 3,
        BB = 4
        
    }
}
