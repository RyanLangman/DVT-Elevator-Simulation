using DVT.Elevator.ConsoleApp.Enums;

namespace DVT.Elevator.ConsoleApp.Models
{
    public class Elevator
    {
        public int Id { get; set; }

        public int CurrentFloor { get; set; }

        public int? DestinationFloor { get; set; }

        public int? PickupFloor { get; set; }

        public int Passengers { get; set; }

        public event EventHandler<EventArgs> OnPickupFloorArrivalEvent;

        public int WeightLimit { get; } = 7;

        public Elevator(int id)
        {
            Id = id;
            CurrentFloor = 1;
        }

        public ElevatorDirection Direction
        {
            get
            {
                if (PickupFloor != null)
                {
                    return PickupFloor > CurrentFloor ?
                        ElevatorDirection.Ascending :
                        ElevatorDirection.Descending;
                }
                else if (DestinationFloor != null)
                {

                    return DestinationFloor > CurrentFloor ?
                        ElevatorDirection.Ascending :
                        ElevatorDirection.Descending;
                }

                return ElevatorDirection.Idle;
            }
        }

        /// <summary>
        /// Move passengers from floor to elevator, constrained to max weight limit of elevator.
        /// </summary>
        /// <param name="numberOfPassengers">The number of passengers unable to board the elevator.</param>
        /// <returns></returns>
        public int OnboardPassengers(int numberOfPassengers)
        {
            var capableOfOnboarding = numberOfPassengers > WeightLimit ? WeightLimit : numberOfPassengers;

            Passengers = capableOfOnboarding;

            return numberOfPassengers > WeightLimit ? numberOfPassengers - WeightLimit : 0;
        }

        public void MoveOneStep()
        {
            // No destination floor means elevator is idle
            if (DestinationFloor == null && PickupFloor == null)
            {
                //Console.WriteLine($"E-{Id} is idle.");
                return;
            }

            // Reached pickup floor, onboarding passengers
            if (CurrentFloor == PickupFloor)
            {
                //Console.WriteLine($"E-{Id} arrived at F-{CurrentFloor}, ready to onboard passengers.");
                OnPickupFloorArrivalEvent?.Invoke(this, EventArgs.Empty);
                PickupFloor = null;
                // TODO: Update passenger count
                // TODO: Consider weight limit

                return;
            }

            // Reached destination floor, offboarding passengers
            if (CurrentFloor == DestinationFloor && PickupFloor == null)
            {
                DestinationFloor = null;
                
                //Console.WriteLine($"E-{Id} is offboarding {Passengers} passengers on F-{CurrentFloor}.");
                Passengers = 0;

                return;
            }

            // At this point, we're either going up or down
            if (Direction == ElevatorDirection.Ascending)
            {
                CurrentFloor++;
                //Console.WriteLine($"E-{Id} is ascending to F-{CurrentFloor} with {Passengers} passenger(s).");
            }
            else
            {
                CurrentFloor--;
                //Console.WriteLine($"E-{Id} is descending to F-{CurrentFloor} with {Passengers} passenger(s).");
            }
        }

        public override string ToString()
        {
            switch (Direction)
            {
                case ElevatorDirection.Ascending:
                case ElevatorDirection.Descending:
                    if (PickupFloor != null)
                    {
                        if (CurrentFloor == PickupFloor)
                        {
                            return $"E-{Id} is boarding passenger on F-{CurrentFloor} then headed to F-{DestinationFloor} for dropoff.";
                        }

                        return $"E-{Id} is on F-{CurrentFloor}, headed to F-{PickupFloor} for pickup then F-{DestinationFloor} for dropoff.";
                    }

                    if (CurrentFloor == DestinationFloor)
                    {
                        return $"E-{Id} dropping off {Passengers} passenger(s) on F-{CurrentFloor}.";
                    }

                    return $"E-{Id} is on F-{CurrentFloor}, headed to F-{DestinationFloor} to dropoff {Passengers} passenger(s).";

                case ElevatorDirection.Idle:
                default:
                    return $"E-{Id} is idle on F-{CurrentFloor}.";
            }
        }
    }
}
