<h1>Classes - Structure Overview</h1>
<h2>Vehicle</h2>
<p>This is a base class for the vehicle which contains the vehicle's license plate (for later identification), brand, model, vehicle value, rental period in days, actual rental period (when the vehicle is returned), start date of the reservation, end date of the reservation, actual return date and  isRented bool property. This class is abstract - it cannot be instantiated and every class representing a new vehicle type has to inherit it.</p>
<p>Implemented methods:</p>
<ul>
  <li><b>double GetRentalCost()</b> - Using reflection, depending on the class type and the rented period a certain rental cost is returned.</li>
  <li><b>abstract double GetInsuranceCost()</b> - Abstract method to be implemented in all of the child classes to return initial insurance.</li>
  <li><b>abstract double GetInsuranceCostChanges()</b> - Abstract method to be implemented in all of the child classes to return the amount by which the insurance cost will be changed based on a criteria.</li>
  <li><b>virtual bool RentVehicle(int rentalPeriodInDays, DateOnly reservationStartDate, int? optParam = null)</b> - sets the rental period, when the reservation starts and includes an optional parameter for vehicles which require additional data (it can be overridden).</li>
  <li><b>bool ReturnVehicle()</b> - This method is called when the client returns the car and with it several properties are set.</li>
  <li><b>override string ToString()</b> - The overridden ToString() method will be used to print the invoice for the current vehicle to the CLI.</li>
</ul>
<h2>Car</h2>
<p>Class inheriting Vehicle which implements a new property - SafetyRating. This rating is supplied in the constructor along with all other required vehicle properties.</p>
<p>Methods:</p>
<ul>
  <li><b>override double GetInsuranceCost()</b> - returns the calculated initial insurance based on the vehicle value.</li>
  <li><b>override double GetInsuranceCostChanges()</b> - returns the value with which the initial insurance will be changed to, (could be negative) based on a criteria.</li>
</ul>
<h2>CargoVan</h2>
<p>Class inheriting Vehicle which implements a new property - DriverExperience. This property is supplied when the RentVehicle() overridden method is called.</p>
<p>Methods:</p>
<ul>
  <li><b>override bool RentVehicle(int rentalPeriodInDays, DateOnly reservationStartDate, int? optParam = null)</b> - sets the RentalPeriodInDays, ReservationStartDate and DriverExperience properties.</li>
  <li><b>override double GetInsuranceCost()</b> - returns the calculated initial insurance based on the vehicle value.</li>
  <li><b>override double GetInsuranceCostChanges()</b> - returns the value with which the initial insurance will be changed to, (could be negative) based on a criteria.</li>
</ul>
<h2>Motorcycle</h2>
<p>Class inheriting Vehicle which implements a new property - Age. This property is supplied when the RentVehicle() overridden method is called.</p>
<p>Methods:</p>
<ul>
  <li><b>override bool RentVehicle(int rentalPeriodInDays, DateOnly reservationStartDate, int? optParam = null)</b> - sets the RentalPeriodInDays, ReservationStartDate and Age properties.</li>
  <li><b>override double GetInsuranceCost()</b> - returns the calculated initial insurance based on the vehicle value.</li>
  <li><b>override double GetInsuranceCostChanges()</b> - returns the value with which the initial insurance will be changed to, (could be negative) based on a criteria.</li>
</ul>
<h2>Client</h2>
<p>This class represents a car dealership client. It contains the client's first name, last name, age (optional), experience (optional). It also contains a collection of vehicles which will represent the vehicles that the client has rented (one client will be able to rent multiple vehicles - one to many relation).</p>
<p>Methods:</p>
<ul>
  <li><b>void AddCarToCollection(IVehicle model)</b> - Since the property "Vehicles" will provide a readonly collection. This method will be used to add a new car, that the client has rented, to their vehicles list.</li>
  <li><b>override string ToString()</b> - The ToString() method will be used to print the invoices for all of the client's rented vehicles</li>
