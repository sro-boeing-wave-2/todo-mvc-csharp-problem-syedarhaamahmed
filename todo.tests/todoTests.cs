//using System;
//using Xunit;
//using NSuperTest;

//namespace todo.tests
//{
//    public class TodoTests
//    {
//        static Server server;

//        [Fact]
//        public static void Init()
//        {
//            server = new Server("https://localhost:44311/api/Data");
//        }

//        object TestNoteOne = new
//        {
//            Title = "Note One Title",
//            Text = "This is the text for the Test Note One",
//            Pinned = "True"
//        };

//        object TestNoteTwo = new
//        {
//            Title = "Note Two Title",
//            Text = "This is the text for the Test Note Two",
//            Pinned = "True"
//        };

//        [Fact]
//        public void GetTest()
//        {
//            server
//              .Get("/GetData")
//              .Expect(200)
//              .End();
//        }

//        [Fact]
//        public void CreateTestNote()
//        {
//            server
//            .Post("/PostData")
//            .Send(TestNoteOne)
//            .Expect(201)
//            .End();
//        }

//        [Fact]
//        public void EditTestNote()
//        {
//            server
//            .Put("/PutData")
//            .Send(TestNoteTwo)
//            .Expect(200)
//            .End();
//        }

//        [Fact]
//        public void DeleteTestNote()
//        {
//            server
//            .Delete("/DeleteData/1")
//            .Expect(204)
//            .End();
//        }


//    }

//}

