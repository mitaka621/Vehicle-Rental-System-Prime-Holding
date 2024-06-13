using Vehicle_Rental_System_Prime_Holding.Core;
using Vehicle_Rental_System_Prime_Holding.Models.Contracts;
using Vehicle_Rental_System_Prime_Holding.Models.Vehicles;

InvoiceController dealrship=new InvoiceController();

dealrship.AddVehicle("Car", "asdd", "ferary", "model 3", 1000000, 4);

dealrship.RegisterClient("pesho", "petrov");

dealrship.RentACar("asdd", 1, 10, DateOnly.FromDateTime(DateTime.Now.AddDays(-4)));

dealrship.ReturnVehicleAndPrintInvoice("asdd", 1);

