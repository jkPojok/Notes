import "./Styles/NoteStyles.css"

export default function Note(props) {

  const submitForm = async (e) => {

    e.preventDefault();

    await props.updateNote({
      id: props.note.id,
      title: e.target.elements["note-title"].value,
      text: e.target.elements["note-text"].value
    });
    window.location.reload();
  }

  const deleteNote = async () => {
    await props.deleteNote(props.note.id);
    window.location.reload();
  }

  return (
    <form className="note" onClick={e => e.stopPropagation()} onSubmit={submitForm}>
      <div className="note-header">
        <input type="text" name="note-title" className="note-title" defaultValue={props.note.title} />
      </div>

      <div className="note-body">
        <textarea name="note-text" className="note-text" defaultValue={props.note.text} />
      </div>

      <div className="note-footer">
        <div>
          <span className="note-data">Дата редактирования: {props.note.dateOfEditing}</span> <br />
          <span className="note-data">Дата создания: {props.note.dateOfCreation}</span>
        </div>
        <div className="buttons-container">
          <input type="button" className="delete-button" onClick={deleteNote} value="Delete"></input>
          <input type="submit" className="save-button" value="Save" />
        </div>
      </div>
    </form>
  );
}