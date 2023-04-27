using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Web.ClientServices;
using System.IO;
using System.Web.Script.Services;

namespace WebTest1
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://microsoft.com/webservices/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        private string FileName = "C:\\My Stuff\\Diplomovka\\WebTest1_FinalVerision_1_1\\WebTest1_FinalVerision_1_1\\WebTest1\\cars.xml";
        private XmlDocument doc;
      





      

        
        //TODO---------------------------------------------------------------------------AddCarToXml()-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        [WebMethod(Description = "Add new car to Database")]
        public void addCarToXml(string brand, string model, string bodytype, int yearOfProduction, string VINNumber, string color, string transmission, string fuel, float mileage, float price, string historicalServiceData, DateTime dateOfRegistration)
        {
            // nacitanie existujuceho xml dokumentu
            XDocument doc = XDocument.Load(FileName);


            // nacitanie id + 1 pri kazdom novom zazname
            var items = doc.Root.Elements("Car");
            int maxId = items.Max(i => (int)i.Element("Id"));
            int newId = maxId + 1;



            //vytvorenie noveho elementu pre auto
            XElement newCar = new XElement("Car",
                                    new XElement("Id", newId),
                                    new XElement("Brand", brand),
                                    new XElement("Model", model),
                                    new XElement("Bodytype", bodytype),
                                    new XElement("Yearofproduction", yearOfProduction),
                                    new XElement("VINNumber", VINNumber),
                                    new XElement("Color", color),
                                    new XElement("Transmission", transmission),
                                    new XElement("Fuel", fuel),
                                    new XElement("Mileage", mileage),
                                    new XElement("Price", price),
                                    new XElement("Historicalservicedata", historicalServiceData),
                                    new XElement("Dateofregistration", dateOfRegistration.ToString("dd.MM.yyyy"))
                                    );


            if (dateOfRegistration != DateTime.MinValue)
            {
                newCar.Add(new XElement("Status", "Unsold"));
            }



            //pridanie noveho elementu do korenoveho elementu
            doc.Root.Add(newCar);


            //ulozenie zmen do xml dokumentu
            doc.Save(FileName);
        }

        //TODO--------------------------------------------------------------------------ShowAllCars()-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public class Car
        {
            public string Brand { get; set; }
            public string Model { get; set; }
            public string Bodytype { get; set; }
            public int Yearofproduction { get; set; }
            public string VINNumber { get; set; }
            public string Color { get; set; }
            public string Transmission { get; set; }
            public string Fuel { get; set; }
            public int Mileage { get; set; }
            public int Price { get; set; }
            public string Historicalservicedata { get; set; }
            public string Dateofregistration { get; set; }
            public string Status { get; set; }
            public string Dateofsale { get; set; }
            public string Saleprice { get; set; }
            public string Profit { get; set; }
 

        }

        [WebMethod(Description = "Show all Cars in Database")]
        public List<Car> ShowAllCars()
        {
            XDocument xmlDoc = XDocument.Load(FileName);

            List<Car> cars = xmlDoc.Descendants("Car")
                                   .Select(car => new Car
                                   {
                                       
                                       Brand = (string)car.Element("Brand"),
                                       Model = (string)car.Element("Model"),
                                       Bodytype = (string)car.Element("Bodytype"),
                                       Yearofproduction = (int)car.Element("Yearofproduction"),
                                       VINNumber = (string)car.Element("VINNumber"),
                                       Color = (string)car.Element("Color"),
                                       Transmission = (string)car.Element("Transmission"),
                                       Fuel = (string)car.Element("Fuel"),
                                       Mileage = (int)car.Element("Mileage"),
                                       Price = (int)car.Element("Price"),
                                       Historicalservicedata = (string)car.Element("Historicalservicedata"),
                                       Dateofregistration = (string)car.Element("Dateofregistration"),
                                       Status = (string)car.Element("Status"),
                                       Dateofsale = (string)car.Element("Dateofsale") != null ? (string)car.Element("Dateofsale") : "",
                                       Saleprice = (string)car.Element("Saleprice") != null ? (string)car.Element("Saleprice") : "",
                                       Profit = (string)car.Element("Profit") != null ? (string)car.Element("Profit") : "",
                                      

                                   }).ToList();;

            return cars;
        }

        //TODO--------------------------------------------------------------------------GetCarByVINNumber()-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public class Car1
        {
            //public int Id { get; set; }
            public string Brand { get; set; }
            public string Model { get; set; }
            public string Bodytype { get; set; }
            public int Yearofproduction { get; set; }
            public string VINNumber { get; set; }
            public string Color { get; set; }
            public string Transmission { get; set; }
            public string Fuel { get; set; }
            public float Mileage { get; set; }
            public float Price { get; set; }
            public string Historicalservicedata { get; set; }
            public string Dateofregistration { get; set; }
            public string Status { get; set; }
            public string Dateofsale { get; set; }
            public string Saleprice { get; set; }
            public string Profit { get; set; }
        }

        [WebMethod]
        public Car1 GetCarByVINNumber(string VINNumber)
        {
          
            // načítanie existujúceho XML dokumentu
            XDocument xmlDoc = XDocument.Load(FileName);

            // vyhľadanie prvku podľa VINNumber
            XElement carElement = xmlDoc.Descendants("Car").FirstOrDefault(x => (string)x.Element("VINNumber") == VINNumber);

            if (carElement != null)
            {
                // prevod najdeneho prvku na objekt typu Car
                Car1 car = new Car1
                {
                    Brand = (string)carElement.Element("Brand"),
                    Model = (string)carElement.Element("Model"),
                    Bodytype = (string)carElement.Element("Bodytype"),
                    Yearofproduction = (int)carElement.Element("Yearofproduction"),
                    VINNumber = (string)carElement.Element("VINNumber"),
                    Color = (string)carElement.Element("Color"),
                    Transmission = (string)carElement.Element("Transmission"),
                    Fuel = (string)carElement.Element("Fuel"),
                    Mileage = (float)carElement.Element("Mileage"),
                    Price = (float)carElement.Element("Price"),
                    Historicalservicedata = (string)carElement.Element("Historicalservicedata"),
                    Dateofregistration = (string)carElement.Element("Dateofregistration"),
                    Status = (string)carElement.Element("Status"),
                    Dateofsale = (string)carElement.Element("Dateofsale") != null ? (string)carElement.Element("Dateofsale") : "",
                    Saleprice = (string)carElement.Element("Saleprice") != null ? (string)carElement.Element("Saleprice") : "",
                    Profit = (string)carElement.Element("Profit") != null ? (string)carElement.Element("Profit") : "",
                };
                return car;
            }
            else
            {
                return null;
            }
        }





        //TODO---------------------------------------------------------------------------UpdateCarSale()----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        
        [WebMethod(Description = "Update Car by Vin number")]
        public void UpdateCarSale(string vinNumber, DateTime dateOfSale, float salePrice)
        {
            XDocument xmlDoc = XDocument.Load(FileName);
            XElement carElement = xmlDoc.Descendants("Car").FirstOrDefault(x => (string)x.Element("VINNumber") == vinNumber);


            if (carElement != null)
            {
                if (carElement.Element("Dateofsale") == null) // check if Dateofsale element exists
                {
                    carElement.Add(new XElement("Dateofsale", dateOfSale.ToString("dd.MM.yyyy"))); // add Dateofsale element
                }
                else
                {
                    carElement.Element("Dateofsale").SetValue(dateOfSale.ToString("dd.MM.yyyy")); // update existing Dateofsale element
                }

                if (carElement.Element("Saleprice") == null) // check if Saleprice element exists
                {
                    carElement.Add(new XElement("Saleprice", salePrice)); // add Saleprice element
                }
                else
                {
                    carElement.Element("Saleprice").SetValue(salePrice); // update existing Saleprice element
                }

                if ((string)carElement.Element("Status") == "Unsold") // check if current status is Unsold
                {
                    carElement.Element("Status").SetValue("Sold"); // update the status to Sold
                }

                xmlDoc.Save(FileName);
            }
          
        }

        //TODO---------------------------------------------------------------------------deleteCarByVin()-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
      
        [WebMethod(Description = "Delete Car by Vin number")]
        public void DeleteCarByVin(string VINNumber)
        {
            XDocument doc = XDocument.Load(FileName);
            //Vyhľadávanie elementu s atribútom "vin" rovným hodnote parametra vin
            XElement car = doc.Root.Elements("Car").FirstOrDefault(x => x.Element("VINNumber").Value == VINNumber);

            if (car != null)
            {

                //Vymazanie auta z XML dokumentu
                car.Remove();
                //Uloženie zmien do XML dokumentu
                doc.Save(FileName);
             
             
            }
        }

        //TODO---------------------------------------------------------------------------GetTotalCarsSoldPerYear()-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        [WebMethod(Description = "Total number of cars sold per year for 2021, 2022, 2023")]
        public int[] GetTotalCarsSoldPerYear()
        {
            int[] years = { 2021, 2022, 2023 };
            int[] totalCarsSoldPerYear = new int[years.Length];

            XDocument doc = XDocument.Load(FileName);

            foreach (var car in doc.Descendants("Car"))
            {
                var dateOfSale = car.Element("Dateofsale")?.Value; // ? operator overenia, či element existuje

                if (!string.IsNullOrEmpty(dateOfSale))
                {
                    DateTime saleDate;
                    if (DateTime.TryParse(dateOfSale, out saleDate)) // Skúsiť skonvertovať reťazec na dátum
                    {
                        int yearIndex = Array.IndexOf(years, saleDate.Year);
                        if (yearIndex >= 0)
                        {
                            totalCarsSoldPerYear[yearIndex]++;
                        }
                    }
                    else
                    {
                        // Ak dátum nie je platný, ignorovať tento záznam
                        continue;
                    }
                }
            }

            return totalCarsSoldPerYear;
        }

        //TODO------------------------------------------------------------GetTotalCarsCount()-------------------------------------------------//


        [WebMethod(Description = "Total number of cars per year for 2021, 2022, 2023")]
        public int[] GetTotalCarsCount()
        {
            int[] years = { 2021, 2022, 2023 };
            int[] totalCarsSoldPerYear = new int[years.Length];

            XDocument doc = XDocument.Load(FileName);

            foreach (var car in doc.Descendants("Car"))
            {
                var dateOfRegistration = car.Element("Dateofregistration")?.Value; //  operator overenia, či element existuje

                if (!string.IsNullOrEmpty(dateOfRegistration))
                {
                    DateTime registrationDate;
                    if (DateTime.TryParse(dateOfRegistration, out registrationDate)) // Skúsiť skonvertovať reťazec na dátum
                    {
                        int yearIndex = Array.IndexOf(years, registrationDate.Year);
                        if (yearIndex >= 0)
                        {
                            totalCarsSoldPerYear[yearIndex]++;
                        }
                    }
                    else
                    {
                        // Ak dátum nie je platný, ignorovať tento záznam
                        continue;
                    }
                }
            }

            return totalCarsSoldPerYear;
        }





        //TODO---------------------------------------------------------------------------SearchCarsByCategory()-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public class Car4
        {
            public string Brand { get; set; }
            public string Model { get; set; }
            public string Bodytype { get; set; }
            public int Yearofproduction { get; set; }
            public string VINNumber { get; set; }
            public string Color { get; set; }
            public string Transmission { get; set; }
            public string Fuel { get; set; }
            public string Mileage { get; set; }
            public float Price { get; set; }
            public string Historicalservicedata { get; set; }
            public string Dateofregistration { get; set; }
            public string Status { get; set; }
            public string Dateofsale { get; set; }
            public string Saleprice { get; set; }
            public string Profit { get; set; }
        }

        [WebMethod]
        public List<Car4> SearchCarsByCategory(string bodytype, string color, string transmission, string fuel, float minPrice, float maxPrice, string status)
        {
            XDocument xmlDoc = XDocument.Load(FileName);

            List<Car4> cars = xmlDoc.Descendants("Car")
            .Where(x =>
                           (string.IsNullOrEmpty(bodytype) || (string)x.Element("Bodytype") == bodytype) &&
                            (string.IsNullOrEmpty(color) || (string)x.Element("Color") == color) &&
                            (string.IsNullOrEmpty(transmission) || (string)x.Element("Transmission") == transmission) &&
                            (string.IsNullOrEmpty(fuel) || (string)x.Element("Fuel") == fuel) &&
                           ((float)x.Element("Price") >= minPrice && (float)x.Element("Price") <= maxPrice) &&
                            (string.IsNullOrEmpty(status) || (string)x.Element("Status") == status))

                .Select(x => new Car4
                {
                    Brand = (string)x.Element("Brand"),
                    Model = (string)x.Element("Model"),
                    Bodytype = (string)x.Element("Bodytype"),
                    Yearofproduction = (int)x.Element("Yearofproduction"),
                    VINNumber = (string)x.Element("VINNumber"),
                    Color = (string)x.Element("Color"),
                    Transmission = (string)x.Element("Transmission"),
                    Fuel = (string)x.Element("Fuel"),
                    Mileage = (string)x.Element("Mileage"),
                    Price = (float)x.Element("Price"),
                    Historicalservicedata = (string)x.Element("Historicalservicedata"),
                    Dateofregistration = (string)x.Element("Dateofregistration"),
                    Status = (string)x.Element("Status"),
                    Dateofsale = (string)x.Element("Dateofsale") ?? "",
                    Saleprice = (string)x.Element("Saleprice") ?? "",
                    Profit = (string)x.Element("Profit") ?? "",
                })
                .ToList();

            return cars;
        }




        //TODO---------------------------------------------------------------------------GetSoldAndUnsoldCars()-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//


        [WebMethod(Description = "Get number of sold and unsold cars.")]
        public int[] GetSoldAndUnsoldCars()
        {
            XDocument doc = XDocument.Load(FileName);

            int soldCount = doc.Descendants("Car").Count(car => (string)car.Element("Status") == "Sold");
            int unsoldCount = doc.Descendants("Car").Count(car => (string)car.Element("Status") == "Unsold");
            int totalCount = soldCount + unsoldCount;

            return new int[] { soldCount, unsoldCount, totalCount };
        }

        //TODO---------------------------------------------------------------------------SaveUnsoldCars-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        [WebMethod(Description = "Save unsold cars to an XML file")]
        public void SaveUnsoldCars()
        {
            XDocument doc = XDocument.Load(FileName);

            var unsoldCars = doc.Descendants("Car")
                .Where(car => (string)car.Element("Status") == "Unsold");

            // Vytvorenie nového súboru na konkrétne miesto
            string filePath = @"D:\OneDrive - University of Economics - student\Škola\Piaty ročník FHI\Diplomovka\WebTest1\WebTest1\unsold_cars.xml";
            FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write);

            XmlWriter writer = XmlWriter.Create(file);

            writer.WriteStartElement("Cars");

            foreach (var car in unsoldCars)
            {
                writer.WriteStartElement("Car");

                writer.WriteElementString("Brand", car.Element("Brand").Value);
                writer.WriteElementString("Model", car.Element("Model").Value);
                writer.WriteElementString("Bodytype", car.Element("Bodytype").Value );
                writer.WriteElementString("Yearofproduction", car.Element("Yearofproduction").Value);
                writer.WriteElementString("VINNumber", car.Element("VINNumber").Value);
                writer.WriteElementString("Color", car.Element("Color").Value );
                writer.WriteElementString("Transmission", car.Element("Transmission").Value);
                writer.WriteElementString("Fuel", car.Element("Fuel").Value);
                writer.WriteElementString("Mileage", car.Element("Mileage").Value);
                writer.WriteElementString("Price", car.Element("Price").Value + "\n");
                writer.WriteElementString("Historicalservicedata", car.Element("Historicalservicedata").Value);
                writer.WriteElementString("Dateofregistration", car.Element("Dateofregistration").Value);
                writer.WriteElementString("Status", car.Element("Status").Value);

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.Close();
        }

        //TODO---------------------------------------------------------------------------CalculationOfProfit()-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        [WebMethod(Description = "Calculation of the profit of sold cars")]
        public double CalculationOfProfit()
        {
            XDocument xmlDoc = XDocument.Load(FileName);
            double allProfit = 0.0; 
            foreach (XElement car in xmlDoc.Descendants("Car"))
            {
                if ((string)car.Element("Status") == "Sold")
                {
                    double price = (double)car.Element("Price");
                    double saleprice = (double)car.Element("Saleprice");

                    if (car.Element("Saleprice") != null)
                    {
                        double profit = saleprice - price;

                        XElement profitElement = car.Element("Profit");
                        if (profitElement != null)
                        {
                            profitElement.SetValue(profit);
                        }
                        else
                        {
                            car.Add(new XElement("Profit", profit));
                        }

                        allProfit += profit; 
                    }
                }
            }

            xmlDoc.Save(FileName);
            return allProfit;
        }

        //TODO---------------------------------------------------------------------------CalculationProfit()-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        [WebMethod(Description = "Calculate profit for 2021, 2022, 2023")]
        public int[] CalculateProfit()
        {
            int[] years = { 2021, 2022, 2023 };
            int[] profits = new int[years.Length];

            XDocument doc = XDocument.Load(FileName);

            foreach (var car in doc.Descendants("Car"))
            {
                var profitElement = car.Element("Profit")?.Value;

                if (!string.IsNullOrEmpty(profitElement))
                {
                    int profit;
                    if (int.TryParse(profitElement, out profit))
                    {
                        DateTime saleDate;
                        if (DateTime.TryParse(car.Element("Dateofsale")?.Value, out saleDate))
                        {
                            int yearIndex = Array.IndexOf(years, saleDate.Year);
                            if (yearIndex >= 0)
                            {
                                profits[yearIndex] += profit;
                            }
                        }
                    }
                }
            }

            return profits;
        }


        //TODO---------------------------------------------------------------------------GetCountByBodytype()-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        [WebMethod(Description = "Get car sales by bodytype for years 2021, 2022, 2023")]
        public int[] GetCountByBodytype(string bodytype)
        {
            int[] years = { 2021, 2022, 2023 };
            int[] carSalesByYear = new int[years.Length];
            int[] totalCarsByYear = new int[years.Length];

            XDocument doc = XDocument.Load(FileName);

            foreach (var car in doc.Descendants("Car"))
            {
                var dateOfSale = car.Element("Dateofsale")?.Value;
                var dateOfReg = car.Element("Dateofregistration")?.Value;
                var carBodytype = car.Element("Bodytype")?.Value;

                if (carBodytype != null && carBodytype.ToLower() == bodytype.ToLower())
                {
                    if (!string.IsNullOrEmpty(dateOfSale))
                    {
                        DateTime saleDate;
                        if (DateTime.TryParse(dateOfSale, out saleDate))
                        {
                            if (saleDate.Year >= years[0] && saleDate.Year <= years[years.Length - 1])
                            {
                                for (int i = 0; i < years.Length; i++)
                                {
                                    if (saleDate.Year == years[i])
                                    {
                                        carSalesByYear[i]++;
                                    }
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(dateOfReg))
                    {
                        DateTime regDate;
                        if (DateTime.TryParse(dateOfReg, out regDate))
                        {
                            if (regDate.Year >= years[0] && regDate.Year <= years[years.Length - 1])
                            {
                                for (int i = 0; i < years.Length; i++)
                                {
                                    if (regDate.Year == years[i])
                                    {
                                        totalCarsByYear[i]++;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            int[] result = new int[years.Length * 2];
            for (int i = 0; i < years.Length; i++)
            {
                result[i * 2] = carSalesByYear[i];
                result[i * 2 + 1] = totalCarsByYear[i];
            }

            return result;
        }

        //TODO---------------------------------------------------------------------------GetCountByBrand()-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        [WebMethod(Description = "Get car sales by brand for years 2021, 2022, 2023")]
        public int[] GetCountByBrand(string brand)
        {
            int[] years = { 2021, 2022, 2023 };
            int[] carSalesByYear = new int[years.Length];
            int[] totalCarsByYear = new int[years.Length];

            XDocument doc = XDocument.Load(FileName);

            foreach (var car in doc.Descendants("Car"))
            {
                var dateOfSale = car.Element("Dateofsale")?.Value;
                var dateOfReg = car.Element("Dateofregistration")?.Value;
                var carBrand = car.Element("Brand")?.Value;

                if (carBrand != null && carBrand.ToLower() == brand.ToLower())
                {
                    if (!string.IsNullOrEmpty(dateOfSale))
                    {
                        DateTime saleDate;
                        if (DateTime.TryParse(dateOfSale, out saleDate))
                        {
                            if (saleDate.Year >= years[0] && saleDate.Year <= years[years.Length - 1])
                            {
                                for (int i = 0; i < years.Length; i++)
                                {
                                    if (saleDate.Year == years[i])
                                    {
                                        carSalesByYear[i]++;
                                    }
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(dateOfReg))
                    {
                        DateTime regDate;
                        if (DateTime.TryParse(dateOfReg, out regDate))
                        {
                            if (regDate.Year >= years[0] && regDate.Year <= years[years.Length - 1])
                            {
                                for (int i = 0; i < years.Length; i++)
                                {
                                    if (regDate.Year == years[i])
                                    {
                                        totalCarsByYear[i]++;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            int[] result = new int[years.Length * 2];
            for (int i = 0; i < years.Length; i++)
            {
                result[i * 2] = carSalesByYear[i];
                result[i * 2 + 1] = totalCarsByYear[i];
            }

            return result;
        }

    }


}

