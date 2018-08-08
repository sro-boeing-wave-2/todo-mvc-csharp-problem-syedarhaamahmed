using System;
using todo.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todo.Services
{
    public interface INoteService
    {
        Task<List<Data>> GetAllNotesData();
        Task<List<Data>> GetMultipleLabels(string title, string label, bool? pinned);
        Task<Data> GetNoteByID(int id);
        Task<Data> CreateNoteData(Data note);
        Task<Data> DeleteNoteData(int note);
        Task<Data> EditNoteData(Data note);


    }
}
