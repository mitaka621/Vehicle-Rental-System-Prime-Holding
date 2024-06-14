<h1>Classes Structure Overview</h1>
<h2>Vehicle</h2>
<p>This is a base class for the vehicle which contains the vehicle's licence plate (for later identification), brand, model, vehicle value, rental period in days, actual rental period (when the vehicle is returned), start date of the reservation, end date of the reservation, actual return date and is rented bool property. This class is abstract - it cannot be instantiated and every class representing a new vehicle type has to inherit it.</p>
<p>Implemented methods:</p>
<ul>
  <li><b>double GetRentalCost()</b> - Using reflection, depending on the class type and the rented period a certain rental cost is returned.</li>
  <li><b>abstract double GetInsuranceCost()</b> - Abstract method to be implemented in all of the child classes to return initial insurance.</li>
  <li><b>abstract double GetInsuranceCostChanges()</b> - Abstract method to be implemented in all of the child classes to return the amout by which the insurance has changed base on a set of criteria.</li>
  <li><b>virtual bool RentVehicle(int rentalPeriodInDays, DateOnly reservationStartDate, int? optParam = null)</b> - sets the rental period, when the reservation starts and includes an optional parameter for vehicles which require additional data (it can be overridden).</li>
  <li><b>bool ReturnVehicle()</b> - This method is called when the client returns the car and with it several parameters are set.</li>
  <li><b>override string ToString()</b> - The overridden ToString() method will be used to print the invoice for the current vehicle to the CLI.</li>
</ul>
<h2>Car</h2>
<p>Class inheriting Vehicle which implements a new property - SafetyRating. This rating is supplied in the constructor along with all other requered vehicle properties.</p>
<p>Methods:</p>
<ul>
  <li><b>override double GetInsuranceCost()</b> - returns the calculated initial insurance based on the vehicle value.</li>
  <li><b>override double GetInsuranceCostChanges()</b> - returns the value with which the initial insurance will be changed (could be negative) based on a criteria.</li>
</ul>
<h2>CargoVan</h2>
<p>Class inheriting Vehicle which implements a new property - DriverExperience. This property is supplied when the RentVehicle() ovveriden method is called.</p>
<p>Methods:</p>
<ul>
  <li><b>override bool RentVehicle(int rentalPeriodInDays, DateOnly reservationStartDate, int? optParam = null)</b> - sets the RentalPeriodInDays, ReservationStartDate and DriverExperience properties.</li>
  <li><b>override double GetInsuranceCost()</b> - returns the calculated initial insurance based on the vehicle value.</li>
  <li><b>override double GetInsuranceCostChanges()</b> - returns the value with which the initial insurance will be changed to (could be negative) based on a criteria.</li>
</ul>
<h2>Motorcycle</h2>
<p>Class inheriting Vehicle which implements a new Age - DriverExperience. This property is supplied when the RentVehicle() ovveriden method is called.</p>
<p>Methods:</p>
<ul>
  <li><b>override bool RentVehicle(int rentalPeriodInDays, DateOnly reservationStartDate, int? optParam = null)</b> - sets the RentalPeriodInDays, ReservationStartDate and Age properties.</li>
  <li><b>override double GetInsuranceCost()</b> - returns the calculated initial insurance based on the vehicle value.</li>
  <li><b>override double GetInsuranceCostChanges()</b> - returns the value with which the initial insurance will be changed to (could be negative) based on a criteria.</li>
</ul>
<h2>Client</h2>
<p>This class represents a car dealership client. It contains the client's first name, last name, age (optional), experience (optional). It also contains a collection of vehicles which will represent the vehicles that the client has rented (one client will be able to rent multiple vehicles - one to many relation).</p>
<p>Methods:</p>
<ul>
  <li><b>void AddCarToColection(IVehicle model)</b> - Since the property "Vehicles" will provide a readonly collection this method will be used to add a new car that the client has rented to the vehicles list.</li>
  <li><b>override string ToString()</b> - The ToString() method will be used to print the invoices for all of the client's rented vehicles</li>
</ul>
<h2>Repositories</h2>
<h3>ClientsRepository</h3>
<p>This class will be managing the car dealership clients.</p>
<h3>VehicleRepository</h3>
<p>This class will be managing the available cars in the dealership.</p>
<h2>InvoiceController</h3>
<p>This is the main class the car dealership administrator will be using since in the task description it is explicitly stated that "Input values should be part of the code". This is why a CLI menu is not implemented.</p>
<p>The constructor has one optional bool parameter - EnableHelperMessages. When toggled, helper messages will appear in the CLI when certain actions are executed.</p>
<p>Methods:</p>
<ul>
  <li><b>void RegisterClient(string FirstName, string LastName, int? age = null, int? experience=null)</b> - Registers a new client to the dealership. The client repository assignes a unique id to the customer starting from 1.</li>
  <li><b>void AddVehicle(string typeName,string vehicleLicensePlate,string brand,string model,double vehicleValue,int? safetyRating = null)</b> - Registers a new client to the dealership. The "typeName" can be Car, CargoVan or Motorcycle. If the vehicle is of type car a safety rating has to be provided.</li>
  <li><b>void RentACar(string vehicleLicensePlate, int userId, int rentPeriod, DateOnly startDate)</b> - Rents a car to a user with the provided userId and car license plate</li>
  <li><b>void RentAVan(string vehicleLicensePlate, int userId, int rentPeriod, DateOnly startDate)</b> - Rents a cargo van to a user with the provided userId and car license plate. The user is required to have driver experience entered when the account was created.</li>
  <li><b>void RentAMotorcycle(string vehicleLicensePlate, int userId, int rentPeriod, DateOnly startDate)</b> - Rents a motorcycle to a user with the provided userId and car license plate. The user is required to have age entered when the account was created.</li>
  <li><b>void ReturnVehicleAndPrintInvoice(string numberPlate, int clientId)</b> - Returns the vehicle by the given license plate and the userId. After that an invoice is printed on the CLI.</li>
  <li><b>void PrintInvoicesForAllClients()</b> - Prints Invoices for all clients registered to the dealership.</li>
</ul>

<p><ins>Note - all of the classes listed implement an appropriate interface</ins>></p>
