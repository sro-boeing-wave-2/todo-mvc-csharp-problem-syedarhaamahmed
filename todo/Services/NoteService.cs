using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo.Model;
using todo.Models;

namespace todo.Services
{
    public class NoteService : INoteService

    {
        private readonly todoContext _testcontext;

        public NoteService(todoContext context)
        {
            _testcontext = context;
        }

        public async Task<List<Data>> GetAllNotesData()
        {
            return await _testcontext.Data.Include(a => a.checklist).Include(b => b.label).ToListAsync();
        }

        public async Task<List<Data>> GetMultipleLabels(string title, string label, bool? pinned)
        {
            return await _testcontext.Data.Include(x => x.checklist).Include(x => x.label).Where(
            m => ((string.IsNullOrEmpty(title) || (m.Title == title)) && ((string.IsNullOrEmpty(label)) || (m.label)
            .Any(b => b.text == label)) && ((!pinned.HasValue) || (m.IsPinned == pinned))))
            .ToListAsync();
        }

        public async Task<Data> GetNoteByID(int id)
        {
            return await _testcontext.Data.Include(a => a.checklist).Include(b => b.label).SingleOrDefaultAsync(c => c.ID == id);
        }

        public async Task<Data> CreateNoteData(Data note)
        {
            _testcontext.Data.Add(note);
             await _testcontext.SaveChangesAsync();
             return await Task.FromResult(note);
        }


        public async Task<Data> DeleteNoteData(int id)
        {
            var data = await _testcontext.Data.Include(a => a.checklist).Include(b => b.label).SingleOrDefaultAsync(c => c.ID == id);
            if (data == null)
            {
                throw new NotImplementedException();
            }
            var afterDeletionData = _testcontext.Data.Remove(data);
            _testcontext.SaveChanges();

            return afterDeletionData.Entity;
        }

        public async Task<Data> EditNoteData(Data note)
        {
            _testcontext.Data.Update(note);
            await _testcontext.SaveChangesAsync();
            return await Task.FromResult(note);
        }


    }
}
