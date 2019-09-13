using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using Ddd.Infrastructure;


namespace Ddd.Taxi.Domain
{
	// In real aplication it whould be the place where database is used to find driver by its Id.
	// But in this exercise it is just a mock to simulate database
	public class DriversRepository
	{
		public void FillDriverToOrder(int driverId, TaxiOrder order)
		{
			if (driverId == 15)
			{
                order.AssignDriver(
                    new Driver(driverId,
                    new PersonName("Drive", "Driverson"),
                    new Car("Baklazhan", "Lada sedan", "A123BT 66")));
            }
			else
				throw new Exception("Unknown driver id " + driverId);
		}
	}

	public class TaxiApi : ITaxiApi<TaxiOrder>
	{
		private readonly DriversRepository driversRepo;
		private readonly Func<DateTime> currentTime;
		private int idCounter;

		public TaxiApi(DriversRepository driversRepo, Func<DateTime> currentTime)
		{
			this.driversRepo = driversRepo;
			this.currentTime = currentTime;
		}

		public TaxiOrder CreateOrderWithoutDestination(string firstName, string lastName, string street, string building)
		{
		    return
		        new TaxiOrder(idCounter++,
		            new PersonName(firstName, lastName),
		            new Address(street, building),
		            currentTime());
		}

		public void UpdateDestination(TaxiOrder order, string street, string building)
		{
		    order.UpdateDestination(new Address(street, building));
		}

		public void AssignDriver(TaxiOrder order, int driverId)
		{
            order.AssignDriver(new Driver(driverId,
                new PersonName("Drive", "Driverson"),
                new Car("Baklazhan", "Lada sedan", "A123BT 66")));
            order.SetCurrentTime(currentTime()); 
		}

		public void UnassignDriver(TaxiOrder order)
		{
			order.UnassignDriver();
		}

		public string GetDriverFullInfo(TaxiOrder order)
		{
			if (order.Status == TaxiOrderStatus.WaitingForDriver) return null;
		    return order.GetDriverFullInfo();
		}

		public string GetShortOrderInfo(TaxiOrder order)
		{
		    return order.GetShortOrderInfo();
		}

		public void Cancel(TaxiOrder order)
		{
			order.Cancel();
		    order.SetCurrentTime(currentTime());
        }

		public void StartRide(TaxiOrder order)
		{
			order.StartRide();
		    order.SetCurrentTime(currentTime());
        }

		public void FinishRide(TaxiOrder order)
		{
            order.FinishRide();
		    order.SetCurrentTime(currentTime());
        }
	}

	public class TaxiOrder : Entity<int>
	{
	    public TaxiOrder(int id, PersonName clientName, Address start, DateTime creationTime): base(id)
	    {
	        ClientName = clientName;
	        Start = start;
	        CreationTime = creationTime;
	    }

	    public int Id { get; }
		public PersonName ClientName { get; }
        public Address Start { get; }
        public Address Destination { get; private set; }
        public Driver Driver { get; private set; }
        public TaxiOrderStatus Status { get; private set; }
        public DateTime CreationTime { get; private set; }
        public DateTime DriverAssignmentTime { get; private set; }
        public DateTime CancelTime { get; private set; }
        public DateTime StartRideTime { get; private set; }
        public DateTime FinishRideTime { get; private set; }

	    public void AssignDriver(Driver driver)
	    {
	        if (Driver != null)
	        {
	            throw new InvalidOperationException("WaitingForDriver");
	        }

            this.Driver = driver;
	        Status = TaxiOrderStatus.WaitingCarArrival;
	    }

	    public void UnassignDriver()
	    {
	        if (Status == TaxiOrderStatus.InProgress )
	        {
	            throw new InvalidOperationException();
	        }

            if (Driver == null)
	        {
	            throw new InvalidOperationException("WaitingForDriver");
	        }

	        Driver = null;
	        Status = TaxiOrderStatus.WaitingForDriver;
	    }

        public void UpdateDestination(Address address)
	    {
	        Destination = address;
	    }

