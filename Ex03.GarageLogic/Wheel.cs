using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        protected string m_ManufacturerName;
        protected float m_CurrentAirPressure;
        protected float m_MaximalAirPressure;

        public Wheel(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaximalAirPressure)
        {
            if (i_CurrentAirPressure > i_MaximalAirPressure)
            {
                throw new ValueOutOfRangeException(0, i_MaximalAirPressure);
            }
            this.m_ManufacturerName = i_ManufacturerName;
            this.m_CurrentAirPressure = i_CurrentAirPressure;
            this.m_MaximalAirPressure = i_MaximalAirPressure;
        }

        public void AddPressure(float i_AirPressureToAdd) //CHECK IF POSSIBLE TO COMBINE WITH FUEL AND ELECTRIC
        {
            if (m_CurrentAirPressure + i_AirPressureToAdd <= m_MaximalAirPressure)
            {
                m_CurrentAirPressure += i_AirPressureToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(0, MaximalAirPressure - m_CurrentAirPressure, true, "'Air Pressure'");
            }
        }

        public float CurrentAirPressure {
            get
            {
                return m_CurrentAirPressure;
            }
        }

        public float MaximalAirPressure
        {
            get
            {
                return m_MaximalAirPressure;
            }
        }

        public string Manufacturer
        {
            get
            {
                return m_ManufacturerName;
            }
        }

    }
}
