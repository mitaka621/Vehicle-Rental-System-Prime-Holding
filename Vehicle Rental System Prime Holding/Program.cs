using Vehicle_Rental_System_Prime_Holding.Core;
using Vehicle_Rental_System_Prime_Holding.Core.Contracts;

IInvoiceController dealership=new InvoiceController(false);

dealership.AddVehicle("Car","QWERTY","Mitsubishi","Mirage",15000,3);
dealership.AddVehicle("Motorcycle", "YTREEWQ", "Triumph", "Tiger Sport 660", 10000);

dealership.RegisterClient("John", "Doe");
dealership.RegisterClient("Mary", "Johnson",age: 20);

dealership.RentACar("QWERTY", 1, 10, DateOnly.FromDateTime(DateTime.Now.AddDays(-10)));
dealership.RentAMotorcycle("YTREEWQ", 2, 10, DateOnly.FromDateTime(DateTime.Now.AddDays(-10)));

dealership.ReturnVehicleAndPrintInvoice("QWERTY", 1);
dealership.ReturnVehicleAndPrintInvoice("YTREEWQ", 2);


