using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using Ddd.Infrastructure.Domain;

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
				order.AssignDriver(new Driver(driverId, 
				    new PersonName( "Drive", "Driverson"), 
				    new Car("Baklazhan","Lada sedan", "A123BT 66")));
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
		    order.UpdateDestination(street, building);
		}

		public void AssignDriver(TaxiOrder order, int driverId)
		{
			driversRepo.FillDriverToOrder(driverId, order);
			order.AssignDriver(new Driver(driverId,
			    new PersonName("Drive", "Driverson"),
			    new Car("Baklazhan", "Lada sedan", "A123BT 66")));
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

		private DateTime GetLastProgressTime(TaxiOrder order)
		{
			if (order.Status == TaxiOrderStatus.WaitingForDriver) return order.CreationTime;
			if (order.Status == TaxiOrderStatus.WaitingCarArrival) return order.DriverAssignmentTime;
			if (order.Status == TaxiOrderStatus.InProgress) return order.StartRideTime;
			if (order.Status == TaxiOrderStatus.Finished) return order.FinishRideTime;
			if (order.Status == TaxiOrderStatus.Canceled) return order.CancelTime;
			throw new NotSupportedException(order.Status.ToString());
		}

		public void Cancel(TaxiOrder order)
		{
			order.Cancel();
		}

		public void StartRide(TaxiOrder order)
		{
			order.StartRide();
		}

		public void FinishRide(TaxiOrder order)
		{
            order.FinishRide();
		}
	}

	public class TaxiOrder
	{
	    public TaxiOrder(int id, PersonName clientName, Address startAddress, DateTime creationTime)
	    {
	        Id = id;
	        ClientName = clientName;
	        StartAddress = startAddress;
	        CreationTime = creationTime;
	    }

	    public int Id { get; }
		public PersonName ClientName { get;  }
        public Address StartAddress { get;}
        public Address DestinationAddress { get; private set; }
        public Driver Driver { get; private set; }
        public TaxiOrderStatus Status { get; private set; }
        public DateTime CreationTime { get; private set; }
        public DateTime DriverAssignmentTime { get; private set; }
        public DateTime CancelTime { get; private set; }
        public DateTime StartRideTime { get; private set; }
        public DateTime FinishRideTime { get; private set; }

	    public void AssignDriver(Driver driver)
	    {
	        this.Driver = driver;
	        Status = TaxiOrderStatus.WaitingCarArrival;
	    }

	    public void UnassignDriver()
	    {
	        Driver = null;
	        Status = TaxiOrderStatus.WaitingForDriver;
	    }

        public void UpdateDestination(string street, string building)
	    {
	        DestinationAddress = new Address(street, building);
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
	        return string.Join(" ",
	            "OrderId: " + Id,
	            "Status: " + Status,
	            "Client: " + FormatName(ClientName.FirstName, ClientName.LastName),
	            "Driver: " + FormatName(Driver.PersonName.FirstName, Driver.PersonName.LastName),
	            "From: " + FormatAddress(StartAddress.Street, StartAddress.Building),
	            "To: " + FormatAddress(DestinationAddress.Street, DestinationAddress.Building),
	            "LastProgressTime: " + GetLastProgressTime().ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
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
	        Status = TaxiOrderStatus.Canceled;
	        CancelTime = DateTime.Now;
	    }

	    public void StartRide()
	    {
	        Status = TaxiOrderStatus.InProgress;
	        StartRideTime = DateTime.Now;
	    }

	    public void FinishRide()
	    {
	        Status = TaxiOrderStatus.Finished;
	        FinishRideTime = DateTime.Now;
	    }
    }
}