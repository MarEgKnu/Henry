﻿using Henry.Helpers;
using Henry.Interfaces;
using Henry.Models;
using Microsoft.AspNetCore.Hosting;

namespace Henry.Services
{
    public class BoatRepository : IBoatRepository
    {
        private string jsonFileName = @"Data\BoatData.json";

        private IWebHostEnvironment _env;



        public BoatRepository(IWebHostEnvironment env)
        {
            _env = env;
        }

        public BoatRepository()
        {
        }

        /// <summary>
        /// Adds the given boat param to the repository, and gives it a new unique ID
        /// </summary>
        /// <param name="boat"></param>
        public void AddBoat(Boat boat)
        {
            List<int> BoatIds = new List<int>();

            List<Boat> boats = GetAllBoats();

            foreach (var aBoat in boats)
            {
                BoatIds.Add(aBoat.Id);
            }
            if (BoatIds.Count != 0)
            {
                int start = BoatIds.Max();
                boat.Id = start + 1;
            }
            else
            {
                boat.Id = 1;
            }
            boats.Add(boat);
            JsonFileWriter<Boat>.WriteToJson(boats, jsonFileName);
        }
        /// <summary>
        /// Deletes a boat with the same ID as the provided param from the repository
        /// </summary>
        /// <param name="boat"></param>
        /// <returns>bool</returns>
        public bool DeleteBoat(Boat boat)
        {
            bool sucess;
            // checks if it deleted anything, if not return false
            List<Boat> boats = GetAllBoats();
            foreach (Boat b in boats)
            {
                if (b.Id == boat.Id)
                {
                    sucess = boats.Remove(b);
                    // deletes the img associated with the boat, if it was able to delete it and img isnt null
                    if (b.Img != null && sucess)
                    {
                        string[] paths = { _env.WebRootPath, "Imgs", "BoatImgs", b.Img };
                        string path = Path.Combine(paths);
                        // if the file exists delete it
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }

                    }
                    JsonFileWriter<Boat>.WriteToJson(boats, jsonFileName);
                    return sucess;
                }
            }
            return false;

        }
        /// <summary>
        /// Gets all boats currently in the repository
        /// </summary>
        /// <returns>A list of boats</returns>
        public List<Boat> GetAllBoats()
        {
            return JsonFileReader<Boat>.ReadJson(jsonFileName);
        }

        // overwrites (updates) the boat that has the same ID as the one provided with parameter
        // returns false if no boat with same ID found

        /// <summary>
        /// Updates the boat with the same ID as the given param and overwrites all attributes
        /// </summary>
        /// <param name="boat"></param>
        /// <returns>True if sucessfull, false if not</returns>
        public bool UpdateBoat(Boat boat)
        {
            if (boat != null)
            {
                List<Boat> boats = GetAllBoats();
                foreach (var bo in boats)
                {
                    if (bo.Id == boat.Id)
                    {
                        bo.Description = boat.Description;
                        bo.Name = boat.Name;
                        // if the current img isnt currently null, and the boats have diffrent imagnes, proceed
                        
                        if (bo.Img != null && bo.Img != boat.Img)
                        {
                            string[] paths = { _env.WebRootPath, "Imgs", "BoatImgs", bo.Img };
                            string path = Path.Combine(paths);
                            // if the file exists delete it
                            if (File.Exists(path))
                            {
                                File.Delete(path);
                            }
                            
                        }
                        bo.Img = boat.Img;
                        bo.Type = boat.Type;
                        JsonFileWriter<Boat>.WriteToJson(boats, jsonFileName);
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Returns the boat with an id matching the param. A empty boat object is returned if none is found
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A boat object if found, or empty boat obj if none found</returns>
        public Boat GetBoat(int id)
        {
            foreach (var boat in GetAllBoats())
            {
                if (boat.Id == id) 
                {
                    return boat;
                }
            }
            return new Boat();
        }
    }
}