</ul>
<h2>Repositories</h2>
<h3>ClientsRepository</h3>
<p>This class will be managing the car dealership clients.</p>
<h3>VehicleRepository</h3>
<p>This class will be managing the available cars in the dealership.</p>
<h2>InvoiceController</h3>
<p>This is the main class the car dealership administrator will be using since in the task description it is explicitly stated that "Input values should be part of the code". This is why a CLI menu is not implemented.</p>
<p>The constructor has one optional bool parameter - EnableHelperMessages. When toggled, helper messages will appear in the CLI when certain actions are performed.</p>
<p>Methods:</p>
<ul>
  <li><b>void RegisterClient(string FirstName, string LastName, int? age = null, int? experience=null)</b> - Registers a new client to the dealership. The client repository assigns a unique id to the customer starting from 1.</li>
  <li><b>void AddVehicle(string typeName,string vehicleLicensePlate,string brand,string model,double vehicleValue,int? safetyRating = null)</b> - Registers a new client to the dealership. The "typeName" can be Car, CargoVan or Motorcycle. If the vehicle is of type car a safety rating has to be provided.</li>
  <li><b>void RentACar(string vehicleLicensePlate, int userId, int rentPeriod, DateOnly startDate)</b> - Rents a car to a user with the provided userId and car license plate</li>
  <li><b>void RentAVan(string vehicleLicensePlate, int userId, int rentPeriod, DateOnly startDate)</b> - Rents a cargo van to a user with the provided userId and car license plate. The user is required to have driver experience entered when the account was created.</li>
  <li><b>void RentAMotorcycle(string vehicleLicensePlate, int userId, int rentPeriod, DateOnly startDate)</b> - Rents a motorcycle to a user with the provided userId and car license plate. The user is required to have age entered when the account was created.</li>
  <li><b>void ReturnVehicleAndPrintInvoice(string numberPlate, int clientId)</b> - Returns the vehicle by the given license plate and the userId. After that an invoice is printed on the CLI.</li>
  <li><b>void PrintInvoicesForAllClients()</b> - Prints Invoices for all clients registered to the dealership.</li>
</ul>
<p><ins>Note - all of the classes listed implement an appropriate interface</ins></p>
<h1>Usage With Examples</h1>
<h2>Provided Example</h2>
<h3>Setup</h3>
<pre>IInvoiceController dealership=new InvoiceController(false);

dealership.AddVehicle("Car","QWERTY","Mitsubishi","Mirage",15000,3);
dealership.AddVehicle("Motorcycle", "YTREEWQ", "Triumph", "Tiger Sport 660", 10000);

dealership.RegisterClient("John", "Doe");
dealership.RegisterClient("Mary", "Johnson",age: 20);

dealership.RentACar("QWERTY", 1, 10, DateOnly.FromDateTime(DateTime.Now.AddDays(-10)));
dealership.RentAMotorcycle("YTREEWQ", 2, 10, DateOnly.FromDateTime(DateTime.Now.AddDays(-10)));

dealership.ReturnVehicleAndPrintInvoice("QWERTY", 1);
dealership.ReturnVehicleAndPrintInvoice("YTREEWQ", 2);</pre>
<h3>Output</h3>
<pre>XXXXXXXXXXXXXX
Date: 2024-06-14
Customer Name: John Doe
Rented Vehicle: Mitsubishi Mirage

Reservation start date: 2024-06-04
Reservation end date: 2024-06-14
Reserved rental days: 10

Actual Return date: 2024-06-14
Actual rental days: 10

Rental cost per day: $15.00
Insurance per day: $1.50

Total rent: $150.00
Total Insurance: $15.00
Total: $165.00
XXXXXXXXXXXXX

XXXXXXXXXXXXXX
Date: 2024-06-14
Customer Name: Mary Johnson
Rented Vehicle: Triumph Tiger Sport 660

Reservation start date: 2024-06-04
Reservation end date: 2024-06-14
Reserved rental days: 10

Actual Return date: 2024-06-14
Actual rental days: 10

Rental cost per day: $10.00
Initial insurance per day: $2.00
Insurance addition per day: $0.40
Insurance per day: $2.40

Total rent: $100.00
Total Insurance: $24.00
Total: $124.00
XXXXXXXXXXXXX</pre>
<h2>Printing Invoices for All Clients</h2>
<h3>Setup</h3>
<p>If the client has not yet returned their vehicle the initial period is used for the calculations and for the null properties "--not yet returned--" is displayed.</p>
<pre>IInvoiceController dealership=new InvoiceController();

