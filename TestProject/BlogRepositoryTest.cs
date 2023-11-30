using Henry.Helpers;
using Henry.Models;
using Henry.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    [TestClass]
    public class BlogRepositoryTest
    {
        private string jsonFileName = @"Data\BlogData.json";
        private BlogRepository repository;
        private List<Blog> blogBackup;



        [TestMethod]
        public void AddBlogSucessTest()
        {
            JsonSetUp();
            int beforeAmount = repository.GetAllBlogs().Count;
            repository.AddBlog(new Blog());
            int afterAmount = repository.GetAllBlogs().Count;
            Assert.AreEqual(beforeAmount + 1, afterAmount);
            JsonCleanUp();

        }
        [TestMethod]
        public void AddBlogSameID()
        {
            JsonSetUp();
            int beforeAmount = repository.GetAllBlogs().Count;
            repository.AddBlog(new Blog() { Id = 1 });
            repository.AddBlog(new Blog() { Id = 1 });
            int afterAmount = repository.GetAllBlogs().Count;
            Assert.AreNotEqual(repository.GetAllBlogs()[0].Id, repository.GetAllBlogs()[1].Id);
            JsonCleanUp();

        }

        [TestMethod]
        public void DeleteBlogSucess()
        {
            JsonSetUp();
            int beforeAmount = repository.GetAllBlogs().Count;
            Blog b = new Blog() { Id = 1 };
            Blog c = new Blog() { Id = 1 };
            repository.AddBlog(b);
            bool result = repository.DeleteBlog(c);
            int afterAmount = repository.GetAllBlogs().Count;
            Assert.IsTrue(result);
            Assert.AreEqual(beforeAmount, afterAmount);
            JsonCleanUp();

        }

        [TestMethod]
        public void DeleteBlogNoneFound()
        {
            JsonSetUp();
            int beforeAmount = repository.GetAllBlogs().Count;
            Blog b = new Blog() { Id = 1 };
            repository.AddBlog(b);
            bool sucess = repository.DeleteBlog(new Blog() { Id = 2 });
            int afterAmount = repository.GetAllBlogs().Count;
            Assert.AreNotEqual(beforeAmount, afterAmount);
            Assert.IsFalse(sucess);
            JsonCleanUp();

        }


        [TestMethod]
        public void UpdateBlogSucess()
        {
            JsonSetUp();
            Blog b = new Blog() { Id = 1 };
            repository.AddBlog(b);
            bool sucess = repository.UpdateBlog(new Blog() { Id = 1, Title = "Today's blog" });
            Assert.AreEqual(repository.GetAllBlogs()[0].Title, "Today's blog");
            Assert.IsTrue(sucess);
            JsonCleanUp();

        }

        [TestMethod]
        public void UpdateBlogNotFound()
        {
            JsonSetUp();
            Blog b = new Blog() { Id = 1 };
            repository.AddBlog(b);
            bool sucess = repository.UpdateBlog(new Blog() { Id = 2, Title = "Today's blog" });
            Assert.AreNotEqual(repository.GetAllBlogs()[0].Title, "Today's blog");
            Assert.IsFalse(sucess);
            JsonCleanUp();

        }

        [TestMethod]
        public void GetBlogSucess()
        {
            JsonSetUp();
            Blog b = new Blog() { Id = 1, Title = "Todays blog" };
            repository.AddBlog(b);
            Blog c = repository.GetBlog(b.Id);
            Assert.AreEqual(c.Id, b.Id);
            Assert.AreEqual(c.Title, b.Title);
            JsonCleanUp();

        }







        public void JsonSetUp()
        {
            repository = new BlogRepository();
            blogBackup = repository.GetAllBlogs();
            JsonFileWriter<Blog>.WriteToJson(new List<Blog> { }, jsonFileName);
            blogBackup = repository.GetAllBlogs();
        }
        public void JsonCleanUp()
        {
            JsonFileWriter<Blog>.WriteToJson(blogBackup, jsonFileName);
        }
    }
}
