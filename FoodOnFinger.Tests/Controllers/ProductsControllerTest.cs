using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FoodOnFinger.Controllers;
using FoodOnFinger.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FoodOnFinger.Tests.Controllers
{
    [TestClass]
    public class ProductsControllerTest
    {
        //MOQ Data

        ProductsController pc;

        List<Product> products;

        Mock<IMockProducts> moq;



        [TestInitialize]

        public void TestInitialize()

        {

            products = new List<Product>

            {

                new Product { ProductID = 1, Name = "Fake Product Name", CuisineID = 1, Cuisine = new Cuisine{ CuisineID = 1 } },

                new Product { ProductID = 2, Name = "Fake Product Name", CuisineID = 2, Cuisine = new Cuisine { CuisineID = 2 } }

            };

            moq = new Mock<IMockProducts>();

            moq.Setup(p => p.Products).Returns(products.AsQueryable());

            pc = new ProductsController(moq.Object);

        }

        [TestMethod]
        public void IndexViewName()
        {
            //Act
            ViewResult result = pc.Index() as ViewResult;

            //Assert
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void IndexViewIsNotNull()
        {
            //Act
            ViewResult result = pc.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]

        public void IndexLoadsProducts()

        {
            //Act
            var results = (List<Product>)((ViewResult)pc.Index()).Model;
            //Assert
            CollectionAssert.AreEqual(products.ToList(), results);
        }
    }
}
