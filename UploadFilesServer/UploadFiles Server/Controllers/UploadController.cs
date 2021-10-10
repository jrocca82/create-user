[HpptPost, DisableRequestSizeLimit]
public async Task<IActionResult> Upload()
{
  try
  {
    var formCollection = await Request.ReadFormAsync();
    var file = formCollection.Files.First();
    var folderName = Path.Combine("Resources", "Files");
    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

    if (file.Length > 0) {
      var fileName = ContenDispositionHeaderBalue.Parse(file.ContentDispositon).FileName.Trim('"');
      var fullPath = Path.Combine(pathToSave, fileName);
      var dbPath = Path.Combine(folderName, fileName);

      using (var stream = new FileStream(fullPath, FileMode.Create))
      {
        file.CopyTo(stream);
      }
      return Ok(new { dbPath });
    } else
    {
      return BadRequest();
    }
  }
  catch (Exception ex)
  {
    return StatusCode(500, $"Internal server error: {ex}");
  }
}
