// Program 4
// CIS 199-01
// Due: 4/20/2021
// By: R2889

// This program uses records for a repair shop and outputs the information in required fields once, then
// will output again with changed information. It uses the input data to form a cost per object
using System;
using static System.Console;
namespace Program4
{
    class MainMethod
    {
        static void Main(string[] args)
        {
            const int NUM_OF_REPAIRS = 6;//const for the number of objects 
            RepairRecord[] repairRecords = new RepairRecord[NUM_OF_REPAIRS];//declares array for the repairs
            //created 6 objects and assigned values
            repairRecords[0] = new RepairRecord(42003, "Ford F150", "B2A19S0000", 2001, 56, "Nick Beeny", true);
            repairRecords[1] = new RepairRecord(40208, "Optimus Prime", "IW503MSKI8", 2019, 30, "Naruto", false);
            repairRecords[2] = new RepairRecord(50441, "Going Merry", "TSK830S3LK", 1999, 70, "Monkey D Luffy", true);
            repairRecords[3] = new RepairRecord(60942, "Car", "TUSKLOKSF5", 1099, 20, "Man", false);
            repairRecords[4] = new RepairRecord(20350, "The World", "AWLO04K20R", 1989, 150, "Dio Brando", true);
            repairRecords[5] = new RepairRecord(50278, "Windwall", "5T9SSL09R5", 1009, 100, "Yasuo", false);
            
            DisplayRepairRecord(repairRecords);//displays all the data once before changing values for the zip codes in 0-5
            repairRecords[0].ServiceLocationZip = 90045;
            repairRecords[1].ServiceLocationZip = 87659;
            repairRecords[2].ServiceLocationZip = 45869;
            repairRecords[3].ServiceLocationZip = 13697;
            repairRecords[4].ServiceLocationZip = 12345;
            repairRecords[5].ServiceLocationZip = 09876;
            DisplayRepairRecord(repairRecords);//diaplays the changed data sets 
        }
        public static void DisplayRepairRecord(params RepairRecord[] repairs)//Preconditions: A valid object is passed to parameter, Post: runs toString and calcCost methods for each object and outputs the methods
        {
            foreach (RepairRecord repair in repairs)
            {
                WriteLine(repair.ToString());
                Write("Calculate Cost Output: ");
                double doubleCost = repair.CalcCost();
                string stringCost = doubleCost.ToString("C");
                WriteLine(stringCost);
                WriteLine(""); 
            }
        }
    }
    class RepairRecord
    {
        const int DEFAULT_ZIP = 40204;//default zip code if an invalid one or none is entered
        const int MIN_ZIP = 00000; //minimum zip code value that can be entered
        const int MAX_ZIP = 99999;//max zip code value that can be entered
        const int SERIAL_LENGTH = 10;//exact length of the serial number that must be met
        const string DEFAULT_MAKE = "Unknown Make/Model";//default value for the make and model if none is entered
        const string DEFAULT_SERIAL_NUMBER = "A000000000";//default value for the serial number if none or an invalid number is entered
        const string DEFAULT_TECH = "John Smith";//defualt name if none is entered
        const int MIN_APPOINTMENT_LENGTH = 15;//minimum appointment length in minutes
        const int MAX_APPOINTMENT_LENGTH = 180;//max appointment length in minutes
        const int DEFAULT_APPOINTMENT_LENGTH = 30; //default appointment length if none or invalid number is entered
        const int WARRANTY_COST = 20; //default cost if the person has warranty
        const int FLAT_FEE = 25; //flat fee for people without warranty
        const double PER_MIN_FEE = 1.50; //per min charge for people without warranty
        const int MIN_TO_HOURS = 60; //used to convert appointment min to hours
        //private variables
        private int zipCode;//int to hold zip code
        private string makeAndModel; //string to hold the make and model
        private string serialNumber;//string to hold the serial number
        private int modelYear;//int to hold the model year
        private int appointmentLength;//int to hold the appointment length in minutes
        private string techName;//string to hold the technician's name
        private bool warrantyCoverage;//bool to change to true if person has warranty

        public RepairRecord(int ServiceLocationZipInput, string MakeAndModelInput, string SerialNumberInput, int ModelYearInput, int AppointmentLengthInput, string TechNameInput, bool WarrantyCoverageInput)
        {
            ServiceLocationZip = ServiceLocationZipInput;
            MakeAndModel = MakeAndModelInput;
            SerialNumber = SerialNumberInput;
            ModelYear = ModelYearInput;
            AppointmentLength = AppointmentLengthInput;
            TechName = TechNameInput;
            WarrantyCoverage = WarrantyCoverageInput;
        }
        public int ServiceLocationZip//Precondition: has to between 00000 and 99999, Postcondition: zip code is returned
        {
            get
            {
                return zipCode;
            }
            set
            {
                if (value >= MIN_ZIP && value <= MAX_ZIP)
                    zipCode = value;
                else
                    zipCode = DEFAULT_ZIP;
            }
        }
        public string MakeAndModel//Precondition: cannot be null, Post: returns Make and model
        {
            get
            {
                return makeAndModel;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value) == false)
                    makeAndModel = value;
                else
                    makeAndModel = DEFAULT_MAKE;
            }
        }
        public string SerialNumber//Precondition:has to be exactly 10 characters, Post: reutrns serial number
        {
            get
            {
                return serialNumber;
            }
            set
            {
                if (value.Length == SERIAL_LENGTH)
                    serialNumber = value;
                else
                    serialNumber = DEFAULT_SERIAL_NUMBER;
            }
         }
        public int ModelYear//Precondition: none, Post: returns model year
        {
            get
            {
                return modelYear;
            }
            set
            {
                modelYear = value;
            }
        }
        public int AppointmentLength//Precondition: Min length is 15 Max is 180, Post: returns Appointment Length
        {
            get
            {
                return appointmentLength;
            }
            set
            {
                if (value >= MIN_APPOINTMENT_LENGTH && value <= MAX_APPOINTMENT_LENGTH)
                    appointmentLength = value;
                else
                    appointmentLength = DEFAULT_APPOINTMENT_LENGTH;
            }
        }
        public string TechName//Precondition: techName is not blank, Post: returns techName
        {
            get
            {
                return techName;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value) == false)
                    techName = value;
                else
                    techName = DEFAULT_TECH;
            }
        }
        public bool WarrantyCoverage//Precondtion: none, Post: returns warranty coverage bool
        {
            get
            {
                return warrantyCoverage;
            }
            set
            {
                warrantyCoverage = value;
            }
        }
        public double LengthInHours//Preconditions: none, Post: Appointment minutes are returned as hours
        {
            get
            {
                return (Convert.ToDouble(AppointmentLength) / MIN_TO_HOURS);
            }
        }
        public double CalcCost()//Preconditions: none, Post: Calculates and returns cost value depending on warranty
        {
            if (!warrantyCoverage)
                return (FLAT_FEE + (PER_MIN_FEE * AppointmentLength));
            else
                return WARRANTY_COST;
        }
        public override string ToString()//Preconditions: Object has been created, Post: Returns string listing object properties
        {
            return ("Service Location Zip Code: " + ServiceLocationZip + "\nYear: " + ModelYear +
                "\nMake and Model: " + MakeAndModel + "\nSerial Number: " + SerialNumber + "\nAppointment Length: "
                 + AppointmentLength + "\nAppointment Hours: " + LengthInHours + "\nTechnician Name: " +
                 TechName + "\nWarranty Coverage?: " + WarrantyCoverage);
        }

    }
}
