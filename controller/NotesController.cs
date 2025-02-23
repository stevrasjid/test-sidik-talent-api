using Microsoft.AspNetCore.Mvc;

public class Note
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}

[ApiController]
[Route("/api/[controller]")]
public class NotesController : BaseApiController
{
    private static List<Note> _notes = new List<Note>();

    [HttpGet(Name = "GetNotes")]
    public IActionResult GetNotes()
    {
        return ApiResponse(_notes);
    }

    [HttpDelete("{id}", Name = "DeleteNote")]
    public IActionResult Delete(int id)
    {
        var note = _notes.SingleOrDefault(x => x.Id == id);
        if (note == null) return BadRequest("Note not found");

        _notes.Remove(note);
        return ApiResponse(new List<Note>());
    }

    [HttpGet("{id}", Name = "GetNote")]
    public IActionResult Get(int id)
    {
        var note = _notes.SingleOrDefault(x => x.Id == id);
        if (note == null) return BadRequest("Note not found");

        return ApiResponse(note);
    }

    [HttpPut(Name = "PutNote")]
    public IActionResult Put([FromBody] Note note)
    {
        if (string.IsNullOrWhiteSpace(note.Title)) return BadRequest("Title is empty");
        var noteFromList = _notes.SingleOrDefault(x => x.Id == note.Id);
        if (noteFromList == null) return BadRequest("Note not found");

        noteFromList.Title = note.Title;
        noteFromList.Description = note.Description;

        return ApiResponse(noteFromList);
    }

    [HttpPost(Name = "PostNote")]
    public IActionResult Post([FromBody] Note note)
    {
        if (string.IsNullOrWhiteSpace(note.Title)) return BadRequest("Title is empty");
        if (note.Id == 0)
        {
            note.Id = _notes.Count() + 1;
            while (_notes.Any(x => x.Id == note.Id))
            {
                note.Id++;
            }
        }
        _notes.Add(note);
        return ApiResponse(note);
    }
}