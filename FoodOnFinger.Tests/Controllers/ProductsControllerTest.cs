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

                new Product { ProductID = 1, Name = "Fake Product Name", CuisineID = 1, Cuisine = new Cuisine{ CuisineID = 1,Name="fake" } },

                new Product { ProductID = 2, Name = "Fake Product Name", CuisineID = 2, Cuisine = new Cuisine { CuisineID = 2 , Name="fake"} }

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

        [TestMethod]
        public void DetailsView()
        {
            //Act
            ViewResult result = pc.Details(1) as ViewResult;

            //Assert
            Assert.AreEqual("Details", result.ViewName);
        }
        [TestMethod]
        public void DetailsViewNullReturn()
        {
            //Act
            ViewResult result = pc.Details(1) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DetailsViewNullID()
        {
            //Act

            HttpStatusCodeResult result = pc.Details(null) as HttpStatusCodeResult;

            //Assert

            Assert.AreEqual(400, result.StatusCode);

        }

        [TestMethod]
        public void DetailsViewNul()
        {
            //Act

            HttpStatusCodeResult result = pc.Details(25) as HttpStatusCodeResult;

            //Assert

            Assert.AreEqual(404, result.StatusCode);

        }

        [TestMethod]

        public void DetailsViewProduct()

        {

            //Act

            var result = ((ViewResult)pc.Details(1)).Model;

            //Assert

            Assert.AreEqual(products.SingleOrDefault(p => p.ProductID == 1), result);

        }

        [TestMethod]
        public void CreateViewLoad()
        {
            // Act
            ViewResult result = pc.Create() as ViewResult;

            // Assert
            Assert.AreEqual("Create", result.ViewName);
        }
        [TestMethod]
        public void CreateViewLoadNullReturn()
        {
            // Act
            ViewResult result = pc.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateValidModel()
        {
            pc.ModelState.AddModelError("Description", "error");
            // Act
            ViewResult result = pc.Create(products[0]) as ViewResult;

            // Assert
            Assert.AreEqual("Create", result.ViewName);          
        }
        [TestMethod]
        public void CreatePostRedirect()
        {
            RedirectToRouteResult result = pc.Create(products[0]) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);           
        }

        [TestMethod]
        public void CreatePostSelectList()
        {
            pc.ModelState.AddModelError("Description", "error");
            SelectList result = ((ViewResult)pc.Create(new Product { ProductID = 3, CuisineID = 0 })).ViewBag.CuisineID;

            Assert.AreEqual(0,result.SelectedValue);           
        }
        
        [TestMethod]
        public void EditValidModel()
        {
            pc.ModelState.AddModelError("Description", "error");
            // Act
            ViewResult result = pc.Edit(products[0]) as ViewResult;

            // Assert
            Assert.AreEqual("Edit", result.ViewName);
        }
        [TestMethod]
        public void EditPostRedirect()
        {
            RedirectToRouteResult result = pc.Edit(products[0]) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void EditPostSelectList()
        {
            pc.ModelState.AddModelError("Description", "error");
            SelectList result = ((ViewResult)pc.Edit(products[1])).ViewBag.CuisineID;

            Assert.AreEqual(2, result.SelectedValue);
        }

        [TestMethod]
        public void EditSelectList()
        {
            SelectList result = ((ViewResult)pc.Edit(2)).ViewBag.CuisineID;

            Assert.AreEqual(2, result.SelectedValue);
        }

        [TestMethod]
        public void EditViewNotFound()
        {
            //Act

            HttpStatusCodeResult result = pc.Edit(25) as HttpStatusCodeResult;

            //Assert

            Assert.AreEqual(404, result.StatusCode);

        }

        [TestMethod]
        public void DeleteView()
        {
            //Act
            ViewResult result = pc.Delete(1) as ViewResult;

            //Assert
            Assert.AreEqual("Delete", result.ViewName);
        }[TestMethod]
        public void DeleteViewReturnNullView()
        {
            //Act
            ViewResult result = pc.Delete(1) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteViewNullID()
        {
            //Act

            HttpStatusCodeResult result = pc.Delete(null) as HttpStatusCodeResult;

            //Assert

            Assert.AreEqual(400, result.StatusCode);

        }

        [TestMethod]
        public void DeleteViewNul()
        {
            //Act

            HttpStatusCodeResult result = pc.Delete(25) as HttpStatusCodeResult;

            //Assert

            Assert.AreEqual(404, result.StatusCode);

        }

        [TestMethod]

        public void DeleteViewProduct()

        {

            //Act

            var result = ((ViewResult)pc.Delete(1)).Model;

            //Assert

            Assert.AreEqual(products.SingleOrDefault(p => p.ProductID == 1), result);

        }

        [TestMethod]
        public void DeleteConfirmedPostRedirect()
        {
            RedirectToRouteResult result = pc.DeleteConfirmed(0) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
        [TestMethod]
        public void DeleteConfirmedPostNullView()
        {
            RedirectToRouteResult result = pc.DeleteConfirmed(0) as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }
    }
}
