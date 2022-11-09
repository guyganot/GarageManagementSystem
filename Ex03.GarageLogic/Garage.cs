using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        // LICNECE NUMBER : VehicleInfo
        private readonly Dictionary<string, VehicleInfo> r_GarageVehicles;
        private readonly Dictionary<string, float[]> r_VehiclesThatGarageSupports;

        public Garage()
        {
            this.r_GarageVehicles = new Dictionary<string, VehicleInfo>();
            this.r_VehiclesThatGarageSupports = new Dictionary<string, float[]>();
        }

        public Dictionary<string, float[]> VehiclesThatGarageSupports
        {
            get
            {
                return r_VehiclesThatGarageSupports;
            }
        }

        public void AddSupportToVehicle(string i_TypeOfVehicle, int i_NumberOfWheels, float i_MaxAirPressure, float i_MaxEnergy, eFuelType i_eFuelType)
        {
            float[] staticInfoOfVehicle = { (float)i_NumberOfWheels, i_MaxAirPressure, i_MaxEnergy, (float)i_eFuelType };
            VehiclesThatGarageSupports.Add(i_TypeOfVehicle, staticInfoOfVehicle);
        }

        public bool AddNewVehicleToGarage(string i_OwnerName, string i_PhoneNumber, Vehicle i_VehicleToCreate)
        {
            bool addSuccessful = false;
            VehicleInfo i_VehicleInfoToAdd = new VehicleInfo(i_OwnerName, i_PhoneNumber, i_VehicleToCreate);
            string LicenceNumber = i_VehicleInfoToAdd.OwnedVehicle.LicenceNumber;
            if (doesValueExist(LicenceNumber))
            {
                r_GarageVehicles[LicenceNumber].VehicleStatus = eVehicleStatus.InRepair;
            }
            else
            {
                r_GarageVehicles.Add(LicenceNumber, i_VehicleInfoToAdd);
                addSuccessful = true;
            }

            return addSuccessful;
        }


        //'?' checks is object is nullable
        public List<string> GetVehiclePlateNumbers(eVehicleStatus? i_eVehicleStatusFilter = null)
        {
            List<string> VehiclePlateNumbers = new List<string>();
            if (i_eVehicleStatusFilter != null)
            {
                IEnumerable<KeyValuePair<string, VehicleInfo>> keyValuePairOfFilteredKeys = r_GarageVehicles.Where(vehicle =>
                    vehicle.Value.VehicleStatus == i_eVehicleStatusFilter);
                VehiclePlateNumbers = (from kvp in keyValuePairOfFilteredKeys select kvp.Key).ToList();

            }
            else
            {
                VehiclePlateNumbers = r_GarageVehicles.Keys.ToList();
            }

            return VehiclePlateNumbers;

        }

        //TODO CHECK IF WE SHOULD ADDRESS CHANGE TO THE SAME VALUE OF VEHICLE STATUS
        public bool ChangeVehicleStatus(string i_PlateNumber, eVehicleStatus i_eVehicleStatus)
        {
            bool changeStatusSuccesful = false;
            if (changeStatusSuccesful = doesValueExist(i_PlateNumber))
            {
                r_GarageVehicles[i_PlateNumber].VehicleStatus = i_eVehicleStatus;
                //Console.WriteLine(r_GarageVehicles[i_PlateNumber].Name);
            }

            return changeStatusSuccesful;
        }


        public Vehicle VehicleFactory(string i_VehicleName, string i_LicenceNumber, float i_FuelPercentage,
            List<Wheel> i_WheelTypes, EnergyType i_EnergyType, string i_nameOfClass)
        {
            Type typeOfVehicle = Type.GetType(typeof(Garage).Namespace + "." + i_nameOfClass);
            if (typeOfVehicle == null)
            {
                typeOfVehicle = Type.GetType(typeof(Garage).Namespace + "." + "Vehicle");
            }
            //Vehicle resultVehicle = new Vehicle(i_VehicleName, i_LicenceNumber, i_FuelPercentage, i_WheelTypes, i_EnergyType);
            Vehicle generatedVehicle = (Vehicle)Activator.CreateInstance(typeOfVehicle, i_VehicleName,
                i_LicenceNumber,
                i_FuelPercentage, i_WheelTypes, i_EnergyType);
            List<Object> listOfArguments = new List<object>();
            string extraArgument;
            for (int i = 0; i < generatedVehicle.Questions.Count; i++)
            {
                Console.WriteLine(generatedVehicle.Questions[i]);
                extraArgument = Console.ReadLine();
                if (extraArgument.All(c => Char.IsLetter(c)))
                {
                    extraArgument = extraArgument.Substring(0, 1).ToUpper() + extraArgument.Substring(1).ToLower();
                }
                listOfArguments.Add(extraArgument);
                //"4", "Hello"
            }

            PropertyInfo[] properties = typeOfVehicle.GetProperties();
            int j = 0;
            foreach (PropertyInfo property in properties)
            {
                if (j >= generatedVehicle.Questions.Count) //2
                {
                    break;
                }
                Object castedProperty = null;
                try
                {
                    //Console.WriteLine(property.PropertyType);
                    if (property.PropertyType.IsEnum)
                    {
                        try
                        {
                            castedProperty = Enum.Parse(property.PropertyType, (string)listOfArguments[j]);
                        }
                        catch (ArgumentException e)
                        {
                            Console.WriteLine("Could not find value with given name.");
                            //throw;
                        }
                    }
                    else if (property.PropertyType.IsPrimitive)
                    {
                        try
                        {
                            castedProperty = Convert.ChangeType(listOfArguments[j], property.PropertyType);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Invalid input.");
                        }
                    }
                    else
                    {
                        castedProperty = Activator.CreateInstance(property.PropertyType, listOfArguments[j]);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                //eNum, int
                if (property.PropertyType == castedProperty.GetType())
                {
                    listOfArguments[j] = castedProperty;
                }
                else
                {
                    throw new FormatException("Could not convert given types");
                }

                j++;
            }

            //eDoorColors green
            //int 4
            generatedVehicle.AddAdditionalData(listOfArguments);
            return generatedVehicle;
        }

        bool IsNumeric(Type type)
        {
                return type.IsPrimitive && type != typeof(char) && type != typeof(bool);
        }

        public bool AddAirToTires(string i_PlateNumber)
        {
            bool addedTireSuccessful = false;
            if (addedTireSuccessful = doesValueExist(i_PlateNumber))
            {
                List<Wheel> wheels = r_GarageVehicles[i_PlateNumber].OwnedVehicle.Wheels;
                foreach (Wheel wheel in wheels)
                {
                    float pressureToAdd = wheel.MaximalAirPressure - wheel.CurrentAirPressure;
                    wheel.AddPressure(pressureToAdd);
                }

                addedTireSuccessful = true;
            }

            if (addedTireSuccessful)
            {
                Console.WriteLine("Filled up air succesfuly!");
            }

            return addedTireSuccessful;
        }

        public bool AddEnergy(string i_PlateNumber, eFuelType i_eFuelType, float i_EnergyToAdd)
        {
            bool succesfulAddEnergy = false;
            if (i_eFuelType == eFuelType.Electric)
            {
                succesfulAddEnergy = recharge(i_PlateNumber, i_EnergyToAdd);
            }
            else
            {
                succesfulAddEnergy = refuel(i_PlateNumber, i_eFuelType, i_EnergyToAdd);
            }

            return succesfulAddEnergy;
        }

        private bool refuel(string i_PlateNumber, eFuelType i_eFuelType, float i_LitersFuelToAdd)
        {
            bool refuelSuccesful = false;
            if (doesValueExist(i_PlateNumber))
            {
                EnergyType energyType = r_GarageVehicles[i_PlateNumber].OwnedVehicle.VehicleEnergyType; //FUEL/ELECTRIC
                if (refuelSuccesful = energyType is Fuel)
                {
                    ((Fuel)energyType).AddFuel(i_LitersFuelToAdd, i_eFuelType);
                }

            }

            return refuelSuccesful;
        }

        private bool recharge(string i_PlateNumber, float i_MinutesToAdd)
        {
            bool rechargeSuccesful = false;
            if (doesValueExist(i_PlateNumber))
            {
                EnergyType energyType = r_GarageVehicles[i_PlateNumber].OwnedVehicle.VehicleEnergyType; //FUEL/ELECTRIC
                if (rechargeSuccesful = energyType is Electric)
                {
                    ((Electric)energyType).addBattery(i_MinutesToAdd/60);
                }

            }

            return rechargeSuccesful;
        }

        public bool displayVehicleInfo(string i_PlateNumber, ref string o_vehicleInfoString)
        {
            bool displaySuccesful = false;
            if (displaySuccesful = doesValueExist(i_PlateNumber))
            {
                o_vehicleInfoString = r_GarageVehicles[i_PlateNumber].ToString();
            }

            return displaySuccesful;
        }

        private bool doesValueExist(string i_PlateNumber)
        {
            return r_GarageVehicles.ContainsKey(i_PlateNumber);
        }
    }
}
