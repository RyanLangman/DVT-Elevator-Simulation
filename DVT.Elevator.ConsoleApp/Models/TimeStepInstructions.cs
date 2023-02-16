namespace DVT.Elevator.ConsoleApp.Models
{
    public class TimeStepInstructions
    {
        private readonly int TopFloor;
        private readonly int BottomFloor;

        private int _pickupFloor;
        public int PickupFloor
        {
            get { return _pickupFloor; }
        }

        private int _destinationFloor;
        public int DestinationFloor
        {
            get { return _destinationFloor; }
        }

        private int _numberOfPeople;
        public int NumberOfPeople
        {
            get { return _numberOfPeople; }
        }

        public bool SkipThisStep { get; set; }

        public TimeStepInstructions(int topFloor, int bottomFloor)
        {
            TopFloor = topFloor;
            BottomFloor = bottomFloor;
        }

        public bool SetPickupFloorNumber(string userInput)
        {
            try
            {
                var parsed = int.Parse(userInput);

                if (parsed < BottomFloor || parsed > TopFloor)
                {
                    Console.WriteLine("That floor doesn't exist.");
                    return false;
                };

                if (parsed == DestinationFloor)
                {
                    Console.WriteLine("Pickup floor cannot be the same as destination floor.");
                    return false;
                }

                _pickupFloor = parsed;
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input.");
                return false;
            }
        }

        public bool SetDestinationFloorNumber(string userInput)
        {
            try
            {
                var parsed = int.Parse(userInput);

                if (parsed < BottomFloor || parsed > TopFloor)
                {
                    Console.WriteLine("That floor doesn't exist.");
                    return false;
                };

                if (parsed == PickupFloor)
                {
                    Console.WriteLine("Destination floor cannot be the same as pickup floor.");
                    return false;
                }

                _destinationFloor = parsed;
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input.");
                return false;
            }
        }

        public bool SetAwaitingPassengers(string userInput)
        {
            try
            {
                var parsed = int.Parse(userInput);

                if (parsed < 1)
                {
                    Console.WriteLine("Was that a ghost?");
                    return false;
                };

                if (parsed > 14)
                {
                    Console.WriteLine("Too many people!");
                    return false;
                }

                _numberOfPeople = parsed;
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input.");
                return false;
            }
        }
    }
}
