using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleInfo //VehicleInfo
    {
        private string m_Name;
        private string m_PhoneNumber;
        private Vehicle m_Vehicle;
        private eVehicleStatus m_eVehicleStatus;

        public VehicleInfo(string i_Name, string i_PhoneNumber, Vehicle i_Vehicle)
        {
            this.m_Name = i_Name;
            this.m_PhoneNumber = i_PhoneNumber;
            this.m_Vehicle = i_Vehicle;
            this.m_eVehicleStatus = eVehicleStatus.InRepair;
        }

        public string Name {
            get
            {
                return m_Name;
            }
        }

        public string PhoneNumber {
            get
            {
                return m_PhoneNumber;
            }
        }

        public Vehicle OwnedVehicle {
            get
            {
                return m_Vehicle;
            }
        }

        public eVehicleStatus VehicleStatus {
            get
            {
                return m_eVehicleStatus;
            }
            set
            {
                m_eVehicleStatus = value;
            }
        }

        public override string ToString()
        {
            int i = 0;
            string wheelDescription = "";
            string fuelTypeOrBattery = OwnedVehicle.VehicleEnergyType is Fuel
                ? $"    Fuel Type: {Enum.GetName(typeof(eFuelType), ((Fuel) OwnedVehicle.VehicleEnergyType).FuelType)}\n" +
                  $"    Amount of fuel left: {((Fuel) OwnedVehicle.VehicleEnergyType).CurrentFuelAmount} liters."
                : $"    Hours of battery left: {((Electric) OwnedVehicle.VehicleEnergyType).BatteryTimeLeft}.";
            foreach (Wheel wheel in OwnedVehicle.Wheels)
            {
                wheelDescription += $"  Wheel {++i}:\n" +
                                    $"      Manufacturer Name: {wheel.Manufacturer}.\n" +
                                    $"      Air Pressure: {wheel.CurrentAirPressure}.\n";
            }

            string message = $"**************************\n" +
                             $"Licence Plate Number: {OwnedVehicle.LicenceNumber}\n" +
                             $"Vehicle Model: {OwnedVehicle.VehicleName}\n" +
                             $"Owner Name: {Name}\n" +
                             $"Vehicle Status: {VehicleStatus}\n" +
                             $"Wheels Description: \n{wheelDescription}" +
                             $"Fuel Status: \n{fuelTypeOrBattery}\n" +
                             $"**************************\n";

            return message;
        }
    }

    public enum eVehicleStatus
    {
        InRepair,
        Repaired,
        PayedFor
    }
}
