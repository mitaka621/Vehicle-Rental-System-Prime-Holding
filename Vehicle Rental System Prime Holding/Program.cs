using Vehicle_Rental_System_Prime_Holding.Core;
using Vehicle_Rental_System_Prime_Holding.Models.Contracts;
using Vehicle_Rental_System_Prime_Holding.Models.Vehicles;

InvoiceController dealrship=new InvoiceController();

dealrship.AddVehicle("CargoVan", "asdd", "ferary", "model 3", 10000);

dealrship.RegisterClient("pesho", "petrov",experience: 10);

dealrship.RentAVan("asdd", 1, 10, DateOnly.FromDateTime(DateTime.Now.AddDays(-10)));

dealrship.ReturnVehicleAndPrintInvoice("asdd", 1);