	    public string GetDriverFullInfo()
	    {
	        if (Status == TaxiOrderStatus.WaitingForDriver) return null;
	        return string.Join(" ",
	            "Id: " + Driver.Id,
	            "DriverName: " + FormatName(Driver.PersonName.FirstName, Driver.PersonName.LastName),
	            "Color: " + Driver.Car.CarColor,
	            "CarModel: " + Driver.Car.CarModel,
	            "PlateNumber: " + Driver.Car.CarPlateNumber);
	    }

        public string GetShortOrderInfo()
        {
            string destAddress = null;
            string driver = null;
            destAddress = Destination == null ? "" : FormatAddress(Destination.Street, Destination.Building);
            driver = Driver == null ? "" : FormatName(Driver.PersonName.FirstName, Driver.PersonName.LastName);
            return string.Join(" ",
                "OrderId: " + Id,
                "Status: " + Status,
                "Client: " + FormatName(ClientName.FirstName, ClientName.LastName),
                "Driver: " + driver,
                "From: " + FormatAddress(Start.Street, Start.Building),
                "To: " + destAddress,
                "LastProgressTime: " + GetLastProgressTime()
                    .ToString("yyyy-MM-dd HH:mm:ss", 
                        CultureInfo.InvariantCulture));
        }

        private string FormatName(string firstName, string lastName)
	    {
	        return string.Join(" ", new[] { firstName, lastName }.Where(n => n != null));
	    }

	    private string FormatAddress(string street, string building)
	    {
	        return string.Join(" ", new[] { street, building }.Where(n => n != null));
	    }

	    private DateTime GetLastProgressTime()
	    {
	        if (Status == TaxiOrderStatus.WaitingForDriver) return CreationTime;
	        if (Status == TaxiOrderStatus.WaitingCarArrival) return DriverAssignmentTime;
	        if (Status == TaxiOrderStatus.InProgress) return StartRideTime;
	        if (Status == TaxiOrderStatus.Finished) return FinishRideTime;
	        if (Status == TaxiOrderStatus.Canceled) return CancelTime;
	        throw new NotSupportedException(Status.ToString());
	    }

	    public void Cancel()
	    {
	        if (Status == TaxiOrderStatus.InProgress)
	        {
	            throw new InvalidOperationException();
	        }
	        Status = TaxiOrderStatus.Canceled;
	    }

	    public void StartRide()
	    {
	        if (Status == TaxiOrderStatus.WaitingForDriver)
	        {
	            throw new InvalidOperationException();
	        }
            Status = TaxiOrderStatus.InProgress;
	    }

	    public void FinishRide()
	    {
	        if (Status == TaxiOrderStatus.WaitingCarArrival ||
	            Status == TaxiOrderStatus.WaitingForDriver)
	        {
	            throw new InvalidOperationException();
	        }
            Status = TaxiOrderStatus.Finished;
	    }

        public void SetCurrentTime(DateTime time)
	    {
	        switch (Status)
	        {
                case TaxiOrderStatus.Canceled:
                    CancelTime = time;
                    break;
                case TaxiOrderStatus.Finished:
                    FinishRideTime = time;
                    break;
                case TaxiOrderStatus.InProgress:
                    StartRideTime = time;
                    break;
                case TaxiOrderStatus.WaitingCarArrival:
                    DriverAssignmentTime = time;
                    break;
                case TaxiOrderStatus.WaitingForDriver:
                    CreationTime = time;
                    break;
            }
        }
    }

    public class Car : ValueType<Car>
    {
        public Car(string carColor, string carModel, string carPlateNumber)
        {
            CarColor = carColor;
            CarModel = carModel;
            CarPlateNumber = carPlateNumber;
        }

        public string CarColor { get; }
        public string CarModel { get; }
        public string CarPlateNumber { get; }
    }

    public class Driver : Entity<Int32>
    {
        public Driver(int id, PersonName personName, Car car):base(id)
        {
            Id = id;
            PersonName = personName;
            Car = car;
        }

        public int Id { get; }
        public PersonName PersonName { get; }
        public Car Car { get; }
    }
}