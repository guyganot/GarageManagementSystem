using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class EnergyType
    {
        protected float m_CurrentAmountOfEnergy;
        protected float m_MaximalAmountOfEnergy;
        protected eFuelType m_eFuelType;

        protected EnergyType(float i_CurrentEnergyAmount, float i_MaximalEnergyAmount, eFuelType i_eFuelType)
        {
            if (i_CurrentEnergyAmount > i_MaximalEnergyAmount)
            {
                throw new ValueOutOfRangeException(0, i_MaximalEnergyAmount,true, "Energy");
            }

            this.m_CurrentAmountOfEnergy = i_CurrentEnergyAmount;
            this.m_MaximalAmountOfEnergy = i_MaximalEnergyAmount;
        }
    }
    public class Fuel : EnergyType
    {
        public Fuel(float i_CurrentFuelAmount, float i_MaximalFuelAmount, eFuelType i_eFuelType) : base(i_CurrentFuelAmount, i_MaximalFuelAmount, i_eFuelType)
        {
            this.m_eFuelType = i_eFuelType;
        }
        public void AddFuel(float i_LitersToAdd, eFuelType i_FuelTypeToAdd)
        {
            if (i_FuelTypeToAdd == m_eFuelType)
            {
                if (i_LitersToAdd + CurrentFuelAmount <= MaximalFuelAmount)
                {
                    CurrentFuelAmount += i_LitersToAdd;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, MaximalFuelAmount - CurrentFuelAmount);
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public float CurrentFuelAmount
        {
            get
            {
                return m_CurrentAmountOfEnergy;
            }

            set
            {
                m_CurrentAmountOfEnergy = value;
            }
        }

        public float MaximalFuelAmount
        {
            get
            {
                return m_MaximalAmountOfEnergy;
            }
        }

        public eFuelType FuelType {
            get
            {
                return m_eFuelType;
            }
        }
    }

    public class Electric : EnergyType
    {
        public Electric(float i_BatteryTimeLeft, float i_MaximalBatteryTime, eFuelType i_eFuelType) : base(i_BatteryTimeLeft, i_MaximalBatteryTime, i_eFuelType)
        {
            
        }
        public void addBattery(float i_HoursToAdd)
        {
            if (i_HoursToAdd + BatteryTimeLeft <= MaximalBatteryTime)
            {
                BatteryTimeLeft += i_HoursToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(0, MaximalBatteryTime - BatteryTimeLeft);
            }
        }

        public float BatteryTimeLeft
        {
            get
            {
                return m_CurrentAmountOfEnergy;
            }

            set
            {
                m_CurrentAmountOfEnergy = value;
            }
        }

        public float MaximalBatteryTime
        {
            get
            {
                return m_MaximalAmountOfEnergy;
            }
        }
    }

    public enum eFuelType
    {
        Soler = 1,
        Octan95 = 2,
        Octan96 = 3,
        Octan98 = 4,
        Electric = 5
    }
}
