using Henry.Helpers;
using Henry.Interfaces;
using Henry.Models;

namespace Henry.Services
{
    public class RepairRepository : IRepairRepository
    {

        private string jsonFileName = @"Data\RepairData.json";

        private IWebHostEnvironment _env;

        public RepairRepository(IWebHostEnvironment env)
        {
            _env = env;
        }


        /// <summary>
        /// Adds the given repair param to the repository, and gives it a new unique ID
        /// </summary>
        /// <param name="repair"></param>
        public void AddRepair(Repair repair)
        {
            List<int> RepairIds = new List<int>();

            List<Repair> repairs = GetAllRepairs();

            foreach (var aRepair in repairs)
            {
                RepairIds.Add(aRepair.RepairId);
            }
            if (RepairIds.Count != 0)
            {
                int start = RepairIds.Max();
                repair.RepairId = start + 1;
            }
            else
            {
                repair.RepairId = 1;
            }
            repairs.Add(repair);
            JsonFileWriter<Repair>.WriteToJson(repairs, jsonFileName);
        }







        /// <summary>
        /// Deletes a repair with the same ID as the provided param from the repository
        /// </summary>
        /// <param name="repair"></param>
        /// <returns>bool</returns>
        public bool DeleteRepair(Repair repair)
        {
            bool sucess;
            // checks if it deleted anything, if not return false
            List<Repair> repairs = GetAllRepairs();
            foreach (var b in repairs)
            {
                if (b.RepairId == repair.RepairId)
                {
                    sucess = repairs.Remove(b);
                    // deletes the img associated with the boat, if it was able to delete it and img isnt null
                    if (b.Img != null && sucess)
                    {
                        string[] paths = { _env.WebRootPath, "Imgs", "RepairImgs", b.Img };
                        string path = Path.Combine(paths);
                        // if the file exists delete it
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }

                    }
                    JsonFileWriter<Repair>.WriteToJson(repairs, jsonFileName);
                    return sucess;
                }
            }
            return false;

        }

        /// <summary>
        /// Updates the repair with the same ID as the given param and overwrites  relevant attributes
        /// </summary>
        /// <param name="repair"></param>
        /// <returns>True if sucessfull, false if not</returns>
        public bool UpdateRepair(Repair repair)
        {
            if (repair != null)
            {
                List<Repair> repairs = GetAllRepairs();
                foreach (var re in repairs)
                {
                    if (re.RepairId == repair.RepairId)
                    {
                        re.Title = repair.Title;
                        re.Description = repair.Description;
                        // if the current img isnt currently null, and the repairs have diffrent imagnes, proceed and delete the old img

                        if (re.Img != null && re.Img != repair.Img)
                        {
                            string[] paths = { _env.WebRootPath, "Imgs", "BoatImgs", re.Img };
                            string path = Path.Combine(paths);
                            // if the file exists delete it
                            if (File.Exists(path))
                            {
                                File.Delete(path);
                            }

                        }
                        re.Img = repair.Img;
                        JsonFileWriter<Repair>.WriteToJson(repairs, jsonFileName);
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
        /// Gets all repair currently in the repository
        /// </summary>
        /// <returns>A list of repair</returns>
        public List<Repair> GetAllRepairs()
        {
            return JsonFileReader<Repair>.ReadJson(jsonFileName);
        }

        /// <summary>
        /// Returns the boat with an id matching the param. A empty boat object is returned if none is found
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A boat object if found, or empty boat obj if none found</returns>
        public Repair GetRepair(int id)
        {
            foreach (var repair in GetAllRepairs())
            {
                if (repair.RepairId == id)
                {
                    return repair;
                }
            }
            return new Repair();
        }

        /// <summary>
        /// Returns a list of Repairs for the inputted boatId
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A list of Repair objects</returns>
        public List<Repair> GetRepairsForBoat(int id)
        {
            List<Repair> repairs = new List<Repair>();
            foreach (var repair in GetAllRepairs())
            {
                if (repair.BoatId == id)
                {
                    repairs.Add(repair);
                }
            }
            return repairs;
        }
        /// <summary>
        /// Checks if the inputted boatId has any repairs attached to it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if 1 or more repairs exist, false if not</returns>
        public bool HasAnyRepairs(int id)
        {
            foreach (Repair repair in GetAllRepairs())
            {
                if (repair.BoatId == id)
                {
                    return true;
                }
            }
            return false;
        }


    }
}
