using ElevenNote.Models.NoteModels;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ElevenNote.WEBMVC.Controllers.WebAPI
{
    [Authorize]
    [RoutePrefix("api/Note")]
    public class NoteController : ApiController
    {
        private bool SetStarState(int noteId, bool newState)
        {
            // Create the service
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);

            // Get the note
            var detail = service.GetNoteById(noteId);

            // Create the NoteEdit model instance with the new star state
            var updatedNote =
                new NoteEdit
                {
                    NoteID = detail.NoteID,
                    Title = detail.Title,
                    Content = detail.Content,
                    IsStarred = newState,
                    CategoryID = detail.CategoryID
                };

            // Return a value indicating whether the update succeeded
            return service.UpdateNote(updatedNote);
        }
        private async Task<bool> SetStarStateAsync(int noteId, bool newState)
        {
            // Create the service
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);

            // Get the note
            var detail = await service.GetNoteByIdAsync(noteId);

            // Create the NoteEdit model instance with the new star state
            var updatedNote =
                new NoteEdit
                {
                    NoteID = detail.NoteID,
                    Title = detail.Title,
                    Content = detail.Content,
                    IsStarred = newState,
                    CategoryID = detail.CategoryID
                };

            // Return a value indicating whether the update succeeded
            return await service.UpdateNoteAsync(updatedNote);
        }

        [Route("{id}/Star")]
        [HttpPut]
        public bool ToggleStarOn(int id) => SetStarState(id, true);

        [Route("{id}/Star")]
        [HttpDelete]
        public bool ToggleStarOff(int id) => SetStarState(id, false);
        [Route("{id}/StarAsync")]
        [HttpPut]
        public async Task<bool> ToggleStarOnAsync(int id) => await SetStarStateAsync(id, true);

        [Route("{id}/StarAsync")]
        [HttpDelete]
        public async Task<bool> ToggleStarOffAsync(int id) => await SetStarStateAsync(id, false);
    }
}

