using System;
using System.Collections.Generic;
using System.Text;
using todo.Controllers;
using Microsoft.EntityFrameworkCore;
using todo.Services;
using todo.Models;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using todo.Model;

namespace todo.tests
{
    public class NotesUnitTests
    {
        private DataController testingController;
        Data note;
        Data DeleteThisNote;
          
        public NotesUnitTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<todoContext>();
            optionsBuilder.UseInMemoryDatabase("TestDB");
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var notesService = new NoteService(new todoContext(optionsBuilder.Options));
            var todocontext = new todoContext(optionsBuilder.Options);
            testingController = new DataController(todocontext);

            note = new Data
            {
                ID = 1,
                Title = "first",
                Text = "Ft",
                label = new List<Label>
               {
                   new Label { text = "black"},
                   new Label { text = "green"}
               },
                checklist = new List<Checklist>
               {
                   new Checklist { text = "redbull"},
                   new Checklist { text = "pepsi"}
               },
                IsPinned = true
            };

            DeleteThisNote = new Data
            {
                ID = 3,
                Title = "third",
                Text = "Ft",
                label = new List<Label>
               {
                   new Label { text = "blue"},
                   new Label { text = "green"}
               },
                checklist = new List<Checklist>
               {
                   new Checklist { text = "redbull"},
                   new Checklist { text = "pepsi"}
               },
                IsPinned = true
            };

            todocontext.Data.Add(note);
            todocontext.Data.Add(DeleteThisNote);
            todocontext.SaveChanges();
        }
        
        [Fact]
        public async void TestPost()
        {
            var postThisNote = new Data
            {
                ID = 2,
                Title = "oh",
                Text = "Ft",
                label = new List<Label>
               {
                   new Label { text = "black"},
                   new Label { text = "green"}
               },
                checklist = new List<Checklist>
               {
                   new Checklist { text = "redbull"},
                   new Checklist { text = "pepsi"}
               },
                IsPinned = true
            };
            var result = await testingController.PostData(postThisNote);
            var okObjectResult = result as CreatedAtActionResult;
            var notes = okObjectResult.Value as Data;
            Assert.Equal(postThisNote.Title,notes.Title);
        }

        [Fact]
        public async void TestGet()
        {
            var result = await testingController.GetData();
            var okObjectResult = result as OkObjectResult;
            var notes = okObjectResult.Value as List<Data>;
            Assert.Equal(2, notes.Count);
        }

        [Fact]
        public async void TestGetById()
        {
            var result = await testingController.GetData(1);
            var okObjectResult = result as OkObjectResult;
            var notes = okObjectResult.Value as Data;
            Assert.Equal(1, notes.ID);
        }

        [Fact]
        public async void TestGetByLabel()
        {
            var result = await testingController.GetMultipleData(null, "black", null);
            var okObjectResult = result as OkObjectResult;
            var notes = okObjectResult.Value as List<Data>;
            Assert.Single(notes);
        }

        [Fact]
        public async void TestGetByTitle()
        {
            var result = await testingController.GetMultipleData("first", null , null);
            var okObjectResult = result as OkObjectResult;
            var notes = okObjectResult.Value as List<Data>;
            Assert.Single(notes);
        }

        [Fact]
        public async void TestDeleteById()
        {

            var result = await testingController.DeleteData(3);
            var okObjectResult = result as OkObjectResult;
            var notes = okObjectResult.StatusCode;
            Assert.Equal(200, notes);
        }

        //[Fact]
        //public async void TestPut()
        //{
        //    var updateThisNote = new Data
        //    {
        //        ID = 1,
        //        Title = "third",
        //        Text = "tt",
        //        label = new List<Label>
        //       {
        //           new Label { text = "red"},
        //           new Label { text = "pink"}
        //       },
        //        checklist = new List<Checklist>
        //       {
        //           new Checklist { text = "fanta"},
        //           new Checklist { text = "redlabel"}
        //       },
        //        IsPinned = true
        //    };
        //    var result = await testingController.PutData(1, updateThisNote);
        //    var okObjectResult = result as ObjectResult;
        //    var notes = okObjectResult.StatusCode;
        //    Assert.Equal(204, notes);
        //}



    }
    }
