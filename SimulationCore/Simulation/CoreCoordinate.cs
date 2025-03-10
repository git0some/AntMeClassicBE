using System;

namespace AntMe.Simulation
{


    /// <summary>
    /// Item coordinates on the playground.
    /// </summary>
    /// <author>Wolfgang Gallo (wolfgang@antme.net)</author>
    public struct CoreCoordinate
    {
        private int radius;
        private int direction;
        private int x;
        private int y;

        /// <summary>
        /// Constructor for new instances of a coordinate struct for an item.
        /// </summary>
        /// <param name="x">X coordinate of the item.</param>
        /// <param name="y">Y coordinate of the item.</param>
        /// <param name="radius">Radius of the item.</param>
        /// <param name="direction">Direction the item is oriented to.</param>
        internal CoreCoordinate(int x, int y, int radius, int direction)
        {
            this.x = x * SimulationEnvironment.PLAYGROUND_UNIT;
            this.y = y * SimulationEnvironment.PLAYGROUND_UNIT;

            // All parameters of the constructor must be initialized.
            // That's why radius and direction are set 0 now
            // they will be overwritten afterwards.
            this.radius = 0;
            this.direction = 0;

            Radius = radius * SimulationEnvironment.PLAYGROUND_UNIT;
            Direction = direction;
        }

        /// <summary>
        /// Constructor for new instances of a coordinate struct
        /// without direction.
        /// </summary>
        /// <param name="x">X coordinate of the item.</param>
        /// <param name="y">Y coordinate of the item.</param>
        /// <param name="radius">Radius of the item.</param>
        internal CoreCoordinate(int x, int y, int radius)
        {
            this.x = x * SimulationEnvironment.PLAYGROUND_UNIT;
            this.y = y * SimulationEnvironment.PLAYGROUND_UNIT;
            this.radius = 0;
            direction = 0;
            Radius = radius * SimulationEnvironment.PLAYGROUND_UNIT;
        }

        /// <summary>
        /// Constructor for new instances of a coordinate struct
        /// without direction or radius.
        /// </summary>
        /// <param name="x">X coordinate of the item.</param>
        /// <param name="y">Y coordinate of the item.</param>
        internal CoreCoordinate(int x, int y)
        {
            this.x = x * SimulationEnvironment.PLAYGROUND_UNIT;
            this.y = y * SimulationEnvironment.PLAYGROUND_UNIT;
            radius = 0;
            direction = 0;
        }

        /// <summary>
        /// Constructor for new instances of a coordinate struct
        /// in relation to the given coordinate.
        /// </summary>
        /// <param name="c">The given coordinate.</param>
        /// <param name="deltaX">X coordinate of the item in relation to the given coordinate.</param>
        /// <param name="deltaY">Y coordinate of the item in relation to the given coordinate.</param>
        internal CoreCoordinate(CoreCoordinate c, int deltaX, int deltaY)
        {
            x = c.x + deltaX;
            y = c.y + deltaY;
            radius = c.radius;
            direction = c.direction;
        }

        /// <summary>
        /// The x value of an item coordinate.
        /// </summary>
        internal int X
        {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// The y value of an item coordinate. 
        /// </summary>
        internal int Y
        {
            get { return y; }
            set { y = value; }
        }

        /// <summary>
        /// Radius of the item coordinate.
        /// All calculations of the the simulation are based on coordinates 
        /// and associated hemispheres like the distance between items
        /// therefor the radius is part of the coordinate struct.
        /// </summary>
        internal int Radius
        {
            get { return radius; }
            set { radius = Math.Abs(value); }
        }

        /// <summary>
        /// Direction the item is oriented to, not all items need a direction.
        /// The items needing a direction usually have methods to turn themself
        /// to a direction. This is not part of conventional coordinate systems
        /// but the CoreCoordinate struct is good place to store this information.
        /// </summary>
        internal int Direction
        {
            get { return direction; }
            set
            {
                direction = value;
                while (direction < 0)
                {
                    direction += 360;
                }
                while (direction > 359)
                {
                    direction -= 360;
                }
            }
        }

        /// <summary>
        /// Determine the distance between two items on the playground in steps.
        /// </summary>
        /// <param name="o1">Object 1.</param>
        /// <param name="o2">Object 2.</param>
        /// <returns>Distance in steps.</returns>
        internal static int DetermineDistance(ICoordinate o1, ICoordinate o2)
        {
            return DetermineDistanceI(o1.CoordinateCoreInsect, o2.CoordinateCoreInsect) / SimulationEnvironment.PLAYGROUND_UNIT;
        }

        /// <summary>
        /// Determine direction from one item on the playground to another.
        /// </summary>
        /// <param name="i1">Source item.</param>
        /// <param name="i2">Destination item.</param>
        /// <returns>Direction.</returns>
        internal static int DetermineDirection(ICoordinate i1, ICoordinate i2)
        {
            return DetermineDirection(i1.CoordinateCoreInsect, i2.CoordinateCoreInsect);
        }

        /// <summary>
        /// Determine the distance between two item coordinates on the playground in internal unit.
        /// </summary>
        /// <param name="c1">Coordinate 1.</param>
        /// <param name="c2">Coordinate 2.</param>
        /// <returns>Distance between coordinates in internal unit.</returns>
        internal static int DetermineDistanceI(CoreCoordinate c1, CoreCoordinate c2)
        {
            double deltaX = c1.x - c2.x;
            double deltaY = c1.y - c2.y;
            int distance = (int)Math.Round(Math.Sqrt(deltaX * deltaX + deltaY * deltaY));
            distance = distance - c1.radius - c2.radius;
            if (distance < 0)
            {
                return 0;
            }
            return distance;
        }

        /// <summary>
        /// Determine the distance between two item coordinates on the playground in internal unit
        /// without considering the radii.
        /// </summary>
        /// <param name="c1">Coordinate 1.</param>
        /// <param name="c2">Coordinate 2.</param>
        /// <returns>Distance in internal unit.</returns>
        internal static int DetermineDistanceToCenter(CoreCoordinate c1, CoreCoordinate c2)
        {
            double deltaX = c1.x - c2.x;
            double deltaY = c1.y - c2.y;
            return (int)Math.Round(Math.Sqrt(deltaX * deltaX + deltaY * deltaY));
        }

        /// <summary>
        /// Determine direction from one item coordinate on the playground to another.
        /// </summary>
        /// <param name="c1">Source coordinate.</param>
        /// <param name="c2">Target coordinate.</param>
        /// <returns>Direction.</returns>
        internal static int DetermineDirection(CoreCoordinate c1, CoreCoordinate c2)
        {
            int direction = (int)Math.Round(Math.Atan2(c2.Y - c1.Y, c2.X - c1.X) * 180d / Math.PI);
            if (direction < 0)
            {
                direction += 360;
            }
            return direction;
        }
    }
}