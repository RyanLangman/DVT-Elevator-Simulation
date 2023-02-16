using DVT.Elevator.ConsoleApp.Enums;

namespace DVT.Elevator.ConsoleApp.Models
{
    public class Passenger
    {
        public Guid Id { get; } = Guid.NewGuid();

        public Floor PickupFloor { get; set; }

        public Floor DestinationFloor { get; set; }

        public ElevatorDirection Direction { 
            get
            {
                return DestinationFloor.Id > PickupFloor.Id ?
                    ElevatorDirection.Ascending : 
                    ElevatorDirection.Descending;
            } 
        }
    }
}
