using RestaurantReservierung.Model;

namespace RestaurantReservierung
{
    public class Table
    {
        int chairs;
        TableAttributes attributes;
        int tableID;
        List<Reservation> reservations;


        public Table(int chairs, TableAttributes attributes, int tableID)
        {
            this.chairs = chairs;
            this.attributes = attributes;
            this.tableID = tableID;
            this.reservations = new List<Reservation>();
        }

        /// <summary>
        /// Tries to make a reservation for any table that is available at the requested time. 
        /// Picks the first available one if multiple ones are free.
        /// Updates the passed tables and reservation objects in case of a successful reservation by using the 'ref'-keyword.
        /// Returns the zero-based index of the table in the provided List<Table> object, for which the reservation was made for.
        /// Returns -1 if an error occurs.
        /// Returns -2 if no table in the list was free.
        /// </summary>
        public static int Reserve(ref List<Table> tables, ref Reservation reservation)
        {
            //return error code
            if (reservation == null)
                return -1;

            //time window of desired reservation
            long desiredStart = reservation.StartTime;
            long desiredEnd = reservation.EndTime;


            //search for available table
            List<Reservation> tmp;
            long tStart, tEnd;


            for (int i = 0; i < tables.Count; ++i)
            {
            startOfCheck:
                tmp = tables[i].Reservations;

                for (int j = 0; j < tmp.Count; ++j)
                {
                    tStart = tmp[j].StartTime;
                    tEnd = tmp[j].EndTime;

                    //if the desired start or end time of the new reservation is within the currently
                    //evaluated, existing reservations time window (displayed by tStart and tEnd), skip to the
                    //next table
                    //Note: The 'goto'-keyword is employed here so that we do not reach the code below this for loop
                    //if the table is occupied, like it would be the case with using the 'break' statement
                    //within the condition in the loop
                    if ((tStart > desiredStart && tStart < desiredEnd) || (tEnd > desiredStart && tEnd < desiredEnd))
                    {
                        ++i;
                        goto startOfCheck;
                    }
                }

                //at this point, all existing reservations for the current table were checked and there was no overlap
                // -> make reservation for current table!

                //add table reference to reservation
                reservation.Table = tables[i];

                //add new reservation to reservation list of current table
                tables[i].reservations.Add(reservation);

                return i;
            }

            //no table found
            return -2;
            /*


            int count = reservations.Count;

            //search for table which is available during requested time window
            Reservation current;
            long tStart, tEnd;

            for (int i = 0; i < count; ++i)
            {
                current = reservations[i];
                tStart = current.StartTime;
                tEnd = current.EndTime;




            } */

        }



        #region Getters / Setters

        public int Chairs
        {
            get { return chairs; }
        }

        public TableAttributes Attributes
        {
            get { return attributes; }
        }

        public int TableID
        {
            get { return tableID; }
        }

        public List<Reservation> Reservations
        {
            get { return reservations; }
        }

        #endregion
    }


    public enum TableAttributes
    {
        ROUND = 0,
        SQUARE = 1,
        STANDING = 2,
    }
}
