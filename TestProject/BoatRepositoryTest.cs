using Henry.Helpers;
using Henry.Models;
using Henry.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace TestProject
{
    [TestClass]
    public class BoatRepositoryTest
    {
        private string jsonFileName = @"Data\BoatData.json";
        private BoatRepository repository;
        private List<Boat> boatBackup;

        [TestMethod]
        public void AddBoatSucessTest()
        {
            JsonSetUp();
            int beforeAmount = repository.GetAllBoats().Count;
            repository.AddBoat(new Boat());
            int afterAmount = repository.GetAllBoats().Count;
            Assert.AreEqual(beforeAmount + 1, afterAmount);
            JsonCleanUp();

        }
        [TestMethod]
        public void AddBoatSameID()
        {
            JsonSetUp();
            int beforeAmount = repository.GetAllBoats().Count;
            repository.AddBoat(new Boat() { Id = 1 });
            repository.AddBoat(new Boat() { Id = 1 });
            int afterAmount = repository.GetAllBoats().Count;
            Assert.AreNotEqual(repository.GetAllBoats()[0].Id, repository.GetAllBoats()[1].Id);
            JsonCleanUp();

        }


        [TestMethod]
        public void DeleteBoatSucess()
        {
            JsonSetUp();
            int beforeAmount = repository.GetAllBoats().Count;
            Boat b = new Boat() { Id = 1 };
            Boat c = new Boat() { Id = 1 };
            repository.AddBoat(b);
            repository.DeleteBoat(c);
            int afterAmount = repository.GetAllBoats().Count;
            Assert.AreEqual(beforeAmount, afterAmount);
            JsonCleanUp();

        }

        [TestMethod]
        public void DeleteBoatNoneFound()
        {
            JsonSetUp();
            int beforeAmount = repository.GetAllBoats().Count;
            Boat b = new Boat() { Id = 1 };
            repository.AddBoat(b);
            bool sucess = repository.DeleteBoat(new Boat() { Id = 2 });
            int afterAmount = repository.GetAllBoats().Count;
            Assert.AreNotEqual(beforeAmount, afterAmount);
            Assert.IsFalse(sucess);
            JsonCleanUp();

        }



        [TestMethod]
        public void UpdateBoatSucess()
        {
            JsonSetUp();
            Boat b = new Boat() { Id = 1 };
            repository.AddBoat(b);
            bool sucess = repository.UpdateBoat(new Boat() { Id = 1, Name = "Jollen" });
            Assert.AreEqual(repository.GetAllBoats()[0].Name, "Jollen");
            Assert.IsTrue(sucess);
            JsonCleanUp();

        }

        [TestMethod]
        public void UpdateBoatNotFound()
        {
            JsonSetUp();
            Boat b = new Boat() { Id = 1 };
            repository.AddBoat(b);
            bool sucess = repository.UpdateBoat(new Boat() { Id = 2, Name = "Jollen" });
            Assert.AreNotEqual(repository.GetAllBoats()[0].Name, "Jollen");
            Assert.IsFalse(sucess);
            JsonCleanUp();

        }

        [TestMethod]
        public void GetBoatSucess()
        {
            JsonSetUp();
            Boat b = new Boat() { Id = 1 };
            repository.AddBoat(b);
            Boat c = repository.GetBoat(b.Id);
            Assert.AreEqual(c.Id, b.Id);
            Assert.AreEqual(c.Name, b.Name);
            JsonCleanUp();

        }


        public void JsonSetUp()
        {
            repository = new BoatRepository();
            boatBackup = repository.GetAllBoats();
            JsonFileWriter<Boat>.WriteToJson(new List<Boat> { }, jsonFileName);
        }
        public void JsonCleanUp()
        {
            JsonFileWriter<Boat>.WriteToJson(boatBackup, jsonFileName);
        }
    }
}