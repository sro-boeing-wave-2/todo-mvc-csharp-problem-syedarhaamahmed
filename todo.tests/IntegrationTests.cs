using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using FluentAssertions;
using todo;
using System.Net;
using todo.Model;
using todo.Models;

namespace todo.tests
{
    public class IntegrationTests
    {
        private todoContext context;
        private HttpClient _client;
        public IntegrationTests()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>());

            context = server.Host.Services.GetService(typeof(todoContext)) as todoContext;
            _client = server.CreateClient();
            context.Data.AddRange(SampleNoteinDB);
            context.SaveChanges();
        }

        List<Data> SampleNoteinDB = new List<Data>

        {
            new Data()
            {
                Title = "Title-1",
                Text = "Text-1",
                label = new List<Label>
               {
                   new Label { text = "black-1"},
                   new Label { text = "green-1"}
               },
                checklist = new List<Checklist>
               {
                   new Checklist { text = "redbull-1"},
                   new Checklist { text = "pepsi-1"}
               },
                IsPinned = true
            },

        new Data()
            {
                Title = "Title-2",
                Text = "Text-2",
                label = new List<Label>
               {
                   new Label { text = "black-2"},
                   new Label { text = "green-2"}
               },
                checklist = new List<Checklist>
               {
                   new Checklist { text = "redbull-2"},
                   new Checklist { text = "pepsi-2"}
               },
                IsPinned = true
            },

        new Data()
            {
                Title = "Title-3",
                Text = "Text-3",
                label = new List<Label>
               {
                   new Label { text = "black-3"},
                   new Label { text = "green-3"}
               },
                checklist = new List<Checklist>
               {
                   new Checklist { text = "redbull-3"},
                   new Checklist { text = "pepsi-3"}
               },
                IsPinned = true
            },
        };

        Data NoteFourBeforeUpdating = new Data()

        {
            ID = 1,
            Title = "Title-4",
            Text = "Text-4",
            label = new List<Label>
               {
                   new Label { text = "black-4"},
                   new Label { text = "green-4"}
               },
            checklist = new List<Checklist>
               {
                   new Checklist { text = "redbull-4"},
                   new Checklist { text = "pepsi-4"}
               },
            IsPinned = true
        };

        Data NoteFiveAfterUpdating = new Data()

        {
            ID = 1,
            Title = "Title-5",
            Text = "Text-5",
            label = new List<Label>
               {
                   new Label { text = "black-5"},
                   new Label { text = "green-5"}
               },
            checklist = new List<Checklist>
               {
                   new Checklist { text = "redbull-5"},
                   new Checklist { text = "pepsi-5"}
               },
            IsPinned = true
        };

        Data NoteSixForPosting = new Data()

        {
            Title = "Title-6",
            Text = "Text-6",
            label = new List<Label>
               {
                   new Label { text = "black-6"},
                   new Label { text = "green-6"}
               },
            checklist = new List<Checklist>
               {
                   new Checklist { text = "redbull-6"},
                   new Checklist { text = "pepsi-6"}
               },
            IsPinned = true
        };

        // Testing Code starts here
        [Fact]
        public async void TestGet()
        {
            var response = await _client.GetAsync("/api/Data/GetData");
            var responsestring = await response.Content.ReadAsStringAsync();
            var responsenote = JsonConvert.DeserializeObject<List<Data>>(responsestring);
            Console.WriteLine(responsenote.ToString());
            Assert.Equal(SampleNoteinDB[1].Title, responsenote[1].Title);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void TestGetById()
        {
            var response = await _client.GetAsync("/api/Data/GetData/1");
            var content = await response.Content.ReadAsAsync<Data>();
            Assert.Equal(content.Title, SampleNoteinDB[0].Title);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void TestGetByTitle()
        {
            var response = await _client.GetAsync("/api/Data/GetMultipleData?Title=Title-1");
            var content = await response.Content.ReadAsAsync<List<Data>>();
            Assert.Equal(content[0].Title, SampleNoteinDB[0].Title);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void TestGetByLabel()
        {
            var response = await _client.GetAsync("/api/Data/GetMultipleData?label=black-1");
            var responsestring = await response.Content.ReadAsAsync<List<Data>>();
            Assert.Equal(responsestring[0].Title, SampleNoteinDB[0].Title);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void TestGetByPinned()
        {
            var response = await _client.GetAsync("/api/Data/GetMultipleData?IsPinned=true");
            var content = await response.Content.ReadAsAsync<List<Data>>();
            Assert.Equal(content[0].IsPinned, SampleNoteinDB[0].IsPinned);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void TestPut()
        {
            var response = await _client.PutAsync<Data>("api/Data/PutData/1", NoteFourBeforeUpdating, new System.Net.Http.Formatting.JsonMediaTypeFormatter());
            var content = await response.Content.ReadAsAsync<Data>();
            Assert.Equal(NoteFourBeforeUpdating.Title, content.Title);
            var responsedata = response.StatusCode;
            Assert.Equal(HttpStatusCode.OK, responsedata);
        }

        [Fact]
        public async void TestPost()
        {
            var json = JsonConvert.SerializeObject(NoteSixForPosting);
            var stringContent = new System.Net.Http.StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/Data/PostData", stringContent);
            var responsedata = response.StatusCode;
            Assert.Equal(HttpStatusCode.Created, responsedata);
        }

        [Fact]
        public async void DeleteById()
        {
            var response = await _client.DeleteAsync("/api/Data/DeleteData/2");
            var content = await response.Content.ReadAsAsync<Data>();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


    }
}

