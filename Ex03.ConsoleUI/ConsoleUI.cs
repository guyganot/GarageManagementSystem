using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class ConsoleUI
    {
        public static readonly Ex03.GarageLogic.Garage r_Garage = new Garage();
        public const bool v_RepeatReadInputUntilValid = true;

        public static void Main(string[] args)
        {
            //LINES 18-21 ARE FOR TESTING!! TODO: DELETE AFTER USAGE
            ConsoleUI ui = new ConsoleUI();
            //Motorbike motor = new Motorbike("Toyota", "8", 0.89f, ui.createWheelList(2, 31, 27, "mishlen"),
            //    new Electric(1, 2.5f, eFuelType.Electric), eLicenseType.A1, 120);
            //r_Garage.AddNewVehicleToGarage("Guy", "052131131", motor);
            AddSupportToBaseVehicles();
            Console.WriteLine($"Hello and welcome to our garage.");
            int userChoice = -1;
            while (userChoice != 0)
            {
                try
                {
                    userChoice = new ConsoleUI().PromptMainMenu();
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine(e);
                    userChoice = -1;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine("invalid input. try again");
                }
                catch (FormatException e)
                {
                    Console.WriteLine("invalid input. try again");
                }
            }
        }

        private static void AddSupportToBaseVehicles()
        {
            r_Garage.AddSupportToVehicle("Electric Car", 4, 29, 3.3f, eFuelType.Electric);
            r_Garage.AddSupportToVehicle("Fuel Car", 4, 29, 38f, eFuelType.Octan95);
            r_Garage.AddSupportToVehicle("Electric Motorbike", 2, 31, 2.5f, eFuelType.Electric);
            r_Garage.AddSupportToVehicle("Fuel Motorbike", 2, 31, 6.2f, eFuelType.Octan98);
            r_Garage.AddSupportToVehicle("Fuel Truck", 16, 24, 120f, eFuelType.Soler);
            r_Garage.AddSupportToVehicle("Fuel Tractor", 2, 30, 120f, eFuelType.Soler);
        }

        private int PromptMainMenu()
        {
            int option;
            Console.WriteLine("1. Add a new vehicle to the garage.\n" +
                              "2. Display license plates of the garage's vehicles.\n" +
                              "3. Change state of vehicle.\n" +
                              "4. Fill up air in the tires of a vehicle.\n" +
                              "5. Add energy to vehicle.\n" +
                              "6. Display vehicle info.\n" +
                              "0. Quit.");
            try
            {
                option = int.Parse(Console.ReadLine());
            }
            catch (FormatException e)
            {
                throw;
            }
            catch (ArgumentNullException e)
            {
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid input.");
                throw;
            }

            switch (option)
            {
                case 0:
                    break;
                case 1:
                    try
                    {
                        PromptAddNewVehicle();
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(e);
                    }
                    catch (ValueOutOfRangeException e)
                    {
                        Console.WriteLine(e);
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine(e);
                    }
                    catch (Exception e) { }

                    break;
                case 2:
                    PromptDisplayLicensePlates();
                    break;
                case 3:
                    PromptChangeVehicleStatus();
                    break;
                case 4:
                    try
                    {
                        PromptFillTires();
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(e);
                    }
                    catch (ValueOutOfRangeException e)
                    {
                        Console.WriteLine(e);
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine(e);
                    }

                    break;
                case 5:
                    try
                    {
                        PromptAddEnergy();
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(e);
                    }
                    catch (ValueOutOfRangeException e)
                    {
                        Console.WriteLine(e);
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine(e);
                    }
                    break;
                case 6:
                    DisplyVehicleInfo();
                    break;
                default:
                    throw new ValueOutOfRangeException(0, 6);
            }

            Console.WriteLine("\nPress Any Key To Continue...");
            Console.ReadKey();
            Console.Clear();
            return option;
        }

        private void DisplyVehicleInfo()
        {
            string vehicleInfoString = "Couldn't find vehicle";
            Console.WriteLine("Enter plate number: ");
            Garage.displayVehicleInfo(Console.ReadLine(), ref vehicleInfoString);
            Console.WriteLine(vehicleInfoString);
        }

        private void PromptAddEnergy()
        {
            Console.WriteLine("Enter plate number: ");
            string plateNumber = Console.ReadLine();
            Console.WriteLine("What type of energy would you want to add?");
            DisplayOptions(typeof(eFuelType));
            string userChoiceOfFuel = Console.ReadLine();
            eFuelType fuelType;
            if (Enum.IsDefined(typeof(eFuelType), userChoiceOfFuel))
            {
                fuelType = (eFuelType) Enum.Parse(typeof(eFuelType), userChoiceOfFuel);
            }
            else
            {
                throw new ArgumentException("Chosen energy type is not supported");
            }
            float energyToAdd = 0;
            if (fuelType == eFuelType.Electric)
            {
                Console.WriteLine("How many minutes would you like to charge?");
            }
            else
            {
                Console.WriteLine("How much fuel would you like to add?");
            }

            try
            {
                energyToAdd = float.Parse(Console.ReadLine());
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Expected a float, wrong type.");
                throw;
            }

            if (Garage.AddEnergy(plateNumber, fuelType, energyToAdd))
            {
                Console.WriteLine("Energy added successfully!");
            }
            else
            {
                Console.WriteLine("Couldn't find plate number");
            }


        }

        private void PromptFillTires()
        {
            Console.WriteLine("Enter plate number: ");
            string plateNumber = Console.ReadLine();
            try
            {
                Garage.AddAirToTires(plateNumber);
            }
            catch (ValueOutOfRangeException e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public static void DisplayOptions(Type e)
        {
            Enum.GetNames(e).ToList().ForEach(val => Console.WriteLine($"   {val}"));
        }

        private void PromptChangeVehicleStatus()
        {
            Console.WriteLine("Enter Plate Number: ");
            string plateNumber = Console.ReadLine();
            Console.WriteLine("Choose status to change to: ");
            DisplayOptions(typeof(eVehicleStatus));
            //Enum.GetNames(typeof(eVehicleStatus)).ToList().ForEach(val => Console.WriteLine($"   {val}"));
            string vehicleStatus = Console.ReadLine();
            while (!Enum.IsDefined(typeof(eVehicleStatus), vehicleStatus))
            {
                Console.WriteLine("Invalid input, try again.");
                vehicleStatus = Console.ReadLine();
            }

            eVehicleStatus filter = (eVehicleStatus) Enum.Parse(typeof(eVehicleStatus), vehicleStatus);
            Garage.ChangeVehicleStatus(plateNumber, filter);
            Console.WriteLine($"The vehicle’s status has changed successfully to: {vehicleStatus}");
        }

        public Garage Garage
        {
            get { return r_Garage; }
        }

        private void PromptDisplayLicensePlates()
        {
            string enumNames = "";
            Enum.GetNames(typeof(eVehicleStatus)).ToList().ForEach(val => enumNames += val + "/");
            enumNames = enumNames.Substring(0, enumNames.Length - 1);
            Console.Write($"Enter vehicle status ({enumNames}) or leave empty ");
            string vehicleStatusFilter = Console.ReadLine();
            List<string> licensePlates;
            if (Enum.IsDefined(typeof(eVehicleStatus), vehicleStatusFilter))
            {
                eVehicleStatus filter = (eVehicleStatus) Enum.Parse(typeof(eVehicleStatus), vehicleStatusFilter);
                licensePlates = Garage.GetVehiclePlateNumbers(filter);
            }
            else if (vehicleStatusFilter == "")
            {
                licensePlates = Garage.GetVehiclePlateNumbers();
            }
            else
            {
                Console.WriteLine("Invalid option.");
                return;
            }

            licensePlates.ForEach(val => Console.WriteLine(val));
        }

        public string getStringWithUpperAsFirstChar()
        {
            string getInput = Console.ReadLine();
            getInput = getInput.Substring(0, 1).ToUpper() + getInput.Substring(1).ToLower();
            return getInput;
        }

        private void PromptAddNewVehicle()
        {
            Console.WriteLine("Enter a license plate number: ");
            string licensePlate = Console.ReadLine();
            Console.WriteLine("Enter a vehicle model: "); //Toyota
            string vehicleName = getStringWithUpperAsFirstChar();
            Console.WriteLine("Enter a vehicle type: "); //Motorbike
            string vehicleType = getStringWithUpperAsFirstChar();
            Console.WriteLine("Enter a wheel manufacturer: ");
            string wheelManufacturer = Console.ReadLine();
            Console.WriteLine("Enter current wheel air pressure: ");
            float airPressure = 0;
            while (v_RepeatReadInputUntilValid)
            {
                try
                {
                    airPressure = float.Parse(Console.ReadLine());
                    break;
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Invalid input, try again.");
                }
            }

            Console.WriteLine("Enter energy type (Electric/Fuel): ");
            Type energyType = typeof(EnergyType);
            string energyTypeChoice;
            float currentEnergy = 0;
            string currentSentence = "";
            while (v_RepeatReadInputUntilValid)
            {
                switch (energyTypeChoice = Console.ReadLine().ToLower())
                {
                    case "electric":
                        currentSentence = "Enter hours left of battery: ";
                        //maximalSentence = "Enter maximal hours of battery: ";
                        energyType = typeof(Electric);
                        break;
                    case "fuel":
                        currentSentence = "Enter current amount of fuel: ";
                        //maximalSentence = "Enter maximal amount of fuel: ";
                        energyType = typeof(Fuel);
                        break;
                    default:
                        Console.WriteLine("Invalid Input, try again.");
                        continue;
                    //Dict =>  FuelType VehicleType
                }

                break;
            }

            Console.WriteLine(currentSentence);
            while (v_RepeatReadInputUntilValid)
            {
                try
                {
                    currentEnergy = float.Parse(Console.ReadLine());
                    break;
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Invalid input, try again.");
                }
            }

            string completeVehicleType = energyType.Name + " " + vehicleType; //"Fuel car"
            //Console.WriteLine($"Given key: {completeVehicleType}");
            int wheelAmount = 0;
            float maximumAirPressure = 0, maximumEnergy = 0;
            eFuelType fuelType;
            Vehicle vehicle = null;
            if (Garage.VehiclesThatGarageSupports.ContainsKey(completeVehicleType))
            {
                wheelAmount = (int) Garage.VehiclesThatGarageSupports[completeVehicleType][0];
                maximumAirPressure = Garage.VehiclesThatGarageSupports[completeVehicleType][1];
                maximumEnergy = Garage.VehiclesThatGarageSupports[completeVehicleType][2];
                fuelType = (eFuelType) Garage.VehiclesThatGarageSupports[completeVehicleType][3];
                List<Wheel> wheelsOfVehicle;
                try
                {
                    wheelsOfVehicle =
                        createWheelList(wheelAmount, maximumAirPressure, airPressure, wheelManufacturer);
                }
                catch (ValueOutOfRangeException e)
                {
                    //Console.WriteLine(e);
                    throw;
                }
                EnergyType castedEnergyType;
                try
                {
                    castedEnergyType =
                        (EnergyType)Activator.CreateInstance(energyType, currentEnergy, maximumEnergy, fuelType);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"The chosen value is out of bounds. (not between {0} and {maximumEnergy})");
                    throw;
                }
                string garageNamespace = typeof(Garage).Namespace;
                //Type typeOfVehicle = Type.GetType(garageNamespace+"."+vehicleType);
                Vehicle createdVehicle = Garage.VehicleFactory(vehicleName, licensePlate, (currentEnergy / maximumEnergy),
                    wheelsOfVehicle, castedEnergyType, vehicleType);
                Console.WriteLine("Enter Owner Name:");
                string ownerName = Console.ReadLine();
                Console.WriteLine("Enter Owner Phone Number:");
                string phoneNumber = Console.ReadLine();
                bool addSuccesful = Garage.AddNewVehicleToGarage(ownerName, phoneNumber, createdVehicle);
                if (!addSuccesful)
                {
                    Console.WriteLine("Vehicle plate number already exists in the garage, we put the car in repair.");
                }
                else
                {
                    Console.WriteLine($"{vehicleType} Added Succesfuly!");
                }
            }
            else
            {
                Console.WriteLine("Vehicle is not supported");
            }
        }
        
        private List<Wheel> createWheelList(int i_WheelAmount, float i_MaximumPressure, float i_CurrentPressure, string i_Manufacturer)
        {
            List<Wheel> wheels = new List<Wheel>();
            for (int i = 0; i < i_WheelAmount; i++)
            {
                try
                {
                    wheels.Add(new Wheel(i_Manufacturer, i_CurrentPressure, i_MaximumPressure));
                }
                catch (ValueOutOfRangeException e)
                {
                    //Console.WriteLine(e);
                    throw;
                }
            }

            return wheels;
        }

       
    }

}
