using MIS333K_FinalProject.DAL;
using System;
using System.Linq;


namespace MIS333K_FinalProject.Utilities
{
    public static class GenerateNextPropertyID
    {
        public static Int32 GetNextPropertyID(AppDbContext _context)
        {
            //set a constant to designate where the registration numbers 
            //should start
            const Int32 START_NUMBER = 3001;

            Int32 intMaxPropertyNumber; //the current maximum course number
            Int32 intNextPropertyNumber; //the course number for the next class

            if (_context.Properties.Count() == 0) //there are no registrations in the database yet
            {
                intMaxPropertyNumber = START_NUMBER; //registration numbers start at 10001
            }
            else
            {
                intMaxPropertyNumber = _context.Properties.Max(c => c.PropertyId); //this is the highest number in the database right now
            }

            //You added records to the datbase before you realized 
            //that you needed this and now you have numbers less than 100 
            //in the database
            if (intMaxPropertyNumber < START_NUMBER)
            {
                intMaxPropertyNumber = START_NUMBER;
            }

            //add one to the current max to find the next one
            intNextPropertyNumber = intMaxPropertyNumber + 1;

            //return the value
            return intNextPropertyNumber;
        }

    }
}