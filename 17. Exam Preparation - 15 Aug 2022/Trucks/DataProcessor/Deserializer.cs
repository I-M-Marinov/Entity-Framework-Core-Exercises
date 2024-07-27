using Newtonsoft.Json;
using Trucks.Data.Models;
using Trucks.Data.Models.Enums;
using Trucks.DataProcessor.ImportDto;
using Trucks.Utilities;

namespace Trucks.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Text.Json;
    using System.Xml;
    using Data;
    using Microsoft.EntityFrameworkCore;


    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedDespatcher
            = "Successfully imported despatcher - {0} with {1} trucks.";

        private const string SuccessfullyImportedClient
            = "Successfully imported client - {0} with {1} trucks.";

        public static string ImportDespatcher(TrucksContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRoot = "Despatchers";

            ICollection<Despatcher> dispatchersToImport = new List<Despatcher>();

            ImportDispatcherDto[] deserializedDispatchers =
                xmlHelper.Deserialize<ImportDispatcherDto[]>(xmlString, xmlRoot);

            foreach (ImportDispatcherDto dispatcherDto in deserializedDispatchers)
            {
                if (!IsValid(dispatcherDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Despatcher newDispatcher = new Despatcher()
                {
                    Name = dispatcherDto.Name,
                    Position = dispatcherDto.Position
                };

                foreach (ImportTruckDto truckDto in dispatcherDto.Trucks)
                {

                    if (!IsValid(truckDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    CategoryType categoryType;

                    if (truckDto.CategoryType == 0)
                    {
                        categoryType = CategoryType.Flatbed;
                    }
                    else if (truckDto.CategoryType == 1)
                    {
                        categoryType = CategoryType.Jumbo;
                    }
                    else if (truckDto.CategoryType == 2)
                    {
                        categoryType = CategoryType.Refrigerated;
                    }
                    else if (truckDto.CategoryType == 3)
                    {
                        categoryType = CategoryType.Semi;
                    }
                    else
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    MakeType makeType;

                    if (truckDto.MakeType == 0)
                    {
                        makeType = MakeType.Daf;
                    }
                    else if (truckDto.MakeType == 1)
                    {
                        makeType = MakeType.Man;
                    }
                    else if (truckDto.MakeType == 2)
                    {
                        makeType = MakeType.Mercedes;
                    }
                    else if (truckDto.MakeType == 3)
                    {
                        makeType = MakeType.Scania;
                    }
                    else if (truckDto.MakeType == 4)
                    {
                        makeType = MakeType.Volvo;
                    }
                    else
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Truck newTruck = new Truck()
                    {
                        RegistrationNumber = truckDto.RegistrationNumber,
                        VinNumber = truckDto.VinNumber,
                        TankCapacity = truckDto.TankCapacity,
                        CargoCapacity = truckDto.CargoCapacity,
                        CategoryType = categoryType,
                        MakeType = makeType
                    };

                    newDispatcher.Trucks.Add(newTruck);
                }

                dispatchersToImport.Add(newDispatcher);
                sb.AppendLine(string.Format(SuccessfullyImportedDespatcher, newDispatcher.Name, newDispatcher.Trucks.Count));
            }

            context.Despatchers.AddRange(dispatchersToImport);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportClient(TrucksContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            ICollection<Client> clientsToImport = new List<Client>();

            var validTruckIds = context.Trucks.Select(tr => tr.Id).ToList();

            ImportClientDto[] deserializedClients = JsonConvert.DeserializeObject<ImportClientDto[]>(jsonString)!;

            foreach (ImportClientDto clientDto in deserializedClients)
            {
                if (!IsValid(clientDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (clientDto.Type == "usual")
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Client newClient = new Client()
                {
                    Name = clientDto.Name,
                    Nationality = clientDto.Nationality,
                    Type = clientDto.Type
                };

                ICollection<ClientTruck> clientTrucksToImport = new List<ClientTruck>();

                foreach (var truckId in clientDto.Trucks.Distinct())
                {

                    if (!validTruckIds.Contains(truckId))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    ClientTruck clientTruck = new ClientTruck()
                    {
                        Client = newClient,
                        TruckId = truckId
                    };

                    clientTrucksToImport.Add(clientTruck);
                }

                newClient.ClientsTrucks = clientTrucksToImport;

                clientsToImport.Add(newClient);

                sb.AppendLine(string.Format(SuccessfullyImportedClient, newClient.Name, newClient.ClientsTrucks.Count));

            }

            context.Clients.AddRange(clientsToImport);
            context.SaveChanges();

            return sb.ToString(); 

        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}