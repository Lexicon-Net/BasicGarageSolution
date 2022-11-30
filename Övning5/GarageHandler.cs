﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Övning5
{
    internal class GarageHandler: IHandler
    {
        private Garage<IVehicle> thisGarage;

        private IEnumerable<IVehicle> query;
        public void InitGarage()
        {
            thisGarage.Add(new Boat("ABC123", "Red", 3, 27));
            thisGarage.Add(new Car("DEF456", "White", 4, 1.64));
            thisGarage.Add(new Bus("GHI789", "Black", 6, 24));
            thisGarage.Add(new Car("JKL101", "Blue", 4, 19));
            thisGarage.Add(new Motorcycle("MNO112", "Blue", 2, "Diesel"));
        }

        //Kan kanske förbättra/skriva ihop med något annat
        public List<IVehicle> GetGarage()
        {
           // var strings = thisGarage.Select(v => v.Stats()).ToList();

            List<IVehicle> output = new List<IVehicle>();
            foreach (var vehicle in thisGarage)
            {
                if (vehicle != null) { 
                    output.Add(vehicle);
                }
            }
            return output;
        }

        public IVehicle? GetFromRegNr(string regNr)
        {

           // return thisGarage.FirstOrDefault(v => v.RegNr.ToLower() == regNr.ToLower());
            foreach(var vehicle in thisGarage)
            {
                if(vehicle.RegNr.ToLower() == regNr.ToLower())
                {
                    return vehicle;
                }
            }
            return null;
        }

        public void AddVehicle(IVehicle vehicle)
        {
            thisGarage.Add(vehicle);
        }


        public bool RemoveVehicle(string regNr)
        {

            return thisGarage.Unpark(regNr);
        }

        public GarageHandler(int capacity)
        {
            thisGarage = new Garage<IVehicle>(capacity);
        }

        public List<IVehicle> NrOfType(string type)
        {

            //var res = thisGarage.GroupBy(v => v.GetType().Name)
            //                    .Select(v => new NrOfTypes
            //                    {
            //                        TypeName = v.Key,
            //                        NrOfVehicles = v.Count()
            //                    });
           

            List<IVehicle> output = new List<IVehicle>();
            foreach(var vehicle in thisGarage)
            {
                string currType = vehicle.GetType().Name;//.ToString().Split('.').Last();
                if (currType == type)
                {
                    output.Add(vehicle);
                }
            }
            return output;
        }

        public bool IsFull()
        {
            return thisGarage.IsFull;
        }

        [MemberNotNull("query")]
        public void InitFilter()
        {
            query = thisGarage;//.GetQuery();
        }
        public void Filter(string typeQ, string typeS, int typeI)
        {
            if(query == null)
            {
                InitFilter();
            }

            if(typeQ == "type")
            {
                query = query.Where(p => p.GetType().Name == typeS);
                return;
            }else if(typeQ == "color")
            {
                query = query.Where(p => p.Color.ToLower() == typeS.ToLower());
                return;
            }
            else if (typeQ == "wheels")
            {
                query = query.Where(p => p.Wheels == typeI);
                return;
            }
        }

        public List<IVehicle> PrintFilter()
        {
            return query.ToList();
        }

    }
}