dealership.AddVehicle("Car", "QWERTY", "Mitsubishi", "Mirage", 15000, 3);
dealership.AddVehicle("Motorcycle", "YTREEWQ", "Triumph", "Tiger Sport 660", 10000);

dealership.RegisterClient("John", "Doe");
dealership.RegisterClient("Mary", "Johnson", age: 20);
dealership.RegisterClient("Gosho", "Petrov");

dealership.RentACar("QWERTY", 1, 10, DateOnly.FromDateTime(DateTime.Now.AddDays(-10)));
dealership.RentAMotorcycle("YTREEWQ", 2, 10, DateOnly.FromDateTime(DateTime.Now.AddDays(-10)));

dealership.ReturnVehicleAndPrintInvoice("QWERTY", 1);

dealership.PrintInvoicesForAllClients();</pre>
<h3>Output</h3>
<pre>
<code>
Mitsubishi Mirage is now registered of type Car
Triumph Tiger Sport 660 is now registered of type Motorcycle
John Doe is now registered
Mary Johnson is now registered
Gosho Petrov is now registered
John Doe is renting Mitsubishi Mirage
Mary Johnson is renting Triumph Tiger Sport 660
XXXXXXXXXXXXXX
Date: 2024-06-14
Customer Name: John Doe
Rented Vehicle: Mitsubishi Mirage

Reservation start date: 2024-06-04
Reservation end date: 2024-06-14
Reserved rental days: 10

Actual Return date: 2024-06-14
Actual rental days: 10

Rental cost per day: $15.00
Insurance per day: $1.50

Total rent: $150.00
Total Insurance: $15.00
Total: $165.00
XXXXXXXXXXXXX

------------------All Users Invoices----------------------
XXXXXXXXXXXXXX
Date: 2024-06-14
Customer Name: John Doe
Rented Vehicle: Mitsubishi Mirage

Reservation start date: 2024-06-04
Reservation end date: 2024-06-14
Reserved rental days: 10

Actual Return date: 2024-06-14
Actual rental days: 10

Rental cost per day: $15.00
Insurance per day: $1.50

Total rent: $150.00
Total Insurance: $15.00
Total: $165.00
XXXXXXXXXXXXX
------------------------------
XXXXXXXXXXXXXX
Date: 2024-06-14
Customer Name: Mary Johnson
Rented Vehicle: Triumph Tiger Sport 660

Reservation start date: 2024-06-04
Reservation end date: 2024-06-14
Reserved rental days: 10

Actual Return date: --not yet returned--
Actual rental days: --not yet returned--

Rental cost per day: $10.00
Initial insurance per day: $2.00
Insurance addition per day: $0.40
Insurance per day: $2.40

Total rent: $100.00
Total Insurance: $24.00
Total: $124.00
XXXXXXXXXXXXX
------------------------------
XXXXXXXXXXXXXX
Date: 2024-06-14
Customer Name: Gosho Petrov
--No Rented Cars--
XXXXXXXXXXXXXX
------------------------------
</code>
</pre>
<h2>Handling Invalid Input</h2>
<h3>Adding vehicle of type that does not exist</h3>
<p>An exception will be thrown</p>
<pre>System.ArgumentException: The class type name you have entered has not been implemented- asd</pre>
<h2>Not providing a safety rating when adding a car.</h2>
<p>An exception will be thrown</p>
<pre>System.ArgumentNullException: Value cannot be null. (Parameter 'safetyRating')</pre>
<h2>Renting a motorcycle or a cargovan to a client who does not have age or experience</h2>
<p>An exception will be thrown</p>
<pre>System.ArgumentNullException: Value cannot be null. (Parameter 'Age')</pre>
<p>or</p>
<pre>System.ArgumentNullException: Value cannot be null. (Parameter 'Experience')</pre>
<h2>Adding a new vehicle with a license plate which already exists</h2>
<p>An exception will be thrown</p>
<pre> System.ArgumentException: A vehicle with the license plate QWERTY already exists</pre>
<h2>Entering invalid values for name, age, experience etc...</h2>
<p>An exception will be thrown</p>
<pre>System.ArgumentException: The property Age cannot be negative (less than zero)</pre>
<pre>System.ArgumentNullException: Value cannot be null. (Parameter 'The property FirstName cannot be null or empty')</pre>
